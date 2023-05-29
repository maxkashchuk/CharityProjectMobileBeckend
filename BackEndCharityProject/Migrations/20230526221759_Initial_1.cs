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
                name: "PostsHelp",
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
                    table.PrimaryKey("PK_PostsHelp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostsHelp_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostsVolunteer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Goal = table.Column<int>(type: "int", nullable: false),
                    Donated = table.Column<int>(type: "int", nullable: false),
                    Header = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lattitude = table.Column<double>(type: "float", nullable: true),
                    Longtitude = table.Column<double>(type: "float", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<double>(type: "float", nullable: false),
                    UserOriginId = table.Column<int>(type: "int", nullable: false),
                    UserVoteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_Users_UserOriginId",
                        column: x => x.UserOriginId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ratings_Users_UserVoteId",
                        column: x => x.UserVoteId,
                        principalTable: "Users",
                        principalColumn: "Id");
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
                        name: "FK_Images_PostsHelp_PostHelpsId",
                        column: x => x.PostHelpsId,
                        principalTable: "PostsHelp",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Images_PostsVolunteer_PostVolunteersId",
                        column: x => x.PostVolunteersId,
                        principalTable: "PostsVolunteer",
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
                        name: "FK_Tags_PostsHelp_PostHelpsId",
                        column: x => x.PostHelpsId,
                        principalTable: "PostsHelp",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tags_PostsVolunteer_PostVolunteersId",
                        column: x => x.PostVolunteersId,
                        principalTable: "PostsVolunteer",
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
                name: "IX_PostsHelp_Id",
                table: "PostsHelp",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostsHelp_UserId",
                table: "PostsHelp",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PostsVolunteer_Id",
                table: "PostsVolunteer",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostsVolunteer_UserId",
                table: "PostsVolunteer",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_Id",
                table: "Ratings",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserOriginId",
                table: "Ratings",
                column: "UserOriginId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserVoteId",
                table: "Ratings",
                column: "UserVoteId");

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
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "PostsHelp");

            migrationBuilder.DropTable(
                name: "PostsVolunteer");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
