namespace JwtAuthenticationManager.Models
{
    public class UserAccount
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<string>  Role { get; set; }
    }
}
