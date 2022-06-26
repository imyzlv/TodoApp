using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todoapp.Migrations
{
    public partial class PublicTaskOption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PublicTask",
                schema: "Identity",
                table: "ToDoLists",
                type: "INTEGER",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicTask",
                schema: "Identity",
                table: "ToDoLists");
        }
    }
}
