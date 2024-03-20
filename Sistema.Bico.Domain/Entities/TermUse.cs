using Sistema.Bico.Domain.Enums;

namespace Sistema.Bico.Domain.Entities
{
    public class TermUse : Base
    {
        public string Description { get; set; }
        public TypeTerm TypeTerm { get; set; }
        public int Version { get; set; }
        public bool Active { get; set; }
    }
}
