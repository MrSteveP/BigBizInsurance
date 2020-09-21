namespace Common.Application.Services.Abstraction
{
    public interface IAuthenticationGeneralSystemSettings
    {
        bool AllowRememberMe { set; get; }
        bool AllowForgotPassword { set; get; }
        bool RequireConfirmedAccount { set; get; }
    }
}
