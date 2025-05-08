using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class initial4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FoodId",
                table: "OrderItems",
                newName: "OrderFoodId");

            migrationBuilder.AddColumn<bool>(
                name: "isCancelled",
                table: "OrderItems",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isCancelled",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "OrderFoodId",
                table: "OrderItems",
                newName: "FoodId");
        }
    }
}
