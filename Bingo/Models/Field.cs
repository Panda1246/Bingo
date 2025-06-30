namespace Bingo.Models
{
    public class Field
    {
        public int Id { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public bool IsMarked { get; set; }
    }
}
