using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEndCharityProject.Migrations
{
    /// <inheritdoc />
    public partial class Initial_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_PostsVolunteer_PostVolunteersId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_PostsVolunteer_PostVolunteersId",
                table: "Tags");

            migrationBuilder.DropTable(
                name: "PostsVolunteer");

            migrationBuilder.DropIndex(
                name: "IX_Tags_PostVolunteersId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Images_PostVolunteersId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "PostVolunteersId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "PostVolunteersId",
                table: "Images");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostVolunteersId",
                table: "Tags",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PostVolunteersId",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PostsVolunteer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Donated = table.Column<int>(type: "int", nullable: false),
                    Goal = table.Column<int>(type: "int", nullable: false),
                    Header = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lattitude = table.Column<double>(type: "float", nullable: true),
                    Longtitude = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostsVolunteer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostsVolunteer_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tags_PostVolunteersId",
                table: "Tags",
                column: "PostVolunteersId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_PostVolunteersId",
                table: "Images",
                column: "PostVolunteersId");

            migrationBuilder.CreateIndex(
                name: "IX_PostsVolunteer_Id",
                table: "PostsVolunteer",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostsVolunteer_UserId",
                table: "PostsVolunteer",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_PostsVolunteer_PostVolunteersId",
                table: "Images",
                column: "PostVolunteersId",
                principalTable: "PostsVolunteer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_PostsVolunteer_PostVolunteersId",
                table: "Tags",
                column: "PostVolunteersId",
                principalTable: "PostsVolunteer",
                principalColumn: "Id");
        }
    }
}
