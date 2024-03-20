using MediatR;
using Sistema.Bico.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Sistema.Bico.Domain.Command
{
    public class AddProfessionalCommand : IRequest<Unit>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public byte[] PerfilPicture { get; set; }
        public Guid ClientId { get; set; }
        public Guid AddressId { get; set; }
        public Guid ProfessionalAreaId { get; set; }
        public string About { get; set; }
        public string Profession { get; set; }
        public List<ProfessionalEspeciality> Especiality { get; set; }
        public  Client Client { get; set; }
        public  Address Address { get; set; }
        public  ProfessionalArea ProfissionalArea { get; set; }
    }
}
