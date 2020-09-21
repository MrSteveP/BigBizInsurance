using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UserManagement.Domain.Models;

namespace UserManagement.Application.Services.Logic
{
    public sealed class AuthorizationPolicyCreation
    {
        public AuthorizationPolicyCreation(AuthorizationOptions options, RoleManager<ApplicationRole> roleManager)
        {
            Options = options;
            RoleManager = roleManager;
        }

        public AuthorizationOptions Options { get; }
        public RoleManager<ApplicationRole> RoleManager { get; }

        public void Create(string policyName, List<Guid> selectedRoleIds)
        {
            var selectedRoles = RoleManager.Roles.Where(role => selectedRoleIds.Contains(role.Id))
              .Select(role => role.Name).ToList();
            Options.AddPolicy(policyName, policy => policy.RequireRole(selectedRoles));
        }
        public void Clear(string policyName)
        {
            Options.AddPolicy(policyName, policy => policy.RequireRole(""));
        }
    }
}
