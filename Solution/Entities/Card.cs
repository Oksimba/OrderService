namespace Entities
{
    public class Card
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CardNumber { get; set; }

        public int CardBalance { get; set; }

        public User User { get; set; }
    }
   
}
