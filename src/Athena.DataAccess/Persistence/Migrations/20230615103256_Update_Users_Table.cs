using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Athena.DataAccess.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Update_Users_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "LastModificationTime",
                table: "Users",
                newName: "LastLogin");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Users",
                newName: "IsActivated");

            migrationBuilder.RenameColumn(
                name: "CreationTime",
                table: "Users",
                newName: "CreatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastLogin",
                table: "Users",
                newName: "LastModificationTime");

            migrationBuilder.RenameColumn(
                name: "IsActivated",
                table: "Users",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Users",
                newName: "CreationTime");

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "Users",
                type: "bigint",
                nullable: true);
        }
    }
}
