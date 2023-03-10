using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using Battleships.DAL.Configurations;
using Battleships.DAL.Entities;
using Battleships.Model;
using Battleships.Model.Helpers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Battleships.DAL
{
    public class GameResultContext : DbContext
    {
        public DbSet<GameResult> Results { get; set; }
        public DbSet<GameAction> Actions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // We use the exact string as using ConfigurationManager throws unexpected null reference error
            // This seems to be a known bug fixed in EF Core 7.0
            // To see more follow this thread: https://github.com/dotnet/efcore/issues/22205
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=battleships;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameResult>()
                .HasMany(result => result.Actions)
                .WithOne(act => act.GameResult);

            new GameActionConfiguration().Configure(modelBuilder.Entity<GameAction>());
        }
    }
}
