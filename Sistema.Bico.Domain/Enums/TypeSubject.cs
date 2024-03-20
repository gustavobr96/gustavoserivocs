using System.ComponentModel;

namespace Sistema.Bico.Domain.Enums
{
    public enum TypeSubject
    {
        [Description("Conta Criada Com Sucesso!")]
        Cadastro = 1,
        [Description("Recuperação de Senha!")]
        RecuperacaoSenha = 2,
        [Description("Pagameno Confirmado!")]
        PagamentoConfirmado = 3,
    }
}
