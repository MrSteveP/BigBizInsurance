namespace UserManagement.Domain.DTOs
{
    public class DashboardCardsDto
    {
        public int TotalUsers { set; get; }
        public int ActiveUsers { set; get; }
        public int InactiveUsers { set; get; }
        public int TotalRoles { set; get; }
    }
}
