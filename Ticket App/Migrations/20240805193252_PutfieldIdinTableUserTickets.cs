using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticket_App.Migrations
{
    /// <inheritdoc />
    public partial class PutfieldIdinTableUserTickets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTickets",
                table: "UserTickets");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "UserTickets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTickets",
                table: "UserTickets",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserTickets_TicketId",
                table: "UserTickets",
                column: "TicketId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTickets",
                table: "UserTickets");

            migrationBuilder.DropIndex(
                name: "IX_UserTickets_TicketId",
                table: "UserTickets");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserTickets");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTickets",
                table: "UserTickets",
                columns: new[] { "TicketId", "UsersId" });
        }
    }
}
