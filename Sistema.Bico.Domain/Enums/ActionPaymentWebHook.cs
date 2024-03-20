using System.ComponentModel;

namespace Sistema.Bico.Domain.Enums
{
    public enum ActionPaymentWebHook
    {
        [Description("payment.updated")]
        UPDATE = 1,
        [Description("payment.created")]
        CREATE = 2
    }
}
