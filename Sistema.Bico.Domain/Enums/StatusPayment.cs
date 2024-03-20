using System.ComponentModel;

namespace Sistema.Bico.Domain.Enums
{
    public enum StatusPayment
    {
        [Description("approved")]
        APRO = 1,
        [Description("rejected")]
        REPRO = 2,
        [Description("in_process")]
        PROCESS = 3,
        [Description("pending")]
        PENDING = 4,
        [Description("refunded")]
        ESTORNO = 5,
        [Description("authorized")]
        AUTHORIZED = 6,
        [Description("in_mediation")]
        MEDIATION = 7,
        [Description("charged_back")]
        CHARGED = 8, 
        [Description("cancelled")]
        CANCELED = 9,
    }
}
