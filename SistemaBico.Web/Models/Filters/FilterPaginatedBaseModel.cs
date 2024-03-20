namespace SistemaBico.Web.Models.Filters
{
    public class FilterPaginatedBaseModel
    {
        public int Page { get; set; } = 1;
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 12;
    }
}
