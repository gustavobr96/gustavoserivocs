using System.Collections.Generic;

namespace Sistema.Bico.Domain.Response
{
    public class ProfessionalProfilePaginationResponse
    {
        public List<ProfessionalProfileResponse> ProfessionalProfile { get; set; }
        public int CountRegister { get; set; }
    }
}
