using Common.Domain.Module;
using Microsoft.EntityFrameworkCore;

namespace Common.Persistance
{
    public static class SeedData
    {
        public static ModelBuilder SeedSystemSettings(this ModelBuilder modelBuilder)
        {
            #region AuthenticationGeneral Application
            modelBuilder.Entity<SystemSetting>().HasData(new SystemSetting
            {
                Id = 1,
                ApplicationId = CommonStaticValues.SystemSettingsData.AuthenticationGeneralApplication.ApplicationId,
                Key = CommonStaticValues.SystemSettingsData.AuthenticationGeneralApplication.KeysEnum.AllowRememberMe.ToString(),
                Value = CommonStaticValues.SystemSettingsData.AuthenticationGeneralApplication.Values[CommonStaticValues.SystemSettingsData.AuthenticationGeneralApplication.KeysEnum.AllowRememberMe],
                IsActive = true,
                Desc_En = "Should 'Remember Me' checkbox be displayed on login form?"
            });
            modelBuilder.Entity<SystemSetting>().HasData(new SystemSetting
            {
                Id = 2,
                ApplicationId = CommonStaticValues.SystemSettingsData.AuthenticationGeneralApplication.ApplicationId,
                Key = CommonStaticValues.SystemSettingsData.AuthenticationGeneralApplication.KeysEnum.AllowForgotPassword.ToString(),
                Value = CommonStaticValues.SystemSettingsData.AuthenticationGeneralApplication.Values[CommonStaticValues.SystemSettingsData.AuthenticationGeneralApplication.KeysEnum.AllowForgotPassword],
                IsActive = true,
                Desc_En = "Enable/Disable forgot password feature"
            });
            modelBuilder.Entity<SystemSetting>().HasData(new SystemSetting
            {
                Id = 3,
                ApplicationId = CommonStaticValues.SystemSettingsData.AuthenticationGeneralApplication.ApplicationId,
                Key = CommonStaticValues.SystemSettingsData.AuthenticationGeneralApplication.KeysEnum.RequireConfirmedAccount.ToString(),
                Value = CommonStaticValues.SystemSettingsData.AuthenticationGeneralApplication.Values[CommonStaticValues.SystemSettingsData.AuthenticationGeneralApplication.KeysEnum.RequireConfirmedAccount],
                IsActive = true,
                Desc_En = "Should user confirm his account ?"
            });
            #endregion

            #region AuthenticationThrottling Application
            modelBuilder.Entity<SystemSetting>().HasData(new SystemSetting
            {
                Id = 4,
                ApplicationId = CommonStaticValues.SystemSettingsData.AuthenticationThrottlingApplication.ApplicationId,
                Key = CommonStaticValues.SystemSettingsData.AuthenticationThrottlingApplication.KeysEnum.AllowThrottleAuthentication.ToString(),
                Value = CommonStaticValues.SystemSettingsData.AuthenticationThrottlingApplication.Values[CommonStaticValues.SystemSettingsData.AuthenticationThrottlingApplication.KeysEnum.AllowThrottleAuthentication],
                IsActive = true,
                Desc_En = "Should the system throttle authentication attempts?"
            });
            modelBuilder.Entity<SystemSetting>().HasData(new SystemSetting
            {
                Id = 5,
                ApplicationId = CommonStaticValues.SystemSettingsData.AuthenticationThrottlingApplication.ApplicationId,
                Key = CommonStaticValues.SystemSettingsData.AuthenticationThrottlingApplication.KeysEnum.LockoutTime.ToString(),
                Value = CommonStaticValues.SystemSettingsData.AuthenticationThrottlingApplication.Values[CommonStaticValues.SystemSettingsData.AuthenticationThrottlingApplication.KeysEnum.LockoutTime],
                IsActive = true,
                Desc_En = "Number of minutes to lock the user out for after specified maximum numbers."
            });
            modelBuilder.Entity<SystemSetting>().HasData(new SystemSetting
            {
                Id = 6,
                ApplicationId = CommonStaticValues.SystemSettingsData.AuthenticationThrottlingApplication.ApplicationId,
                Key = CommonStaticValues.SystemSettingsData.AuthenticationThrottlingApplication.KeysEnum.MaximumNumberOfAttempts.ToString(),
                Value = CommonStaticValues.SystemSettingsData.AuthenticationThrottlingApplication.Values[CommonStaticValues.SystemSettingsData.AuthenticationThrottlingApplication.KeysEnum.MaximumNumberOfAttempts],
                IsActive = true,
                Desc_En = "Maximum number of incorrect login attempts before lockout."
            });
            #endregion

            #region General Application
            modelBuilder.Entity<SystemSetting>().HasData(new SystemSetting
            {
                Id = 7,
                ApplicationId = CommonStaticValues.SystemSettingsData.GeneralApplication.ApplicationId,
                Key = CommonStaticValues.SystemSettingsData.GeneralApplication.KeysEnum.DefaultCulture.ToString(),
                Value = CommonStaticValues.SystemSettingsData.GeneralApplication.Values[CommonStaticValues.SystemSettingsData.GeneralApplication.KeysEnum.DefaultCulture],
                IsActive = true,
                Desc_En = "Default culture."
            });
            #endregion

            #region Smtp Application
            modelBuilder.Entity<SystemSetting>().HasData(new SystemSetting
            {
                Id = 8,
                ApplicationId = CommonStaticValues.SystemSettingsData.SmtpApplication.ApplicationId,
                Key = CommonStaticValues.SystemSettingsData.SmtpApplication.KeysEnum.SmtpServer.ToString(),
                Value = CommonStaticValues.SystemSettingsData.SmtpApplication.Values[CommonStaticValues.SystemSettingsData.SmtpApplication.KeysEnum.SmtpServer],
                IsActive = true,
                Desc_En = "Smtp Server."
            });
            modelBuilder.Entity<SystemSetting>().HasData(new SystemSetting
            {
                Id = 9,
                ApplicationId = CommonStaticValues.SystemSettingsData.SmtpApplication.ApplicationId,
                Key = CommonStaticValues.SystemSettingsData.SmtpApplication.KeysEnum.SmtpUserName.ToString(),
                Value = CommonStaticValues.SystemSettingsData.SmtpApplication.Values[CommonStaticValues.SystemSettingsData.SmtpApplication.KeysEnum.SmtpUserName],
                IsActive = true,
                Desc_En = "Smtp UserName."
            });
            modelBuilder.Entity<SystemSetting>().HasData(new SystemSetting
            {
                Id = 10,
                ApplicationId = CommonStaticValues.SystemSettingsData.SmtpApplication.ApplicationId,
                Key = CommonStaticValues.SystemSettingsData.SmtpApplication.KeysEnum.SmtpPassword.ToString(),
                Value = CommonStaticValues.SystemSettingsData.SmtpApplication.Values[CommonStaticValues.SystemSettingsData.SmtpApplication.KeysEnum.SmtpPassword],
                IsActive = true,
                Desc_En = "Smtp Password."
            });
            modelBuilder.Entity<SystemSetting>().HasData(new SystemSetting
            {
                Id = 11,
                ApplicationId = CommonStaticValues.SystemSettingsData.SmtpApplication.ApplicationId,
                Key = CommonStaticValues.SystemSettingsData.SmtpApplication.KeysEnum.IsSmtpAuthenticated.ToString(),
                Value = CommonStaticValues.SystemSettingsData.SmtpApplication.Values[CommonStaticValues.SystemSettingsData.SmtpApplication.KeysEnum.IsSmtpAuthenticated],
                IsActive = true,
                Desc_En = "Is Smtp Authenticated."
            });
            modelBuilder.Entity<SystemSetting>().HasData(new SystemSetting
            {
                Id = 12,
                ApplicationId = CommonStaticValues.SystemSettingsData.SmtpApplication.ApplicationId,
                Key = CommonStaticValues.SystemSettingsData.SmtpApplication.KeysEnum.SmtpPort.ToString(),
                Value = CommonStaticValues.SystemSettingsData.SmtpApplication.Values[CommonStaticValues.SystemSettingsData.SmtpApplication.KeysEnum.SmtpPort],
                IsActive = true,
                Desc_En = "Smtp Port."
            });
            modelBuilder.Entity<SystemSetting>().HasData(new SystemSetting
            {
                Id = 13,
                ApplicationId = CommonStaticValues.SystemSettingsData.SmtpApplication.ApplicationId,
                Key = CommonStaticValues.SystemSettingsData.SmtpApplication.KeysEnum.SmtpEnableSSL.ToString(),
                Value = CommonStaticValues.SystemSettingsData.SmtpApplication.Values[CommonStaticValues.SystemSettingsData.SmtpApplication.KeysEnum.SmtpEnableSSL],
                IsActive = true,
                Desc_En = "Smtp Enable SSL."
            });
            #endregion

            #region Attachment
            modelBuilder.Entity<SystemSetting>().HasData(new SystemSetting
            {
                Id = 14,
                ApplicationId = CommonStaticValues.SystemSettingsData.AttachmentApplication.ApplicationId,
                Key = CommonStaticValues.SystemSettingsData.AttachmentApplication.KeysEnum.AttachmentPath.ToString(),
                Value = CommonStaticValues.SystemSettingsData.AttachmentApplication.Values[CommonStaticValues.SystemSettingsData.AttachmentApplication.KeysEnum.AttachmentPath],
                IsActive = true,
                Desc_En = "Attachment folder path."
            });
            modelBuilder.Entity<SystemSetting>().HasData(new SystemSetting
            {
                Id = 15,
                ApplicationId = CommonStaticValues.SystemSettingsData.AttachmentApplication.ApplicationId,
                Key = CommonStaticValues.SystemSettingsData.AttachmentApplication.KeysEnum.SaveFilesToDatabase.ToString(),
                Value = CommonStaticValues.SystemSettingsData.AttachmentApplication.Values[CommonStaticValues.SystemSettingsData.AttachmentApplication.KeysEnum.SaveFilesToDatabase],
                IsActive = true,
                Desc_En = "should system save files in database."
            });
            #endregion
            return modelBuilder;
        }

