using Bingo.Models.Dto;
using Bingo.Models.Entity;

namespace Bingo.Services
{
    public interface IGameService
    {
        Task CreateGame(CreateGameRequestDto request);
        Task<Board> JoinGame(JoinGameDto request);
        Task LeaveGame(LeaveGameDto request);
    }
}
