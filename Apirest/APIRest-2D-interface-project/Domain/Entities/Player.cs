namespace APIRest_2D_interface_project.Domain.Entities
{
    public class Player
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public int Level { get; set; }

        public int Gems { get; set; }

        public int Gold { get; set; }
    }
}
