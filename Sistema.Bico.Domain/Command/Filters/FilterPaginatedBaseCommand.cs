namespace Sistema.Bico.Domain.Command.Filters
{
    public class FilterPaginatedBaseCommand
    {
        public int Page { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
