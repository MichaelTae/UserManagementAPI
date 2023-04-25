using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class ticketfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateBooked",
                table: "Tickets",
                newName: "DateCreated");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateBooked",
                table: "UserTickets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdated",
                table: "Tickets",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateBooked",
                table: "UserTickets");

            migrationBuilder.DropColumn(
                name: "DateUpdated",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "Tickets",
                newName: "DateBooked");
        }
    }
}
