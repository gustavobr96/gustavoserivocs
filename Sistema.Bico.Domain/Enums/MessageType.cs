using System.ComponentModel;

namespace Sistema.Bico.Domain.Enums
{
    public enum MessageType
    {
        [Description("RegisterCliente")]
        RegisterCliente = 1,
        [Description("RegisterWorker")]
        RegisterWorker = 2,   
        [Description("ApplyWorker")]
        ApplyWorker = 3,
        [Description("ApplyProfessional")]
        ApplyProfessional = 4, 
        [Description("CancelApplyProfessional")]
        CancelApplyProfessional = 5,
        [Description("DoneWorker")]
        DoneWorker = 6,
        [Description("UpdateProfessionalClient")]
        UpdateProfessionalClient = 7,
        [Description("Payment")]
        Payment = 8,
        [Description("WorkerCancelPlan")]
        WorkerCancelPlan = 9,
        [Description("SendEmail")]
        SendEmail = 10,
        [Description("Forgot")]
        Forgot = 11,
        [Description("RegisterProfessional")]
        RegisterProfessional = 12,
    }
}
