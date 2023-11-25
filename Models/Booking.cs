namespace Resturant.Models
{
    public class Booking
    {
        public Booking(int id, DateTime from, string name, string phone, int partySize, string description)
        {
            Id = id;
            From = from;
            Name = name;
            Phone = phone;
            PartySize = partySize;
            Description = description;
        }

        public Booking()
        {

        }

        public int Id { get; set; }

        public DateTime From { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public int PartySize { get; set; }

        public string Description { get; set; }
    }
}
