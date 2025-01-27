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
            using var transaction = _context.Database.BeginTransaction();

            try
            {

                var professional = await _context.ProfessionalProfile.FirstOrDefaultAsync(f => f.Perfil == doneWorkerCommand.PrestadorPerfil);
                if (professional != null)
                {
                    var workerDoneProfessional = await _context.WorkerDoneProfessional.FirstOrDefaultAsync(f => f.ProfessionalProfile.Perfil == doneWorkerCommand.PrestadorPerfil);
                    if (workerDoneProfessional == null)
                    {
                        workerDoneProfessional = new WorkerDoneProfessional();
                        workerDoneProfessional.Id = new Guid();
                        workerDoneProfessional.ProfessionalProfileId = Guid.Empty;
                    }

                    var workerDone = new WorkerDone();
                    workerDone.WorkerDoneProfessionalId = workerDoneProfessional.Id;


                    if (doneWorkerCommand.WorkerId != null)
                    {
                        var workerId = doneWorkerCommand.WorkerId;

                        _context.WorkerProfessional.RemoveRange(_context.WorkerProfessional.Where(w => w.WorkerId == workerId));

                        var worker = await _context.Worker.Where(w => w.Id == workerId).FirstOrDefaultAsync();

                        if (worker != null) // Remove worker publicado.
                        {
                            workerDone.Description = worker.Title;
                            _context.Worker.Remove(worker);
                        }
                        else
                        {
                            workerDone.Description = string.IsNullOrEmpty(doneWorkerCommand.Description) ? "Nenhuma descrição." : doneWorkerCommand.Description;
                        }
                    }//

                    var client = await _context.Client.FirstOrDefaultAsync(f => f.Id == doneWorkerCommand.ClientId);

                    workerDone.Avaliation = Math.Round( ((decimal)doneWorkerCommand.AvaliationQuality +
                                                                 doneWorkerCommand.AvaliationCommunication +
                                                                 doneWorkerCommand.AvaliationDeadline) / 3, 2);

                    var three = _context.ThreeAvaliation.Where(w => w.ProfessionalProfileId == professional.Id).FirstOrDefault();
                    decimal mediaAvaliationProfessional;
                    if(three == null)
                    {
                        three =  new ThreeAvaliation
                        {
                            Deadline = doneWorkerCommand.AvaliationDeadline,
                            Quality = doneWorkerCommand.AvaliationQuality,
                            Communication = doneWorkerCommand.AvaliationCommunication,
                            NumberAvaliation = 1,
                            ProfessionalProfileId = professional.Id,
                        };

                        _context.ThreeAvaliation.Add(three);
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
                                                                three.Communication) / (three.NumberAvaliation*3), 2);
                    }

                    var verifyPlan = await _context.ProfessionalPayment.Where(w => w.ProfessionalId == professional.Id && w.Created.AddDays(31) >= DateTime.Now && w.Status == StatusPayment.APRO.GetDescription()).FirstOrDefaultAsync();
                    if(verifyPlan == null) // Verifica plano e desativa profissional.
                       professional.Ativo = false;
                    
                    professional.Avaliation = mediaAvaliationProfessional;
                    _context.ProfessionalProfile.Update(professional);

                    // Finalizar serviço
                    var professionalClient = await _context.ProfessionalClient.FirstOrDefaultAsync(w => w.ProfessionalProfileId == professional.Id && w.ClientId == doneWorkerCommand.ClientId && w.StatusWorker == StatusWorker.Contratado);
                    professionalClient.StatusWorker = StatusWorker.Finalizado;
                    _context.ProfessionalClient.Update(professionalClient);

                    workerDone.Comment = string.IsNullOrEmpty(doneWorkerCommand.Comment) ? "Nenhum comentário." : doneWorkerCommand.Comment;
                    workerDone.Name = $"{client.Name} {client.LastName}";

                    if(workerDoneProfessional.ProfessionalProfileId == Guid.Empty)
                    {
                        workerDoneProfessional.ProfessionalProfileId = professional.Id;
                        var listaWorkerDone = new List<WorkerDone>();
                        listaWorkerDone.Add(workerDone);

                        workerDoneProfessional.WorkerDone = listaWorkerDone;
                        _context.WorkerDoneProfessional.Add(workerDoneProfessional);
                    }
                    else
                    {
                        _context.WorkerDone.Add(workerDone);
                    }

                   
                    _context.SaveChanges();
                    transaction.Commit();
                }

            }
            catch (Exception e)
            {
                transaction.Rollback();
            }
        }
    }
}
