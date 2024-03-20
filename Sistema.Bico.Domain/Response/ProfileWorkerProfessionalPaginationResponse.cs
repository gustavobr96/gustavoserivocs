using System.Collections.Generic;

namespace Sistema.Bico.Domain.Response
{
    public class ProfileWorkerProfessionalPaginationResponse
    {
        public List<WorkerDoneResponse> WorkerDone { get; set; }
        public ProfessionalProfileResponse ProfessionalProfile { get; set; }
        public ThreeAvaliationResponse ThreeAvaliation { get; set; }
        public int CountRegister { get; set; }
    }
}
