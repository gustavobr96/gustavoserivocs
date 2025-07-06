using System;

namespace Sistema.Bico.Domain.Response
{
    public class ThreeAvaliationResponse
    {
        public Guid Id { get; set; }
        public decimal Deadline { get; set; }
        public decimal Quality { get; set; }
        public decimal Communication { get; set; }
        public int NumberAvaliation { get; set; }
    }
}
