using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TB.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedStatusinEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "status",
                table: "Events",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "Events");
        }
    }
}
