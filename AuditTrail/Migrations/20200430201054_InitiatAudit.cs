using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AuditTrailComponent.Migrations
{
    public partial class InitiatAudit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "audit");

            migrationBuilder.CreateTable(
                name: "Action",
                schema: "audit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    SendNotification = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Action", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActionUserGroup",
                schema: "audit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Audit_ActionCode = table.Column<string>(nullable: true),
                    EntityName = table.Column<string>(maxLength: 20, nullable: true),
                    ToEmails = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    From = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionUserGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditTrail",
                schema: "audit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(maxLength: 30, nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Audit_ActionCode = table.Column<string>(nullable: true),
                    EntityName = table.Column<string>(maxLength: 50, nullable: true),
                    ActionData = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditTrail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ErrorLog",
                schema: "audit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    StackTrace = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                schema: "audit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Audit_ActionCode = table.Column<string>(nullable: true),
                    ToEmails = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    From = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    Sent = table.Column<bool>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationLog",
                schema: "audit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationId = table.Column<int>(nullable: false),
                    TryDate = table.Column<DateTime>(nullable: false),
                    SendSucceeded = table.Column<bool>(nullable: false),
                    ErrorMessage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationLog", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Action",
                schema: "audit");

            migrationBuilder.DropTable(
                name: "ActionUserGroup",
                schema: "audit");

            migrationBuilder.DropTable(
                name: "AuditTrail",
                schema: "audit");

            migrationBuilder.DropTable(
                name: "ErrorLog",
                schema: "audit");

            migrationBuilder.DropTable(
                name: "Notification",
                schema: "audit");

            migrationBuilder.DropTable(
                name: "NotificationLog",
                schema: "audit");
        }
    }
}
