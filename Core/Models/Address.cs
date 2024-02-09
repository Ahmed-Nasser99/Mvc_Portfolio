namespace Core.Models
{
    public class Address : BaseEntity
    {
        public string Street { get; set; }
        public string City { get; set; }
        public int Number { get; set; }
    }
}
