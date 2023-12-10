namespace Resturant.Models
{
    public class ChangePassword
    {
        private string? password;
        private string? newPassword;

        public string Password { get => password ?? string.Empty; set => password = value; }
        public string NewPassword { get => newPassword ?? string.Empty; set => newPassword = value; }
    }
}
