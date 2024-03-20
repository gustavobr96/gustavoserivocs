using Sistema.Bico.Domain.Enums;
using System;

namespace Sistema.Bico.Domain.Entities
{
    public class ProfessionalPayment : Base
    {
        public decimal Value { get; set; }
        public long? PagamentoId { get; set; }
        public Guid ProfessionalId { get; set; }
        public virtual ProfessionalProfile Professional { get; set; }
        public bool Enable { get; set; }
        public StatusPayment StatusPayment { get; set;}
    }
}
