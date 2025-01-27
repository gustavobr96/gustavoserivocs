using System;
using System.Collections.Generic;

namespace Sistema.Bico.Domain.Command.Filters
{
    public class FilterWorkerCommand : FilterPaginatedBaseCommand
    {
        public string? City { get; set; }
        public string? Profession { get; set; }
        public int? Area { get; set; }
        public bool Remote { get; set; }
        public Guid ClientId { get; set; }
    }
}
