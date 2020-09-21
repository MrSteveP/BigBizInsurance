namespace Common.Application.Services.Abstraction
{
    public interface ISmtpSystemSettings
    {
        string SmtpServer { set; get; }
        string SmtpUserName { set; get; }
        string SmtpPassword { set; get; }
        bool IsSmtpAuthenticated { set; get; }
        int SmtpPort { set; get; }
        bool SmtpEnableSSL { set; get; }
    }
}
