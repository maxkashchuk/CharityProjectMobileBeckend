using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEndCharityProject.Migrations
{
    /// <inheritdoc />
    public partial class Initial_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<bool>(type: "bit", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostHelp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Money = table.Column<int>(type: "int", nullable: true),
                    Header = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lattitude = table.Column<double>(type: "float", nullable: true),
                    Longtitude = table.Column<double>(type: "float", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostHelp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostHelp_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostVolunteer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Goal = table.Column<int>(type: "int", nullable: false),
                    Donated = table.Column<int>(type: "int", nullable: false),
                    Header = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lattitude = table.Column<double>(type: "float", nullable: false),
                    Longtitude = table.Column<double>(type: "float", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostVolunteer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostVolunteer_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostHelpsId = table.Column<int>(type: "int", nullable: true),
                    PostVolunteersId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_PostHelp_PostHelpsId",
                        column: x => x.PostHelpsId,
                        principalTable: "PostHelp",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Images_PostVolunteer_PostVolunteersId",
                        column: x => x.PostVolunteersId,
                        principalTable: "PostVolunteer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostHelpsId = table.Column<int>(type: "int", nullable: true),
                    PostVolunteersId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tags_PostHelp_PostHelpsId",
                        column: x => x.PostHelpsId,
                        principalTable: "PostHelp",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tags_PostVolunteer_PostVolunteersId",
                        column: x => x.PostVolunteersId,
                        principalTable: "PostVolunteer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_Id",
                table: "Images",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_PostHelpsId",
                table: "Images",
                column: "PostHelpsId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_PostVolunteersId",
                table: "Images",
                column: "PostVolunteersId");

            migrationBuilder.CreateIndex(
                name: "IX_PostHelp_Id",
                table: "PostHelp",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostHelp_UserId",
                table: "PostHelp",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PostVolunteer_Id",
                table: "PostVolunteer",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostVolunteer_UserId",
                table: "PostVolunteer",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Id",
                table: "Tags",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_PostHelpsId",
                table: "Tags",
                column: "PostHelpsId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_PostVolunteersId",
                table: "Tags",
                column: "PostVolunteersId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Id",
                table: "Users",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "PostHelp");

            migrationBuilder.DropTable(
                name: "PostVolunteer");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
