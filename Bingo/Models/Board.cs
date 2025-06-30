
namespace Bingo.Models
{
    public class Board
    {
        public int Id { get; set; }
        public int Size { get; set; }
        public List<Field>? Fields { get; set; }
        public User? User { get; set; }
    }
}
