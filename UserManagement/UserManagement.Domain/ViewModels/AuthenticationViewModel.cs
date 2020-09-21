namespace UserManagement.Domain.ViewModels
{
    public class AuthenticationViewModel
    {
        public bool AllowRememberMe { set; get; }
        public bool AllowForgotPassword { set; get; }
        public bool RequireConfirmedAccount { set; get; }
        public bool AllowThrottleAuthentication { set; get; }
        public int LockoutTime { set; get; }
        public int MaximumNumberOfAttempts { set; get; }
    }
}
