using System.Collections.Generic;

namespace Sistema.Bico.Domain.Response
{
    public class WorkerPaginationResponse
    {
        public List<WorkerResponse> Worker { get; set; }
        public int CountRegister { get; set; }
    }
}