        public static ModelBuilder SeedAttachmentTypes(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AttachmentType>().HasData(new AttachmentType
            {
                Id = (int)CommonEnumerations.AttachmentTypes.AvatarImage,
                AllowedFilesExtension= CommonStaticValues.AttachmentTypeData.AvatarImage.AllowedFilesExtension,
                Code = CommonStaticValues.AttachmentTypeData.AvatarImage.Code,
                IsImage = CommonStaticValues.AttachmentTypeData.AvatarImage.IsImage,
                MaxSizeInMegabytes = CommonStaticValues.AttachmentTypeData.AvatarImage.MaxSizeInMegabytes,
                NameEn = CommonStaticValues.AttachmentTypeData.AvatarImage.NameEn
            });
            modelBuilder.Entity<AttachmentType>().HasData(new AttachmentType
            {
                Id = (int)CommonEnumerations.AttachmentTypes.Video,
                AllowedFilesExtension = CommonStaticValues.AttachmentTypeData.Video.AllowedFilesExtension,
                Code = CommonStaticValues.AttachmentTypeData.Video.Code,
                IsImage = CommonStaticValues.AttachmentTypeData.Video.IsImage,
                MaxSizeInMegabytes = CommonStaticValues.AttachmentTypeData.Video.MaxSizeInMegabytes,
                NameEn = CommonStaticValues.AttachmentTypeData.Video.NameEn
            });
            modelBuilder.Entity<AttachmentType>().HasData(new AttachmentType
            {
                Id = (int)CommonEnumerations.AttachmentTypes.TextFile,
                AllowedFilesExtension = CommonStaticValues.AttachmentTypeData.TextFile.AllowedFilesExtension,
                Code = CommonStaticValues.AttachmentTypeData.TextFile.Code,
                IsImage = CommonStaticValues.AttachmentTypeData.TextFile.IsImage,
                MaxSizeInMegabytes = CommonStaticValues.AttachmentTypeData.TextFile.MaxSizeInMegabytes,
                NameEn = CommonStaticValues.AttachmentTypeData.TextFile.NameEn
            });
            return modelBuilder;
        }
    }

}
