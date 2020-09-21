using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using UserManagement.Application.Persistance;
using UserManagement.Application.Services.Abstraction;
using UserManagement.Domain.DTOs;
using UserManagement.Domain.Models;

namespace UserManagement.Application.Services.Concrete
{
    public sealed  class DashboardService: IDashboardService
    {
        public DashboardService(IUserManagementUnitOfWork userManagementUnitOfWork,
         UserManager<ApplicationUser> userManager,
         RoleManager<ApplicationRole> roleManager)
        {
            UserManagementUnitOfWork = userManagementUnitOfWork;
            UserManager = userManager;
            RoleManager = roleManager;
        }

        public IUserManagementUnitOfWork UserManagementUnitOfWork { get; }
        public UserManager<ApplicationUser> UserManager { get; }
        public RoleManager<ApplicationRole> RoleManager { get; }

        public DashboardCardsDto GetDashboadCards()
        {
            DashboardCardsDto dto = new DashboardCardsDto();

            dto.TotalUsers = UserManager.Users.Count();
            dto.ActiveUsers = UserManager.Users.Count(a => a.LockoutEnd == null);
            dto.InactiveUsers = UserManager.Users.Count(a => a.LockoutEnd != null);
            dto.TotalRoles = RoleManager.Roles.Count();

            return dto;
        }

        public List<DayUsersDashboardDto> GetUsersPerDayForDashboard(DateTime? from, DateTime? to)
        {
            var query = UserManager.Users;

            if (from.HasValue)
                query = query.Where(a => a.CreatedOn.Value.Date >= from);

            if (to.HasValue)
                query = query.Where(a => a.CreatedOn.Value.Date <= to);

            return query.GroupBy(user => new DayUsersDashboardDto
            {
                Year = user.CreatedOn.Value.Year,
                Month = user.CreatedOn.Value.Month,
                Day = user.CreatedOn.Value.Day
            }).Select(grouped => new DayUsersDashboardDto
            {
                Year = grouped.Key.Year,
                Month = grouped.Key.Month,
                Day = grouped.Key.Day,
                Count = grouped.Count()
            }).ToList();
        }
    }
}
