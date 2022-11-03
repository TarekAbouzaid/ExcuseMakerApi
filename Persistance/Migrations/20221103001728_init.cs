using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Excuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Excuses", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Excuses",
                columns: new[] { "Id", "Category", "Text" },
                values: new object[,]
                {
                    { 1, 0, "I promised my aunts that I would meet them this reunion." },
                    { 2, 0, "I got run over by a cyclist" },
                    { 3, 0, "I fell over in the shower and knocked myself out." },
                    { 4, 0, "Had a morning action rolling. It was too good to leave." },
                    { 5, 0, "My goldfish is ill. Have to take care of it." },
                    { 6, 1, "My fortune teller advised against going home." },
                    { 7, 1, "My plot to take over the presidency of the book club is thickening, and I must stay at the office to make sure everything is working out smoothly." },
                    { 8, 1, "I have to go to the post office to see if I am still wanted." },
                    { 9, 1, "I am currently working on my bucket list and, unfortunately, going home is not on my list." },
                    { 10, 1, "I need to plant my watermelon seeds in the office. Yes, I know it is the middle of the winter. I am starting ahead of the game this year!." },
                    { 11, 2, "I am trying to be less popular. Someone has got to do it!" },
                    { 12, 2, "I have lost my lucky rat's tail. Sorry, but I never go out without it!\r\n" },
                    { 13, 2, "I am being deported Friday, sorry I will not be able to make it." },
                    { 14, 2, "My socks are matching! This is a natural disaster, an emergency!. I can't leave now." },
                    { 15, 2, "I don't like to leave my comfort zone." }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Excuses");
        }
    }
}
