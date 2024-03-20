using Sistema.Bico.Domain.Enums;

namespace Sistema.Bico.Domain.Entities
{
    public class Template : Base
    {
        public string Description { get; set; }
        public TypeTemplate TypeTemplate { get; set; }
    }
}
