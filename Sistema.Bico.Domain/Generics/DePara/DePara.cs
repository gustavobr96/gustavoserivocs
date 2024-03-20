using Sistema.Bico.Domain.Enums;
using System.Collections.Generic;

namespace Sistema.Bico.Domain.Generics.DePara
{
    public static class DePara
    {
        public static readonly Dictionary<string, StatusPayment> DeParaStatusPayment = new()
        {
            { "approved", StatusPayment.APRO},
            { "rejected",  StatusPayment.REPRO},
            { "in_process", StatusPayment.PROCESS },
            { "pending", StatusPayment.PENDING },
            { "refunded", StatusPayment.ESTORNO },
            { "authorized", StatusPayment.AUTHORIZED },
            { "in_mediation", StatusPayment.MEDIATION },
            { "charged_back", StatusPayment.CHARGED },
            { "cancelled", StatusPayment.CANCELED },
        };

        public static readonly Dictionary<string, ActionPaymentWebHook> DeParaActionWebHook = new()
        {
            { "payment.updated", ActionPaymentWebHook.UPDATE},
            { "payment.created",  ActionPaymentWebHook.CREATE}
        };
    }
}
