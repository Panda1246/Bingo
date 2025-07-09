using Bingo.Models.Entity;
using Microsoft.EntityFrameworkCore;


namespace Bingo.Context
{
    public class BingoContext(DbContextOptions<BingoContext> options) : DbContext(options)
    {
        public DbSet<Board> Boards { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Board>()
                .HasMany(b => b.Fields)
                .WithOne(f => f.Board)
                .HasForeignKey(f => f.BoardId);

            modelBuilder.Entity<Game>()
                .HasMany(g => g.Boards)
                .WithOne(b => b.Game)
                .HasForeignKey(b => b.GameId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Boards)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId);
        }
    }
}