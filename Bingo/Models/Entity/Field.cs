using Bingo.Models.Entity;

namespace Bingo.Models.Entity;
public class Field
{
    public Guid Id { get; set; }
    public int Row { get; set; }
    public int Column { get; set; }
    public string Text { get; set; } = string.Empty;
    public bool IsMarked { get; set; }
    public Guid BoardId { get; set; } // <-- to jest klucz obcy
    public Board Board { get; set; }  // <-- to jest nawigacja
}
