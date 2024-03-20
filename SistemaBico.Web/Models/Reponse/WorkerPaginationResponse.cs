using SistemaBico.Web.Models.Filters;

namespace SistemaBico.Web.Models.Reponse
{
    public class WorkerPaginationResponse : FilterPaginatedWorkerModel
    {
        public List<WorkerDto> Worker { get; set; }
        public int CountRegister { get; set; }
        public int PagesSize { get; set; }
    }
}
