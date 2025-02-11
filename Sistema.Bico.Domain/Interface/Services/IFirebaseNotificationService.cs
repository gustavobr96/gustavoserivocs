using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.Interface.Services
{
    public interface IFirebaseNotificationService
    {
        Task EnviarNotificacaoParaProfissionais(List<string> tokens, string titulo, string mensagem);
    }
}
