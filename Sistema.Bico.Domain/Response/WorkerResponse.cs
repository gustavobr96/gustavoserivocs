using System;
using System.Collections.Generic;

namespace Sistema.Bico.Domain.Response
{
    public class WorkerResponse
    {
        public Guid Id { get; set; }
        public string Created { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string? Titulo { get; set; }
        public string? Phone { get; set; }
        public int Area { get; set; }
        public bool? Remote { get; set; } = false;
        public string? Sobre { get; set; }
        public string? Profession { get; set; }
        public string? CEP { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public int? Interessados { get; set; }
        public string AreaName { get; set; }
    }
}
