 using Microsoft.EntityFrameworkCore;
using Sistema.Bico.Domain.Command;
using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Enums;
using Sistema.Bico.Domain.Generics.Extensions;
using Sistema.Bico.Domain.Interface;
using Sistema.Bico.Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Bico.Infra.Repository
{
    public class DoneTransactionRepository : IDoneTransactionRepository
    {
        private readonly ContextBase _context;

        public DoneTransactionRepository(ContextBase context)
        {
            this._context = context;
        }

        public async Task DoneWorkerTransaction(DoneWorkerCommand doneWorkerCommand)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var professional = await _context.ProfessionalProfile.FirstOrDefaultAsync(f => f.Perfil == doneWorkerCommand.PrestadorPerfil);
                if (professional != null)
                {
                    var workerDoneProfessional = await _context.WorkerDoneProfessional
                        .FirstOrDefaultAsync(f => f.ProfessionalProfile.Perfil == doneWorkerCommand.PrestadorPerfil);

                    if (workerDoneProfessional == null)
                    {
                        workerDoneProfessional = new WorkerDoneProfessional
                        {
                            Id = Guid.NewGuid(), // Use Guid.NewGuid(), não new Guid()
                            ProfessionalProfileId = Guid.Empty
                        };
                    }

                    var workerDone = new WorkerDone
                    {
                        WorkerDoneProfessionalId = workerDoneProfessional.Id
                    };

                    if (doneWorkerCommand.WorkerId != null)
                    {
                        var workerId = doneWorkerCommand.WorkerId;

                        _context.WorkerProfessional.RemoveRange(_context.WorkerProfessional.Where(w => w.WorkerId == workerId));

                        var worker = await _context.Worker.FirstOrDefaultAsync(w => w.Id == workerId);

                        if (worker != null)
                        {
                            workerDone.Description = worker.Title;
                            _context.Worker.Remove(worker);
                        }
                        else
                        {
                            workerDone.Description = string.IsNullOrEmpty(doneWorkerCommand.Description)
                                ? "Nenhuma descrição."
                                : doneWorkerCommand.Description;
                        }
                    }

                    var client = await _context.Client.FirstOrDefaultAsync(f => f.Id == doneWorkerCommand.ClientId);

                    workerDone.Avaliation = Math.Round(((decimal)doneWorkerCommand.AvaliationQuality +
                                                         doneWorkerCommand.AvaliationCommunication +
                                                         doneWorkerCommand.AvaliationDeadline) / 3, 2);

                    var three = await _context.ThreeAvaliation
                        .FirstOrDefaultAsync(w => w.ProfessionalProfileId == professional.Id);

                    decimal mediaAvaliationProfessional;

                    if (three == null)
                    {
                        three = new ThreeAvaliation
                        {
                            Deadline = doneWorkerCommand.AvaliationDeadline,
                            Quality = doneWorkerCommand.AvaliationQuality,
                            Communication = doneWorkerCommand.AvaliationCommunication,
                            NumberAvaliation = 1,
                            ProfessionalProfileId = professional.Id
                        };

                        await _context.ThreeAvaliation.AddAsync(three);
                        mediaAvaliationProfessional = workerDone.Avaliation;
                    }
                    else
                    {
                        three.Deadline += doneWorkerCommand.AvaliationDeadline;
                        three.Quality += doneWorkerCommand.AvaliationQuality;
                        three.Communication += doneWorkerCommand.AvaliationCommunication;
                        three.NumberAvaliation += 1;

                        _context.ThreeAvaliation.Update(three);
                        mediaAvaliationProfessional = Math.Round(((decimal)three.Deadline +
                                                                  three.Quality +
                                                                  three.Communication) / (three.NumberAvaliation * 3), 2);
                    }

                    var verifyPlan = await _context.ProfessionalPayment
                        .Where(w => w.ProfessionalId == professional.Id &&
                                    w.Created.AddDays(31) >= DateTime.Now &&
                                    w.Status == StatusPayment.APRO.GetDescription())
                        .FirstOrDefaultAsync();

                    if (verifyPlan == null)
                        professional.Ativo = false;

                    professional.Avaliation = mediaAvaliationProfessional;
                    _context.ProfessionalProfile.Update(professional);

                    var professionalClient = await _context.ProfessionalClient
                        .FirstOrDefaultAsync(w =>
                            w.ProfessionalProfileId == professional.Id &&
                            w.ClientId == doneWorkerCommand.ClientId &&
                            w.StatusWorker == StatusWorker.Contratado);

                    if (professionalClient != null)
                    {
                        professionalClient.StatusWorker = StatusWorker.Finalizado;
                        _context.ProfessionalClient.Update(professionalClient);
                    }

                    workerDone.Comment = string.IsNullOrEmpty(doneWorkerCommand.Comment)
                        ? "Nenhum comentário."
                        : doneWorkerCommand.Comment;

                    workerDone.Name = $"{client?.Name} {client?.LastName}";

                    if (workerDoneProfessional.ProfessionalProfileId == Guid.Empty)
                    {
                        workerDoneProfessional.ProfessionalProfileId = professional.Id;
                        workerDoneProfessional.WorkerDone = new List<WorkerDone> { workerDone };
                        await _context.WorkerDoneProfessional.AddAsync(workerDoneProfessional);
                    }
                    else
                    {
                        await _context.WorkerDone.AddAsync(workerDone);
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw; 
            }
        }

    }
}
