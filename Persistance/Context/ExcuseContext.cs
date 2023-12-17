using DTO;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Persistance.Context
{
    public class ExcuseContext : DbContext
    {
        public ExcuseContext(DbContextOptions<ExcuseContext> options) : base(options)
        {
            // Check if the 'Excuses' table exists
            if (!TableExists("Excuses"))
            {
                Database.Migrate();
            }
        }

        private bool TableExists(string tableName)
        {
            var connection = Database.GetDbConnection();
            using var command = connection.CreateCommand();
            command.CommandText =
                $"SELECT CASE WHEN OBJECT_ID(N'dbo.{tableName}', 'U') IS NOT NULL THEN 1 ELSE 0 END";
            connection.Open();
            var exists = (int)(command.ExecuteScalar() ?? throw new InvalidOperationException());
            connection.Close();
            return exists == 1;
        }

        public DbSet<Excuse?> Excuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Office
            modelBuilder.Entity<Excuse>().HasData(new Excuse
            {
                Id = 1, Category = (ExcuseCategory)0, Text = "I promised my aunts that I would meet them this reunion."
            });
            modelBuilder.Entity<Excuse>().HasData(new Excuse
                { Id = 2, Category = (ExcuseCategory)0, Text = "I got run over by a cyclist" });
            modelBuilder.Entity<Excuse>().HasData(new Excuse
                { Id = 3, Category = (ExcuseCategory)0, Text = "I fell over in the shower and knocked myself out." });
            modelBuilder.Entity<Excuse>().HasData(new Excuse
            {
                Id = 4, Category = (ExcuseCategory)0, Text = "Had a morning action rolling. It was too good to leave."
            });
            modelBuilder.Entity<Excuse>().HasData(new Excuse
                { Id = 5, Category = (ExcuseCategory)0, Text = "My goldfish is ill. Have to take care of it." });

            //Partner
            modelBuilder.Entity<Excuse>().HasData(new Excuse
                { Id = 6, Category = (ExcuseCategory)1, Text = "My fortune teller advised against going home." });
            modelBuilder.Entity<Excuse>().HasData(new Excuse
            {
                Id = 7, Category = (ExcuseCategory)1,
                Text =
                    "My plot to take over the presidency of the book club is thickening, and I must stay at the office to make sure everything is working out smoothly."
            });
            modelBuilder.Entity<Excuse>().HasData(new Excuse
            {
                Id = 8, Category = (ExcuseCategory)1,
                Text = "I have to go to the post office to see if I am still wanted."
            });
            modelBuilder.Entity<Excuse>().HasData(new Excuse
            {
                Id = 9, Category = (ExcuseCategory)1,
                Text = "I am currently working on my bucket list and, unfortunately, going home is not on my list."
            });
            modelBuilder.Entity<Excuse>().HasData(new Excuse
            {
                Id = 10, Category = (ExcuseCategory)1,
                Text =
                    "I need to plant my watermelon seeds in the office. Yes, I know it is the middle of the winter. I am starting ahead of the game this year!."
            });

            //Friends
            modelBuilder.Entity<Excuse>().HasData(new Excuse
            {
                Id = 11, Category = (ExcuseCategory)2,
                Text = "I am trying to be less popular. Someone has got to do it!"
            });
            modelBuilder.Entity<Excuse>().HasData(new Excuse
            {
                Id = 12, Category = (ExcuseCategory)2,
                Text = "I have lost my lucky rat's tail. Sorry, but I never go out without it!\r\n"
            });
            modelBuilder.Entity<Excuse>().HasData(new Excuse
            {
                Id = 13, Category = (ExcuseCategory)2,
                Text = "I am being deported Friday, sorry I will not be able to make it."
            });
            modelBuilder.Entity<Excuse>().HasData(new Excuse
            {
                Id = 14, Category = (ExcuseCategory)2,
                Text = "My socks are matching! This is a natural disaster, an emergency!. I can't leave now."
            });
            modelBuilder.Entity<Excuse>().HasData(new Excuse
                { Id = 15, Category = (ExcuseCategory)2, Text = "I don't like to leave my comfort zone." });
        }
    }
}