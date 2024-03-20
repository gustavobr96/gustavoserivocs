using Microsoft.AspNetCore.Identity;
using Sistema.Bico.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Sistema.Bico.Domain.Generics.Entities
{
    public  class ApplicationUser : IdentityUser
    {
        public Guid ClientId { get; set; }
        public virtual Client Client { get; set; }
    }
}
