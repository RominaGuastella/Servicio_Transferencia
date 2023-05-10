using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transferencias.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateTransferenciaDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transferencias",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    resultado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cuilOriginante = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cuilDestinatario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cbuOrigen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cbuDestino = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    importe = table.Column<double>(type: "float", nullable: false),
                    concepto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transferencias", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transferencias");
        }
    }
}
