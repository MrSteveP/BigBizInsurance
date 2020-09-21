using CrossCutting.Core;
using CrossCutting.Core.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Application.Authorization;
using UserManagement.Application.Authorization.Models;
using UserManagement.Application.Persistance;
using UserManagement.Application.Services.Abstraction;
using UserManagement.Application.Services.Logic;
using UserManagement.Domain.Mapper;
using UserManagement.Domain.Models;
using UserManagement.Domain.ViewModels;
using PolicyResources = UserManagement.Localization.PoliciesManagement.PoliciesManagement;
using SharedResources = Common.Localization.Shared.Shared;

namespace UserManagement.Application.Services.Concrete
{
    public sealed class PolicyService : IPolicyService
    {
        public PolicyService(IUserManagementUnitOfWork userManagementUnitOfWork,
            IOptions<AuthorizationOptions> options,
            RoleManager<ApplicationRole> roleManager)
        {
            UserManagementUnitOfWork = userManagementUnitOfWork;
            RoleManager = roleManager;
            _options = options.Value;
        }

        public IUserManagementUnitOfWork UserManagementUnitOfWork { get; }
        public RoleManager<ApplicationRole> RoleManager { get; }

        private readonly AuthorizationOptions _options;

        public async Task<ReturnResult<PolicyViewModel>> GetById(int policyId)
        {
            ReturnResult<PolicyViewModel> result = new ReturnResult<PolicyViewModel>();

            var policy = await UserManagementUnitOfWork.PolicyRepository.GetByIdAsync(policyId);
            List<RoleViewModel> relatedRoles = (await UserManagementUnitOfWork.ApplicationPolicyRoleRepository
                .GetAsync(a => a.ApplicationPolicyId == policy.Id, null, a => a.ApplicationRole))
                .Select(a => RoleMapper.MapToViewModel(a.ApplicationRole))
                .ToList();
            result.Value = policy;
            result.Value.Roles = relatedRoles;

            if (policy == null)
                result.AddErrorItem("", PolicyResources.msg_PolicyNotFound);

            return result;

        }

        public async Task<List<PolicyViewModel>> GetAll()
        {
            return (await UserManagementUnitOfWork.PolicyRepository.GetAsync())
                     .Select(policy => PolicyMapper.MapToViewModel(policy)).ToList();
        }

        public async Task<ReturnResult<PolicyViewModel>> Save(ReturnResult<PolicyViewModel> model)
        {

            var requestlogic = PolicyFactory.CreateInstance(model.Value.Id, UserManagementUnitOfWork, _options, RoleManager);

            model = await requestlogic.IsValidAsync(model);
            if (!model.IsValid)
                return model;

            return await requestlogic.SaveAsync(model);
        }



        public async Task<ReturnResult<int>> Delete(ReturnResult<int> model)
        {
            var policy = await UserManagementUnitOfWork.PolicyRepository.GetByIdAsync(model.Value);
            var policyRoles = await UserManagementUnitOfWork.ApplicationPolicyRoleRepository.GetAsync(a => a.ApplicationPolicyId == model.Value);

            UserManagementUnitOfWork.ApplicationPolicyRoleRepository.Delete(policyRoles);
            var result = UserManagementUnitOfWork.PolicyRepository.Delete(policy);
             UserManagementUnitOfWork.Save();

            //AuthorizationPolicyCreation authorizationPolicyCreation = new AuthorizationPolicyCreation(_options, RoleManager);
            // authorizationPolicyCreation.Clear(policy.Name);

            if (!result)
                model.AddErrorItem("", SharedResources.DeleteFail);

            return model;
        }

        public async Task<PolicyFilterViewModel> Search(PolicyFilterViewModel model)
        {
            var query = UserManagementUnitOfWork.PolicyRepository.GetQuery();

            if (!string.IsNullOrEmpty(model.Name))
                query = query.Where(m => m.Name.Contains(model.Name.ToLower()));

            List<PolicyViewModel> items = new List<PolicyViewModel>();

            model.TotalCount = query.Count();
            if (model.jtPageSize > 0)
                items = await query.Skip((int)(model.PageNumber * model.jtPageSize)).Take((int)model.jtPageSize)
                    .Select(policy => PolicyMapper.MapToViewModel(policy)).ToListAsync();
            else
                items = await query.Select(policy => PolicyMapper.MapToViewModel(policy)).ToListAsync();

            model.Items = items;

            return model;
        }


        public List<SitePage> GetAllSitePages(string root)
        {
            AuthorizationCollector authorizationCollector = new AuthorizationCollector(root, UserManagementUnitOfWork);

            var sitePagesListFromJsonFile = AuthorizationConfig.ReadAuthorizationConfig().SitePages;
            var sitePagesListFromFileSystem = authorizationCollector.GetAllSitePages();
            sitePagesListFromFileSystem.ForEach(sitePageFromFileSystem=>
            {
            var sitePageFromJsonFile = sitePagesListFromJsonFile.FirstOrDefault(a =>
             a.AreaName.NullToString() == sitePageFromFileSystem.AreaName &&
             a.FolderName.NullToString() == sitePageFromFileSystem.FolderName &&
             a.Path.NullToString() == sitePageFromFileSystem.Path);

                sitePageFromFileSystem.PolicyName = sitePageFromJsonFile?.PolicyName;
            });

            return sitePagesListFromFileSystem;
        }

        public async Task<ReturnResult<List<SitePage>>> SaveApplyPolicyChanges(string root, ReturnResult<List<SitePage>> model)
        {
            AuthorizationCollector authorizationCollector = new AuthorizationCollector(root, UserManagementUnitOfWork);
            var allSitePolicies = await authorizationCollector.GetAllSitePolicies();

            AuthorizationConfig.WriteAuthorizationConfig(allSitePolicies.ToHashSet(), model.Value.ToHashSet());
            ChechUserAuthorization.LoadSitePagesFromJson();
            return model;
        }

        private void UpdateSiteFolders(ReturnResult<List<SitePage>> model, SiteFolder siteFolder)
        {
            UpdateSitePages(model, siteFolder.SitePages.ToList());

            if (siteFolder.SiteFolders.ToList().Any())
            {
                siteFolder.SiteFolders.ToList().ForEach(innerFolder =>
                {
                    UpdateSiteFolders(model, innerFolder);
                });
            }
        }
        private void UpdateSitePages(ReturnResult<List<SitePage>> model, List<SitePage> sitePages)
        {
            if (sitePages.Any())
            {
                sitePages.ForEach(sitePage =>
                {
                    var inputSitePage = model.Value.FirstOrDefault(a =>
                     a.AreaName == sitePage.AreaName &&
                     a.FolderName == sitePage.FolderName &&
                     a.Path == sitePage.Path);

                    if (inputSitePage != null)
                        sitePage.PolicyName = inputSitePage.PolicyName;
                });
            }
        }
    }
}
