using Sistema.Bico.Domain.Enums;
using System;

namespace Sistema.Bico.Domain.Entities
{
    public class ProfessionalPayment : Base
    {
        public string? PagamentoId { get; set; }
        public Guid ProfessionalId { get; set; }
        public string Status { get; set; }
        public string Detalhes { get; set; } // JSON ou informações adicionais
        public virtual ProfessionalProfile Professional { get; set; }
    }
}
