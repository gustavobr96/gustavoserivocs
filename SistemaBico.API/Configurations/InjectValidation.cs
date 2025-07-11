﻿using MediatR;
using Sistema.Bico.Domain.Command;
using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Generics.Entities;
using Sistema.Bico.Domain.Generics.Interfaces;
using Sistema.Bico.Domain.Generics.Result;
using Sistema.Bico.Domain.Interface;
using Sistema.Bico.Domain.UseCases.Cliente;
using Sistema.Bico.Domain.UseCases.Email;
using Sistema.Bico.Domain.UseCases.PaymentProfessional;
using Sistema.Bico.Domain.UseCases.Plan;
using Sistema.Bico.Domain.UseCases.Professional;
using Sistema.Bico.Domain.UseCases.Worker;
using Sistema.Bico.Infra.Dapper.Repository;
using Sistema.Bico.Infra.Generics.Repository;
using Sistema.Bico.Infra.Repository;

namespace SistemaBico.API.Configurations
{
    public static class InjectNative
    {
        public static void AddInjectValidation(this IServiceCollection services)
        {
        }

        public static void AddInjectHandlers(this IServiceCollection services)
        {

          
            services.AddScoped<IRequestHandler<AddClientCommand, Unit>, RegisterClientCommandHandler>();
            services.AddScoped<IRequestHandler<ForgotCommand, Unit>, ForgotClientCommandHandler>();
            services.AddScoped<IRequestHandler<AddWorkerCommand, Unit>, RegisterWorkertCommandHandler>();
            services.AddScoped<IRequestHandler<AddProfessionalCommand, Unit>, RegisterProfessionalCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateProfessionalCommand, ProfessionalProfile>, UpdateProfessionalCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateClientCommand, ApplicationUser>, UpdateClientCommandHandler>();
            services.AddScoped<IRequestHandler<UpdatePasswordCommand, Result>, UpdatePasswordCommandHandler>();
            services.AddScoped<IRequestHandler<ApplyWorkerCommand, Unit>, ApplyWorkerCommandHandler>();
            services.AddScoped<IRequestHandler<ApplyProfessionalCommand, Unit>, ApplyProfessionalCommandHandler>();
            services.AddScoped<IRequestHandler<CancelApplyProfessionalCommand, Unit>, CancelApplyProfessionalCommandHandler>();
            services.AddScoped<IRequestHandler<DoneWorkerCommand, Unit>, DoneWorkerCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateProfessionalClientCommand, Result>, UpdateProfessionalClientCommandHandler>();
            services.AddScoped<IRequestHandler<AddPaymentProfessionalCommand, string>, RealizarPagamentoCommandHandler>();
            services.AddScoped<IRequestHandler<UpdatePaymentCommand, Unit>, AtualizaStatusPagamentoCommandHandler>();
            services.AddScoped<IRequestHandler<ApprovalOrRecusedCommand, Unit>, ApproveOrRecuseCommandHandler>();
            services.AddScoped<IRequestHandler<WorkerCancellPlansCommand, Unit>, WorkerCancelPlanCommandHandler>();
            services.AddScoped<IRequestHandler<SendEmailCommand, Unit>, SendEmailCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateClientTokenCommand, Unit>, UpdateClientTokenCommandHandler>();



         
        }

        public static void AddInjectRepositorys(this IServiceCollection services)
        {
     
            services.AddSingleton(typeof(IGeneric<>), typeof(RepositoryGenerics<>));
            //services.AddSingleton(typeof(IConnectFactory), typeof(ConnectFactoryCreator));
            services.AddScoped<IToken, Token>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IIdentityRepository, IdentityRepository>();
            services.AddScoped<IProfessionalProfileRepository, ProfessionalProfileRepository>();
            services.AddScoped<IProfessionalAreaRepository, ProfessionalAreaRepository>();
            services.AddScoped<IProfessionalEspecialityRepository, ProfessionalEspecialityRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<ITermUseRepository, TermUseRepository>();
            services.AddScoped<IWorkerRepository, WorkerRepository>();
            services.AddScoped<IThreeAvaliationRepository, ThreeAvaliationRepository>();
            services.AddScoped<IWorkerProfessionalRepository, WorkerProfessionalRepository>();
            services.AddScoped<IProfessionalClientRepository, ProfessionalClientRepository>();
            services.AddScoped<IWorkerDoneProfessionalRepository, WorkerDoneProfessionalRepository>();
            services.AddScoped<IWorkerDoneRepository, WorkerDoneRepository>();
            services.AddScoped<IDoneTransactionRepository, DoneTransactionRepository>();
            services.AddScoped<IProfessionalPaymentRepository, ProfessionalPaymentRepository>();
            services.AddScoped<ITemplateRepository, TemplateRepository>();
            services.AddScoped<IPlanDapperRepository, PlanDapperRepository>();
            services.AddScoped<IUserDapperRepository, UserDapperRepository>();
            services.AddScoped<IDapperProfessionalClientRepository, DapperProfessionalClientRepository>();
            services.AddScoped<IDapperWorkerRepository, DapperWorkerRepository>();
        }
    }
}
