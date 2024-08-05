using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticket_App.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTickets_Users_UserId",
                table: "UserTickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTickets",
                table: "UserTickets");

            migrationBuilder.DropIndex(
                name: "IX_UserTickets_TicketId",
                table: "UserTickets");

            migrationBuilder.DropIndex(
                name: "IX_UserTickets_UserId",
                table: "UserTickets");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserTickets");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTickets",
                table: "UserTickets",
                columns: new[] { "TicketId", "UsersId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserTickets_UsersId",
                table: "UserTickets",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTickets_Users_UsersId",
                table: "UserTickets",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTickets_Users_UsersId",
                table: "UserTickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTickets",
                table: "UserTickets");

            migrationBuilder.DropIndex(
                name: "IX_UserTickets_UsersId",
                table: "UserTickets");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "UserTickets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTickets",
                table: "UserTickets",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTickets_TicketId",
                table: "UserTickets",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTickets_UserId",
                table: "UserTickets",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTickets_Users_UserId",
                table: "UserTickets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
