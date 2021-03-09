using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MailSender.Migrations
{
    public partial class MailSender : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Person_Company = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailAddresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MessagePatterns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessagePatterns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MessageSendContainers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SmtpServerUse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PortUse = table.Column<int>(type: "int", nullable: false),
                    SSLUse = table.Column<bool>(type: "bit", nullable: false),
                    SmtpAccountEmailUse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SmtpAccountPasswordUse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SmtpAccountPerson_CompanyUse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddressesTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SendDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageSendContainers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SmtpServers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SmtpServ = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Port = table.Column<int>(type: "int", nullable: false),
                    UseSSL = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmtpServers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SmtpAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Person_Company = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Smtp_ServerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmtpAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SmtpAccounts_SmtpServers_Smtp_ServerId",
                        column: x => x.Smtp_ServerId,
                        principalTable: "SmtpServers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SmtpAccounts_Smtp_ServerId",
                table: "SmtpAccounts",
                column: "Smtp_ServerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailAddresses");

            migrationBuilder.DropTable(
                name: "MessagePatterns");

            migrationBuilder.DropTable(
                name: "MessageSendContainers");

            migrationBuilder.DropTable(
                name: "SmtpAccounts");

            migrationBuilder.DropTable(
                name: "SmtpServers");
        }
    }
}
