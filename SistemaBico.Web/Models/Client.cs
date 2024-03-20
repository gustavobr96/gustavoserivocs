using SistemaBico.Web.Enum;

namespace SistemaBico.Web.Models
{
    public class Client
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? CpfCnpj { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? RPassword { get; set; }
        public string? PhoneNumber { get; set; }
        public byte[]? PerfilPicture { get; set; }

        public TypePeople TypePeople = TypePeople.Física; 

        public bool ValidateLogin()
        {
            return  (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password)) ? true : false;
        }
        public bool ValidatePassword()
        {
            return !string.IsNullOrEmpty(Password) && Password.Length > 6 ? true : false;
        }
        public bool ValidateRegister()
        {
            return (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(LastName) || string.IsNullOrEmpty(CpfCnpj) || string.IsNullOrEmpty(Email) 
                   || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(RPassword) || string.IsNullOrEmpty(PhoneNumber) || (!string.Equals(Password, RPassword))) ? true : false;
        }
    }
}
