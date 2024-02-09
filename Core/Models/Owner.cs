namespace Core.Models
{
    public class Owner : BaseEntity
    {
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string Profil { get; set; }
        public Address Address { get; set; }
    }
}
