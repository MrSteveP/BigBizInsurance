namespace UserManagement.Domain.ViewModels
{
    public class SMTPViewModel
    {
        public string SmtpServer { get; set; }
        public string SmtpUserName { get; set; }
        public string SmtpPassword { get; set; }
        public bool IsSmtpAuthenticated { get; set; }
        public int? SmtpPort { get; set; }
        public bool SmtpEnableSSL { get; set; }
    }
}
