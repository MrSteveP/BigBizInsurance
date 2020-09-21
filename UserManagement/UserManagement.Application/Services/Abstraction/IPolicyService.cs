using CrossCutting.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Application.Authorization.Models;
using UserManagement.Domain.ViewModels;

namespace UserManagement.Application.Services.Abstraction
{
    public interface IPolicyService
    {
        Task<ReturnResult<PolicyViewModel>> GetById(int policyId);
        Task<List<PolicyViewModel>> GetAll();
        Task<ReturnResult<PolicyViewModel>> Save(ReturnResult<PolicyViewModel> model);
        Task<ReturnResult<int>> Delete(ReturnResult<int> model);
        Task<PolicyFilterViewModel> Search(PolicyFilterViewModel model);

        List<SitePage> GetAllSitePages(string root);
        Task<ReturnResult<List<SitePage>>> SaveApplyPolicyChanges(string root, ReturnResult<List<SitePage>> model);
    }
}
