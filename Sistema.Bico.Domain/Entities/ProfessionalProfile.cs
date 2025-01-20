using System;
using System.Collections.Generic;

namespace Sistema.Bico.Domain.Entities
{
    public class ProfessionalProfile : Base
    {
        public ProfessionalProfile()
        {
            Especiality = new HashSet<ProfessionalEspeciality>();
            WorkerProfessional = new HashSet<WorkerProfessional>();
            ProfessionalClient = new HashSet<ProfessionalClient>();
        }

        public string Name { get; set; }
        public string LastName { get; set; }
        public string Perfil { get; set; }
        public string Phone { get; set; }
        public byte[] PerfilPicture { get; set; }
        public Guid ClientId { get; set; }
        public Guid? AddressId { get; set; }
        public Guid? ProfessionalAreaId { get; set; }
        public string About { get; set; }
        public string Profession { get; set; }
        public decimal? Avaliation { get; set; }
        public bool Ativo { get; set; } = true;
        public bool Premium { get; set; } = false;
        public virtual Client Client { get; set; }
        public  Address Address { get; set; }
        public  ProfessionalArea ProfessionalArea { get; set; }
    
        public virtual ICollection<ProfessionalEspeciality>? Especiality { get; set; }
        public virtual ICollection<WorkerProfessional>? WorkerProfessional { get; set; }
        public virtual ICollection<ProfessionalClient>? ProfessionalClient { get; set; }
        public virtual ICollection<WorkerDoneProfessional>? WorkerDoneProfessional { get; set; }

        public void UpdateProfile(string name, string lastName, string phone, string perfilPicture, string about, string profession, ProfessionalArea professionalArea)
        {
            Name = name;
            LastName = lastName;
            Phone = phone;
            if(!string.IsNullOrEmpty(perfilPicture))
              PerfilPicture = Convert.FromBase64String(perfilPicture);
            About = about;
            Profession = profession;
            ProfessionalArea = professionalArea;
        }
        public void SetPremium()
        {
            Ativo = true;
            Premium = true;
        }
        public void SetEstorno()
        {
            Ativo = false;
            Premium = false;
        }
        public void UpdateAddress(Address address)
        {
            Address.UpdateAddress(address.Logradouro, address.Number, address.Bairro, address.Complemento, address.ZipCode, address.City, address.State);
        }
    }
}
