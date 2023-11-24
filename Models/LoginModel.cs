namespace WebAPI_Project.Models
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        // Other properties relevant for login
    }
}
