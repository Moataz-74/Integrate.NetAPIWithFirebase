using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Integrate.NetAPIWithFirebase.Migrations
{
    /// <inheritdoc />
    public partial class testDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BabyTemperatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Temperature = table.Column<double>(type: "float", nullable: false),
                    IsNormal = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BabyTemperatures", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BabyTemperatures");
        }
    }
}
