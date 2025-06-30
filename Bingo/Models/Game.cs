namespace Bingo.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Host { get; set; } = string.Empty;
        public bool IsOver { get; set; }
        public Board? Board { get; set; }
        public List<User>? Users { get; set; }
    }
}
