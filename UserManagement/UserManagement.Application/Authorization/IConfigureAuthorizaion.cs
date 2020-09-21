using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace UserManagement.Application.Authorization
{
    public interface IConfigureAuthorizaion: IDisposable
    {
        IConfigureAuthorizaion AllowAnonymousToPages(RazorPagesOptions options);
        IConfigureAuthorizaion AuthorizePages(RazorPagesOptions options);
        IConfigureAuthorizaion AuthorizeAreaPages(RazorPagesOptions options);
    }
}