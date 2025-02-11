using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Bico.Domain.Interface.Services
{
    public interface INotificacoesService
    {
        Task DispararNotificacaoNovoServico(string? cidade, int codigoArea, string mensagem);
        Task DispararNotificacaoPendenteAprovacao(string? token);
    }
}
