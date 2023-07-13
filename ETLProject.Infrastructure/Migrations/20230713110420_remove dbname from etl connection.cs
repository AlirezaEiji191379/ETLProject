﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETLProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removedbnamefrometlconnection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DatabaseName",
                table: "EtlConnections");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DatabaseName",
                table: "EtlConnections",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
