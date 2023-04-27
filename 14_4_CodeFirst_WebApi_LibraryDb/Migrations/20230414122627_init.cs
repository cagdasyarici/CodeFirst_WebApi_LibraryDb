using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _14_4_CodeFirst_WebApi_LibraryDb.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AuthorBooks",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorID = table.Column<int>(type: "int", nullable: false),
                    BookID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorBooks", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AuthorBooks_Authors_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "Authors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuthorBooks_Books_BookID",
                        column: x => x.BookID,
                        principalTable: "Books",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookID = table.Column<int>(type: "int", nullable: false),
                    TypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookTypes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BookTypes_Books_BookID",
                        column: x => x.BookID,
                        principalTable: "Books",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookTypes_Types_TypeID",
                        column: x => x.TypeID,
                        principalTable: "Types",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBooks_AuthorID",
                table: "AuthorBooks",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBooks_BookID",
                table: "AuthorBooks",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_BookTypes_BookID",
                table: "BookTypes",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_BookTypes_TypeID",
                table: "BookTypes",
                column: "TypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorBooks");

            migrationBuilder.DropTable(
                name: "BookTypes");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Types");
        }
    }
}
