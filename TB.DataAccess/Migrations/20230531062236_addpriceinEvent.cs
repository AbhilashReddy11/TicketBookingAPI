using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TB.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addpriceinEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TicketPrice",
                table: "Events",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketPrice",
                table: "Events");
        }
    }
}
