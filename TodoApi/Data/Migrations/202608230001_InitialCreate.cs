using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoApi.Data.Migrations;

[Migration("202608230001_InitialCreate")]
public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "todos",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                IsComplete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_todos", todo => todo.Id);
            });

        migrationBuilder.InsertData(
            table: "todos",
            columns: new[] { "Id", "IsComplete", "Title" },
            values: new object[,]
            {
                { 1, false, "Comprar ingredientes para la cena" },
                { 2, false, "Revisar el correo pendiente" },
                { 3, false, "Preparar la reunion semanal" },
                { 4, false, "Actualizar la documentacion" },
                { 5, false, "Hacer una copia de seguridad" },
                { 6, false, "Planificar las tareas de manana" },
                { 7, false, "Llamar al proveedor" },
                { 8, false, "Organizar los archivos del proyecto" },
                { 9, false, "Leer las notas del equipo" },
                { 10, false, "Confirmar la proxima cita" }
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "todos");
    }
}