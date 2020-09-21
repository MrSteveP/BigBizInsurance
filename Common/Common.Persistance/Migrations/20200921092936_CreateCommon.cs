using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Common.Persistance.Migrations
{
    public partial class CreateCommon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "common");

            migrationBuilder.CreateTable(
                name: "AttachmentTypes",
                schema: "common",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AllowedFilesExtension = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    ImageMaxHeight = table.Column<int>(nullable: true),
                    ImageMaxWidth = table.Column<int>(nullable: true),
                    IsImage = table.Column<bool>(nullable: false),
                    IsMandatory = table.Column<bool>(nullable: false),
                    MaxSizeInMegabytes = table.Column<int>(nullable: false),
                    NameAr = table.Column<string>(nullable: true),
                    NameEn = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttachmentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemSettings",
                schema: "common",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<string>(nullable: true),
                    Key = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    ValueType = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    GroupName = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsSticky = table.Column<bool>(nullable: false),
                    Secure = table.Column<bool>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Desc_En = table.Column<string>(nullable: true),
                    Desc_Ar = table.Column<string>(nullable: true),
                    IsManaged = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attachments",
                schema: "common",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(nullable: true),
                    Extention = table.Column<string>(nullable: true),
                    FilePath = table.Column<string>(nullable: true),
                    ContentType = table.Column<string>(nullable: true),
                    TitleAr = table.Column<string>(nullable: true),
                    TitleEn = table.Column<string>(nullable: true),
                    DescriptionAr = table.Column<string>(nullable: true),
                    DescriptionEn = table.Column<string>(nullable: true),
                    ItemOrder = table.Column<int>(nullable: false),
                    AttachmentTypeId = table.Column<int>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachments_AttachmentTypes_AttachmentTypeId",
                        column: x => x.AttachmentTypeId,
                        principalSchema: "common",
                        principalTable: "AttachmentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "common",
                table: "AttachmentTypes",
                columns: new[] { "Id", "AllowedFilesExtension", "Code", "CreatedBy", "CreatedOn", "ImageMaxHeight", "ImageMaxWidth", "IsImage", "IsMandatory", "MaxSizeInMegabytes", "NameAr", "NameEn", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, "pdf,jpg,png", "AvatarImage", null, null, null, null, true, false, 2, null, "Personal image", null, null },
                    { 2, "mp4,vlc,avi,wmv", "Video", null, null, null, null, false, false, 1024, null, "Video File", null, null },
                    { 3, "doc,docx,pdf", "TextFile", null, null, null, null, false, false, 20, null, "Word or Pdf file", null, null }
                });

            migrationBuilder.InsertData(
                schema: "common",
                table: "SystemSettings",
                columns: new[] { "Id", "ApplicationId", "CreatedBy", "CreatedOn", "Desc_Ar", "Desc_En", "GroupName", "IsActive", "IsManaged", "IsSticky", "Key", "Secure", "UpdatedBy", "UpdatedOn", "Value", "ValueType" },
                values: new object[,]
                {
                    { 13, "Smtp", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Smtp Enable SSL.", null, true, false, false, "SmtpEnableSSL", false, null, null, "False", null },
                    { 12, "Smtp", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Smtp Port.", null, true, false, false, "SmtpPort", false, null, null, "0", null },
                    { 11, "Smtp", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Is Smtp Authenticated.", null, true, false, false, "IsSmtpAuthenticated", false, null, null, "False", null },
                    { 10, "Smtp", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Smtp Password.", null, true, false, false, "SmtpPassword", false, null, null, "", null },
                    { 9, "Smtp", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Smtp UserName.", null, true, false, false, "SmtpUserName", false, null, null, "", null },
                    { 8, "Smtp", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Smtp Server.", null, true, false, false, "SmtpServer", false, null, null, "", null },
                    { 6, "AuthenticationThrottling", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Maximum number of incorrect login attempts before lockout.", null, true, false, false, "MaximumNumberOfAttempts", false, null, null, "3", null },
                    { 14, "Attachment", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Attachment folder path.", null, true, false, false, "AttachmentPath", false, null, null, "C:\\AppFiles", null },
                    { 5, "AuthenticationThrottling", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Number of minutes to lock the user out for after specified maximum numbers.", null, true, false, false, "LockoutTime", false, null, null, "5", null },
                    { 4, "AuthenticationThrottling", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Should the system throttle authentication attempts?", null, true, false, false, "AllowThrottleAuthentication", false, null, null, "True", null },
                    { 3, "AuthenticationGeneral", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Should user confirm his account ?", null, true, false, false, "RequireConfirmedAccount", false, null, null, "False", null },
                    { 2, "AuthenticationGeneral", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Enable/Disable forgot password feature", null, true, false, false, "AllowForgotPassword", false, null, null, "True", null },
                    { 1, "AuthenticationGeneral", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Should 'Remember Me' checkbox be displayed on login form?", null, true, false, false, "AllowRememberMe", false, null, null, "True", null },
                    { 7, "General", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Default culture.", null, true, false, false, "DefaultCulture", false, null, null, "en-US", null },
                    { 15, "Attachment", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "should system save files in database.", null, true, false, false, "SaveFilesToDatabase", false, null, null, "False", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_AttachmentTypeId",
                schema: "common",
                table: "Attachments",
                column: "AttachmentTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attachments",
                schema: "common");

            migrationBuilder.DropTable(
                name: "SystemSettings",
                schema: "common");

            migrationBuilder.DropTable(
                name: "AttachmentTypes",
                schema: "common");
        }
    }
}
