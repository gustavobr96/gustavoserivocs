namespace SistemaBico.Web.Models
{
    public class WorkerDoneDto
    {
        public string PrestadorPerfil { get; set; }
        public Guid? ClientId { get; set; }
        public string? WorkerIdString { get; set; }
        public Guid? WorkerId { get; set; }
        public int AvaliationDeadline { get; set; }
        public int AvaliationQuality { get; set; }
        public int AvaliationCommunication { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }


        public void ValidationWorkerDone(string clientId)
        {
            ClientId = Guid.Parse(clientId);

            AvaliationDeadline = AvaliationDeadline > 5 ? 5 : AvaliationDeadline;
            AvaliationQuality = AvaliationQuality > 5 ? 5 : AvaliationQuality;
            AvaliationCommunication = AvaliationCommunication > 5 ? 5 : AvaliationCommunication;

            AvaliationDeadline = AvaliationDeadline < 1 ? 1 : AvaliationDeadline;
            AvaliationQuality = AvaliationQuality < 1 ? 1 : AvaliationQuality;
            AvaliationCommunication = AvaliationCommunication < 1 ? 1 : AvaliationCommunication;

            if (!string.IsNullOrEmpty(WorkerIdString) && WorkerIdString != "0")
                WorkerId = Guid.Parse(WorkerIdString);
        }
    }
}
