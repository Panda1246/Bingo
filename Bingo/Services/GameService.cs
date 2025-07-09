using Bingo.Context;
using Bingo.Models.Dto;
using Bingo.Models.Entity;
using System.Security.Cryptography;
using Bingo.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bingo.Services
{
    public class GameService(BingoContext context, IBoardService boardService) : IGameService
    {
        public Task CreateGame(CreateGameRequestDto request)
        {
            String UniquePassword = GenerateGamePassword();
            while (context.Games.Any(g => g.Password == UniquePassword))
            {
                UniquePassword = GenerateGamePassword();
            }
            Game game = new Game
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Password = GenerateGamePassword(),
                IsOver = false,
                IsStarted = false,
                Boards = new List<Board>(),
                Template = boardService.CreateBoard(request.BoardFields, null, null).Result,
            };
            context.Games.Add(game);
            return context.SaveChangesAsync();
        }

        public async Task<Board> JoinGame(JoinGameDto request)
        {
            Game game = await context.Games
                .Include(g => g.Template)
                .FirstOrDefaultAsync(g =>g.Password == request.GamePassword);
            if (game == null)
            {
                throw new ArgumentException("GameId not found with the provided password.");
            }

            if (game.IsOver || game.IsStarted)
            {
                throw new InvalidOperationException("Cannot join a game that is already over or has started.");
            }

            User user = await context.Users.FindAsync(request.UserId);
            if (user == null)
            {
                throw new ArgumentException("UserId not found with the provided ID.");
            }


                // Sprawdź, czy użytkownik już ma planszę w tej grze
                var existingBoard = await context.Boards
                .FirstOrDefaultAsync(b => b.GameId.Id == game.Id && b.UserId.Id == user.Id);

            if (existingBoard != null)
            {
                return existingBoard;
            }

            // Stwórz nową planszę na podstawie szablonu gry
            var template = game.Template;
            var newBoard = new Board
            {
                Id = Guid.NewGuid(),
                Size = template.Size,
                isArchived = false,
                UserId = user,
                GameId = game,
                Fields = template.Fields
                    .Select(f => new Field
                    {
                        Id = Guid.NewGuid(),
                        Row = f.Row,
                        Column = f.Column,
                        Text = f.Text,
                        IsMarked = false
                    })
                    .ToList()
            };

            context.Boards.Add(newBoard);
            await context.SaveChangesAsync();
            return newBoard;
        }

        public Task LeaveGame(LeaveGameDto request)
        {
            Game game = context.Games
                .FirstOrDefault(g => g.Id == request.GameId);
            if (game == null)
            {
                throw new ArgumentException("GameId not found with the provided ID.");
            }
            User user = context.Users
                .FirstOrDefault(u => u.Id == request.UserId);
            if (user == null)
            {
                throw new ArgumentException("UserId not found with the provided ID.");
            }
            var board = context.Boards
                .FirstOrDefault(b => b.GameId.Id == game.Id && b.UserId.Id == user.Id);
            if (board == null)
                {
                throw new ArgumentException("BoardId not found for the user in this game.");
            }
            board.isArchived = true;
            context.Boards.Update(board);
            return context.SaveChangesAsync();
        }
        private string GenerateGamePassword()
        {
            var randomNumber = new byte[8];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
