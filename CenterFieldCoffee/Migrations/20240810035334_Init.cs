using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CenterFieldCoffee.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Store",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    opening_hour = table.Column<short>(type: "INTEGER", nullable: false),
                    opening_minute = table.Column<short>(type: "INTEGER", nullable: false),
                    closing_hour = table.Column<short>(type: "INTEGER", nullable: false),
                    closing_minute = table.Column<short>(type: "INTEGER", nullable: false),
                    create_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    update_date = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Store", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Store",
                columns: new[] { "Id", "Name", "closing_hour", "closing_minute", "create_date", "opening_hour", "opening_minute", "update_date" },
                values: new object[,]
                {
                    { new Guid("9b55a5ab-44f5-4e4e-96cf-a2e9e81a2c08"), "Store 2", (short)21, (short)0, new DateTime(2024, 8, 10, 3, 53, 33, 32, DateTimeKind.Utc).AddTicks(3379), (short)6, (short)30, null },
                    { new Guid("a64181fd-fc94-4189-9dc1-e7f891106470"), "Store 1", (short)18, (short)0, new DateTime(2024, 8, 10, 3, 53, 33, 32, DateTimeKind.Utc).AddTicks(3365), (short)6, (short)30, null },
                    { new Guid("f8c94d04-f86f-4f88-822a-84bdfd197055"), "Store 4", (short)20, (short)0, new DateTime(2024, 8, 10, 3, 53, 33, 32, DateTimeKind.Utc).AddTicks(3386), (short)16, (short)30, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Store");
        }
    }
}
