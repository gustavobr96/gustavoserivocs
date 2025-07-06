using System;

namespace Sistema.Bico.Domain.Response
{
    public class WorkerDoneResponse
    {
        public Guid Id { get; set; }
        public string Created { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Avaliation { get; set; }
        public string Comment { get; set; }
    }
}
