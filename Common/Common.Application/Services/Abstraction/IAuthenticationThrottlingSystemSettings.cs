namespace Common.Application.Services.Abstraction
{
    public interface IAuthenticationThrottlingSystemSettings
    {
        bool AllowThrottleAuthentication { set; get; }
        int LockoutTime { set; get; }
        int MaximumNumberOfAttempts { set; get; }
    }
}
