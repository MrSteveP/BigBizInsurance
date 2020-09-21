using System;
using System.Collections.Generic;
using UserManagement.Domain.DTOs;

namespace UserManagement.Application.Services.Abstraction
{
    public interface IDashboardService
    {
        DashboardCardsDto GetDashboadCards();
        List<DayUsersDashboardDto> GetUsersPerDayForDashboard(DateTime? from, DateTime? to);
    }
}
