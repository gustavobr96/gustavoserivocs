namespace Sistema.Bico.Domain.Command
{
    public class AuthenticationCommandBase
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
    }
}
