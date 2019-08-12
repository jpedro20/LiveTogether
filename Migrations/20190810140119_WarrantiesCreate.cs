using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LiveTogether.Migrations
{
    public partial class WarrantiesCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Warranties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false).Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Product = table.Column<string>(nullable: false, maxLength: 150),
                    Store = table.Column<string>(nullable: true, maxLength: 150),
                    StoreAddress = table.Column<string>(nullable: true, maxLength: 200),
                    PhurcaseDate = table.Column<DateTime>(nullable: true),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    DocumentPath = table.Column<string>(nullable: true, maxLength: 200)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warranties", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Warranties");
        }
    }
}
