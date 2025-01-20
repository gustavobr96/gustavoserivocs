using Sistema.Bico.Domain.Enums;
using Sistema.Bico.Domain.Generics.Entities;
using System;
using System.Collections.Generic;

namespace Sistema.Bico.Domain.Entities
{
    public class Client : Base
    {
        public Client()
        {
            ProfessionalClient = new HashSet<ProfessionalClient>();
        }
        public string Name { get;  set; }
        public string LastName { get;  set; }
        public byte[] PerfilPicture { get;  set; }
        public string Email { get;  set; }
        public TypePeople TypePeople { get;  set; }
        public string CpfCnpj { get;  set; }        
        public bool IsServiceProvider { get;  set; }
        public DateTime? Cancellation { get;  set; }
        public bool Enable { get;  set; }

        public virtual ICollection<ApplicationUser> ApplicationUser { get; set; }
        public virtual ICollection<ProfessionalClient>? ProfessionalClient { get; set; }

        public void UpdateProfile(string name, string lastName,  string perfilPicture)
        {
            Name = name;
            LastName = lastName;
            if (!string.IsNullOrEmpty(perfilPicture))
                PerfilPicture = Convert.FromBase64String(perfilPicture);
        }
    }
}
