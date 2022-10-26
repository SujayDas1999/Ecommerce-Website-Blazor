using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecom.Server.Migrations
{
    public partial class addedseeddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "ImageUrl", "Price", "Title" },
                values: new object[] { 1, "Horrid Henry is the first book of the Horrid Henry series. It was published in 1994 and written by Francesca Simon and illustrated by Tony Ross. The book is a collection of short stories about the same characters, along the lines of the Just William books.", "https://upload.wikimedia.org/wikipedia/en/8/81/Horrid_Henry_1_%28first_paperback_edition%29.jpg", 9.99m, "Horrid Henry" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "ImageUrl", "Price", "Title" },
                values: new object[] { 2, "Harry Potter and the Philosopher's Stone is a 1997 fantasy novel written by British author J. K. Rowling. The first novel in the Harry Potter series and Rowling's debut novel, it follows Harry Potter, a young wizard who discovers his magical heritage on his eleventh birthday, when he receives a letter of acceptance to Hogwarts School of Witchcraft and Wizardry. Harry makes close friends and a few enemies during his first year at the school and with the help of his friends, he faces an attempted comeback by the dark wizard Lord Voldemort, who killed Harry's parents, but failed to kill Harry when he was just 15 months old.", "https://upload.wikimedia.org/wikipedia/en/6/6b/Harry_Potter_and_the_Philosopher%27s_Stone_Book_Cover.jpg", 12.99m, "Harry Potter and the Philosopher's Stone" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "ImageUrl", "Price", "Title" },
                values: new object[] { 3, "The Lord of the Rings is an epic high-fantasy novel[a] by English author and scholar J. R. R. Tolkien. Set in Middle-earth, intended to be Earth at some time in the distant past, the story began as a sequel to Tolkien's 1937 children's book The Hobbit, but eventually developed into a much larger work. Written in stages between 1937 and 1949, The Lord of the Rings is one of the best-selling books ever written, with over 150 million copies sold.", "https://upload.wikimedia.org/wikipedia/en/e/e9/First_Single_Volume_Edition_of_The_Lord_of_the_Rings.gif", 7.99m, "The Lord of the Rings" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
