namespace Bingo.Models.Entity
{
    public class Game
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsOver { get; set; }
        public bool IsStarted { get; set; }

        public List<Board>? Boards { get; set; }
        public Board? Template { get; set; }
    }
}
