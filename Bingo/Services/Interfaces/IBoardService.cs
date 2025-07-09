using Bingo.Models.Entity;

namespace Bingo.Services.Interfaces
{
    public interface IBoardService
    {
        Task<Board> CreateBoard(List<string> texts, Game game, User user);
        Task<Board?> GetBoardById(Guid id);
        Task<List<Board>> GetBoardsByUserId(Guid userId);
        Task<List<Board>> GetBoardsByGameId(Guid gameId);
        Task<Board> GetBoardByGameIdAndUserId(Guid gameId, Guid userId);
        Task<bool> IsWinning(Board board);
    }
}
