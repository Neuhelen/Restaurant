namespace Resturant.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }

        private string Role { get; }
    }
}
