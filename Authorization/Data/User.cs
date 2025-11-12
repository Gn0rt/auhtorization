namespace Authorization.Data
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public List<string> MainPermissions { get; set; } = new List<string>();
        public List<string> SubPermissions { get; set; } = new List<string>();

    }
}