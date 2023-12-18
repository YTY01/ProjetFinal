using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistryBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddTaxable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Taxable",
                table: "Departements",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Taxable",
                table: "Departements");
        }
    }
}
