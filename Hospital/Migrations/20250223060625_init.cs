using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "medications",
                columns: table => new
                {
                    medication_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    medication_description = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    medication_cost = table.Column<decimal>(type: "decimal(9,2)", nullable: true),
                    package_size = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    strength = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    sig = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    units_used_ytd = table.Column<int>(type: "int", nullable: true),
                    last_prescribed_date = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__medicati__DD94789BE1A77686", x => x.medication_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "medications");
        }
    }
}
