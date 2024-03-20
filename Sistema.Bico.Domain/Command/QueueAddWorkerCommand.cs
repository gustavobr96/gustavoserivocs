using MediatR;
using Sistema.Bico.Domain.Entities;
using Sistema.Bico.Domain.Generics.Result;
using System;
using System.Collections.Generic;

namespace Sistema.Bico.Domain.Command
{
    public class QueueAddWorkerCommand : IRequest<Result>
    {
        public string Title { get; set; }
        public string Phone { get; set; }
        public string About { get; set; }
        public string Profession { get; set; }
        public double Price { get; set; }
        public Guid ClientId { get; set; }
        public Guid AddressId { get; set; }
        public Guid ProfessionalAreaId { get; set; }
        public bool IsConcluded { get; set; } = false;
        public virtual Client Client { get; set; }
        public virtual Address Address { get; set; }
        public virtual ProfessionalArea ProfessionalArea { get; set; }
    }
}
