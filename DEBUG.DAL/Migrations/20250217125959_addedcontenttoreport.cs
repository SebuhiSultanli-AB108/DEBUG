using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DEBUG.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addedcontenttoreport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "ReportItems",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "ReportItems");
        }
    }
}
