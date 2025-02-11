using Sistema.Bico.Domain.Generics.Entities;
using Sistema.Bico.Domain.Generics.Interfaces;
using Sistema.Bico.Domain.Interface;
using Sistema.Bico.Domain.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.Service
{
    public class NotificacoesService : INotificacoesService
    {
        private readonly IProfessionalProfileRepository _professionalProfileRepository;
        private readonly IFirebaseNotificationService _firebaseNotificationService;
        private string _titulo;
        private string _mensagem;

        public NotificacoesService(IProfessionalProfileRepository professionalProfileRepository,
            IFirebaseNotificationService firebaseNotificationService)
        {
            _professionalProfileRepository = professionalProfileRepository;
            _firebaseNotificationService = firebaseNotificationService;
        }

        public async Task DispararNotificacaoNovoServico(string? cidade, int codigoArea, string mensagem)
        {
            _titulo = "Surgiu um novo trabalho para você!";
            var profissionais = await _professionalProfileRepository.GetProfessionalByAreaAndCity(cidade, codigoArea);

            List<string> tokens = profissionais
                                    .Where(p => p.Client != null && !string.IsNullOrEmpty(p.Client.TokenPhone)) 
                                    .Select(p => p.Client.TokenPhone)
                                    .Distinct()
                                    .ToList();

            if(tokens.Count > 0)
                await _firebaseNotificationService.EnviarNotificacaoParaProfissionais(tokens, _titulo, mensagem);
        }

        public async Task DispararNotificacaoPendenteAprovacao(string perfil)
        {
            _titulo = "Novo cliente interessado!";
            _mensagem = "Um cliente quer contratar seus serviços. Acesse agora para aprovar ou recusar a solicitação.";


            if (!string.IsNullOrEmpty(perfil))
            {
                var profissionais = await _professionalProfileRepository.GetProfessionalPerfilClient(perfil);


                if (!string.IsNullOrEmpty(profissionais.Client?.TokenPhone))
                     await _firebaseNotificationService.EnviarNotificacaoParaProfissionais(new List<string> { profissionais.Client?.TokenPhone }, _titulo, _mensagem);
            }
               
        }
    }
}
