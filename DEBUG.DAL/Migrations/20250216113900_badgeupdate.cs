using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DEBUG.DAL.Migrations
{
    /// <inheritdoc />
    public partial class badgeupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnswersCount",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Badges",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CommentsCount",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuestionCount",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnswersCount",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Badges",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CommentsCount",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "QuestionCount",
                table: "AspNetUsers");
        }
    }
}
