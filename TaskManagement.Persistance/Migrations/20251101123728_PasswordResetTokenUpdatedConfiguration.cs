using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class PasswordResetTokenUpdatedConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PasswordResetTokens_Token",
                table: "PasswordResetTokens",
                column: "Token",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PasswordResetTokens_Token",
                table: "PasswordResetTokens");
        }
    }
}
