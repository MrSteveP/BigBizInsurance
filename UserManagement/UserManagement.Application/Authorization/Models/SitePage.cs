namespace UserManagement.Application.Authorization.Models
{
    public class SitePage
    {
        public string AreaName { set; get; } = string.Empty;
        public string PageName { get; set; } = string.Empty;
        public string FolderName { get; set; } = string.Empty;
        public bool IsRoot => string.IsNullOrEmpty(this.FolderName);
        public string PolicyName { set; get; } = string.Empty;
        public string Path { get; set; } = string.Empty;
    }
}
