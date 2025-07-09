namespace Bingo.Models.Dto
{
    public class JoinGameDto
    {
        public string GamePassword { get; set; } = string.Empty;
        public Guid UserId { get; set; }
    }
}
