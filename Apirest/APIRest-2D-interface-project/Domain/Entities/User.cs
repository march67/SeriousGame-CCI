namespace APIRest_2D_interface_project.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public Boolean IsVerified { get; set; }
    }
}
