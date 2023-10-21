using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VagasDoc.Migrations
{
    /// <inheritdoc />
    public partial class RelacionamentoUsuarioVaga : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Vagas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vagas_UsuarioId",
                table: "Vagas",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vagas_Usuarios_UsuarioId",
                table: "Vagas",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vagas_Usuarios_UsuarioId",
                table: "Vagas");

            migrationBuilder.DropIndex(
                name: "IX_Vagas_UsuarioId",
                table: "Vagas");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Vagas");
        }
    }
}
