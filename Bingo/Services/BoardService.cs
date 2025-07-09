using Bingo.Context;
using Bingo.Models.Entity;
using Bingo.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bingo.Services
{
    public class BoardService(BingoContext context) : IBoardService
    {
        private static Random rng = new();
        private int Size = 5;

        public async Task<Board> CreateBoard(List<string> texts, Game game, User user)
        {
            if (texts.Count != Size * Size)
            {
                throw new ArgumentException($"The number of texts must be exactly {Size * Size}.");
            }
            if (game is null)
            {
                throw new ArgumentNullException(nameof(game), "GameId cannot be null.");
            }
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user), "UserId cannot be null.");
            }

            var existingBoard = await context.Boards
                .FirstOrDefaultAsync(b => b.GameId == game.Id && b.UserId == user.Id);

            Board board = new Board
            {
                Id = Guid.NewGuid(),
                Size = Size,
                Fields = new List<Field>(),
                UserId = user.Id,
                User = user,
                GameId = game.Id,
                Game = game,
                isArchived = false
            };

            texts = ShuffleList(texts);
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    board.Fields.Add(new Field
                    {
                        Id = Guid.NewGuid(),
                        Text = texts[i * Size + j],
                        IsMarked = false,
                        Row = i,
                        Column = j,
                        BoardId = board.Id
                    });
                }
            }
            context.Boards.Add(board);
            await context.SaveChangesAsync();
            return board;
        }

        public async Task<Board?> GetBoardById(Guid id)
        {
            return await context.Boards
                .Include(b => b.Fields)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public Task<List<Board>> GetBoardsByGameId(Guid gameId)
        {
            List<Board> boards = context.Boards
                .Where(b => b.GameId == gameId && !b.isArchived)
                .ToList();
            return Task.FromResult(boards);
        }

        public Task<List<Board>> GetBoardsByUserId(Guid userId)
        {
            List<Board> boards = context.Boards
                .Where(b => b.UserId == userId && !b.isArchived)
                .ToList();
            return Task.FromResult(boards);
        }

        public Task<bool> IsWinning(Board board)
        {
            bool winning = false;
            if (board == null)
            {
                throw new ArgumentNullException(nameof(board), "BoardId cannot be null.");
            }

            //Column check
            for (int i = 0; i < Size; i++)
            {
                if (board.Fields.Where(f => f.Column == i && f.IsMarked).Count() == Size)
                {
                    winning = true;
                    break;
                }
            }

            //Row check
            if (!winning)
            {
                for (int i = 0; i < Size; i++)
                {
                    if (board.Fields.Where(f => f.Row == i && f.IsMarked).Count() == Size)
                    {
                        winning = true;
                        break;
                    }
                }
            }
            //Diagonal check
            if (!winning)
            {
                if (board.Fields.Where(f => f.Row == f.Column && f.IsMarked).Count() == Size)
                {
                    winning = true;
                }
            }

            return Task.FromResult(winning);
        }

        public async Task<Board> GetBoardByGameIdAndUserId(Guid gameId, Guid userId)
        {
            var board = await context.Boards
                .Include(b => b.Fields)
                .FirstOrDefaultAsync(b => b.GameId == gameId && b.UserId == userId && !b.isArchived);
            if (board is null)
            {
                throw new ArgumentException("No board found for the specified game and user.");
            }
            return board;
        }

        private List<string> ShuffleList(List<string> listofStrings)
        {
            int n = listofStrings.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                (listofStrings[k], listofStrings[n]) = (listofStrings[n], listofStrings[k]);
            }
            return listofStrings;
        }
    }
}