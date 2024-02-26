using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Product.API.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Business");

            migrationBuilder.CreateTable(
                name: "PRODUCTS",
                schema: "Business",
                columns: table => new
                {
                    PRODUCT_ID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PRODUCT_NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PRODUCT_PRICE = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    TIME_CREATED = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CREATED_BY = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TIME_MODIFIED = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MODIFIED_BY = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCTS", x => x.PRODUCT_ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PRODUCTS",
                schema: "Business");
        }
    }
}
