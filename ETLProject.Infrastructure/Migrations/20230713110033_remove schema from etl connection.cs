using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETLProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removeschemafrometlconnection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Schema",
                table: "EtlConnections");

            migrationBuilder.AlterColumn<string>(
                name: "DataSourceType",
                table: "EtlConnections",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DataSourceType",
                table: "EtlConnections",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Schema",
                table: "EtlConnections",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
