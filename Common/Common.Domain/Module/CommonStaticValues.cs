using System.Collections.Generic;

namespace Common.Domain.Module
{
    public static class CommonStaticValues
    {
        public static class SystemSettingsData
        {
            public static class GeneralApplication
            {
                public static string ApplicationId = "General";
                public enum KeysEnum
                {
                    DefaultCulture
                }
                public static Dictionary<KeysEnum, string> Values = new Dictionary<KeysEnum, string>
                {
                    {KeysEnum.DefaultCulture,"en-US" },
                };
            }

            public static class SmtpApplication
            {
                public static string ApplicationId = "Smtp";
                public enum KeysEnum
                {
                    SmtpServer,
                    SmtpUserName,
                    SmtpPassword,
                    IsSmtpAuthenticated,
                    SmtpPort,
                    SmtpEnableSSL
                }
                public static Dictionary<KeysEnum, string> Values = new Dictionary<KeysEnum, string>
                {
                    {KeysEnum.SmtpServer,"" },
                    {KeysEnum.SmtpUserName,"" },
                    {KeysEnum.SmtpPassword,"" },
                    {KeysEnum.IsSmtpAuthenticated,false.ToString() },
                    {KeysEnum.SmtpPort,"0" },
                    {KeysEnum.SmtpEnableSSL,false.ToString()  },
                };
            }


            public static class AuthenticationGeneralApplication
            {
                public static string ApplicationId = "AuthenticationGeneral";
                public enum KeysEnum
                {
                    AllowRememberMe,
                    AllowForgotPassword,
                    RequireConfirmedAccount
                }
                public static Dictionary<KeysEnum, string> Values = new Dictionary<KeysEnum, string>
                {
                    {KeysEnum.AllowRememberMe,true.ToString() },
                    {KeysEnum.AllowForgotPassword,true.ToString() },
                    {KeysEnum.RequireConfirmedAccount,false.ToString() },
                    
                };
            }
            public static class AuthenticationThrottlingApplication
            {

                public static string ApplicationId = "AuthenticationThrottling";
                public enum KeysEnum
                {
                    // Should the system throttle authentication attempts?
                    AllowThrottleAuthentication,
                    //Maximum number of incorrect login attempts before lockout.
                    MaximumNumberOfAttempts,
                    //Number of minutes to lock the user out for after specified maximum numbers
                    LockoutTime
                }
                public static Dictionary<KeysEnum, string> Values = new Dictionary<KeysEnum, string>
                {
                    {KeysEnum.AllowThrottleAuthentication,true.ToString() },
                    {KeysEnum.LockoutTime,"5" },
                    {KeysEnum.MaximumNumberOfAttempts,"3" }
                };
            }

            public static class AttachmentApplication
            {

                public static string ApplicationId = "Attachment";
                public enum KeysEnum
                {
                    AttachmentPath,
                    SaveFilesToDatabase
                }
                public static Dictionary<KeysEnum, string> Values = new Dictionary<KeysEnum, string>
                {
                    {KeysEnum.AttachmentPath,@"C:\AppFiles" },
                    {KeysEnum.SaveFilesToDatabase,false.ToString() }

                };
            }
        }

        public static class AttachmentTypeData
        {
            public static class AvatarImage
            {
                public static string Code = "AvatarImage";
                public static string NameEn = "Personal image";
                public static string AllowedFilesExtension = "pdf,jpg,png";
                public static bool IsImage = true;
                public static int MaxSizeInMegabytes = 2;
            }
            public static class Video
            {
                public static string Code = "Video";
                public static string NameEn = "Video File";
                public static string AllowedFilesExtension = "mp4,vlc,avi,wmv";
                public static bool IsImage = false;
                public static int MaxSizeInMegabytes = 1024;
            }
            public static class TextFile
            {
                public static string Code = "TextFile";
                public static string NameEn = "Word or Pdf file";
                public static string AllowedFilesExtension = "doc,docx,pdf";
                public static bool IsImage = false;
                public static int MaxSizeInMegabytes = 20;
            }
        }
    }
}
