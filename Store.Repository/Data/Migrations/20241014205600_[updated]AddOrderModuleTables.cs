using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedAddOrderModuleTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrederIems_Orders_OrderOId",
                table: "OrederIems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrederIems",
                table: "OrederIems");

            migrationBuilder.RenameTable(
                name: "OrederIems",
                newName: "OrederItems");

            migrationBuilder.RenameIndex(
                name: "IX_OrederIems_OrderOId",
                table: "OrederItems",
                newName: "IX_OrederItems_OrderOId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrederItems",
                table: "OrederItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrederItems_Orders_OrderOId",
                table: "OrederItems",
                column: "OrderOId",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrederItems_Orders_OrderOId",
                table: "OrederItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrederItems",
                table: "OrederItems");

            migrationBuilder.RenameTable(
                name: "OrederItems",
                newName: "OrederIems");

            migrationBuilder.RenameIndex(
                name: "IX_OrederItems_OrderOId",
                table: "OrederIems",
                newName: "IX_OrederIems_OrderOId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrederIems",
                table: "OrederIems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrederIems_Orders_OrderOId",
                table: "OrederIems",
                column: "OrderOId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
