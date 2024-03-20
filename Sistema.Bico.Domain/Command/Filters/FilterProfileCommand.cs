namespace Sistema.Bico.Domain.Command.Filters
{
    public class FilterProfileCommand : FilterPaginatedBaseCommand
    {
        public string Profile { get; set; }
    }
}
