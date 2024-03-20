using SistemaBico.Web.Enum;

namespace SistemaBico.Web.Models
{
    public class TermUse
    {
        public string Description { get; set; }
        public TypeTerm TypeTerm { get; set; }
        public int Version { get; set; }
        public bool Active { get; set; }
    }
}
