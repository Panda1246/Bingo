namespace Bingo.Models.Entity
{
    public class Board
    {
        public Guid Id { get; set; }
        public int Size { get; set; }
        public bool isArchived { get; set; }
        public List<Field> Fields { get; set; }
        public Guid? UserId { get; set; } // <-- klucz obcy
        public User? User { get; set; }   // <-- nawigacja
        public Guid? GameId { get; set; } // <-- klucz obcy
        public Game? Game { get; set; }   // <-- nawigacja
    }
}
