using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResponseMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReasonCode = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResponseMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShortenedUrls",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApiHostUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GivenUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GivenUrlSchema = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GivenUrlHost = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GivenUrlRouteAndQuery = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCustomHash = table.Column<bool>(type: "bit", nullable: false),
                    Hash = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false, collation: "Latin1_General_CS_AS")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShortenedUrls", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResponseMessages");

            migrationBuilder.DropTable(
                name: "ShortenedUrls");
        }
    }
}
