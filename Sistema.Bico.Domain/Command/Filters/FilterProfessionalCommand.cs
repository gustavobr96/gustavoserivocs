using System;
using System.Collections.Generic;

namespace Sistema.Bico.Domain.Command.Filters
{
    public class FilterProfessionalCommand : FilterPaginatedBaseCommand
    {
        public string? City { get; set; }
        public List<string> Especiality { get; set; }
        public int? Area { get; set; }
        public string Profession { get; set; }
        public Guid ClientId { get; set; }
    }
}
