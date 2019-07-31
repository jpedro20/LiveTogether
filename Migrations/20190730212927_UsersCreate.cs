using LiveTogether.Utils.Security;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LiveTogether.Migrations
{
    public partial class UsersCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false).Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Name = table.Column<string>(nullable: false, maxLength: 150),
                    Username = table.Column<string>(nullable: false, maxLength: 30),
                    Email = table.Column<string>(nullable: false, maxLength: 75),
                    PasswordHash = table.Column<byte[]>(nullable: false),
                    PasswordSalt = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", u => u.Id);
                }
            );

            AuthSecurity.CreatePasswordHash("johndoepw", out byte[] pwdHash, out byte[] pwdSalt);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] {"Name", "Username", "Email", "PasswordHash", "PasswordSalt"},
                values: new object[] {"John Doe", "johndoe", "johndoe@email.com", pwdHash, pwdSalt}
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Users");
        }
    }
}
