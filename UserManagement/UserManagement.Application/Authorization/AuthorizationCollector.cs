using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Application.Authorization.Models;
using UserManagement.Application.Persistance;

namespace UserManagement.Application.Authorization
{
    public class AuthorizationCollector
    {
        string Root { get; }
        public IUserManagementUnitOfWork UserManagementUnitOfWork { get; }

        const string PAGES_FOLDER = "Pages";
        const string SHARED_FOLDER = "Shared";
        const string AREAS_FOLDER = "Areas";
        const string PAGE_EXTENSION = ".cshtml";
        public AuthorizationCollector(string root, IUserManagementUnitOfWork userManagementUnitOfWork)
        {
            Root = root;
            UserManagementUnitOfWork = userManagementUnitOfWork;
        }

        public async Task<List<SitePolicy>> GetAllSitePolicies()
        {
            var policy = await UserManagementUnitOfWork.PolicyRepository.GetAsync();
            var allRelatedRoles = (await UserManagementUnitOfWork.ApplicationPolicyRoleRepository
                .GetAsync(null, null, a => a.ApplicationRole)).ToList();

            List<SitePolicy> sitePolicies = new List<SitePolicy>();
            policy.ForEach(policy => 
            {
                SitePolicy sitePolicy = new SitePolicy();
                sitePolicy.Name = policy.Name;
                sitePolicy.Roles = allRelatedRoles.Where(a => a.ApplicationPolicyId == policy.Id)
                                  .Select(a => a.ApplicationRole.Name).ToHashSet();

                sitePolicies.Add(sitePolicy);
            });

            return sitePolicies;
        }

        public List<SiteArea> GetAllSiteAreas()
        {
            List<string> subdirectoryEntries = Directory.GetDirectories(Root).ToList();

            List<SiteArea> siteAreas = GetSiteAreas(subdirectoryEntries);
            CollectFromPagesFolder(Root, siteAreas, "");

            return siteAreas;
        }

        public List<SitePage> GetAllSitePages()
        {
            List<SiteArea> siteAreas = GetAllSiteAreas();
            return ExtractSitePages(siteAreas).OrderBy(a=>a.AreaName).ToList();
        }

        private List<SitePage> ExtractSitePages(List<SiteArea> siteAreas)
        {
            List<SitePage> allSitePages = new List<SitePage>();

            var rootSitePages = siteAreas.SelectMany(a => a.SitePages).ToList();
            allSitePages.AddRange(rootSitePages);

            List<SiteFolder> AllSiteFolders = new List<SiteFolder>();
            var rootFolders = siteAreas.SelectMany(a => a.SiteFolders).ToList();
            AllSiteFolders.AddRange(rootFolders);


            while (rootFolders.Any(a => a.SiteFolders.Count() > 0))
            {
                rootFolders = rootFolders.SelectMany(a => a.SiteFolders).ToList();
                AllSiteFolders.AddRange(rootFolders);
            }

            allSitePages.AddRange(AllSiteFolders.SelectMany(a => a.SitePages));

            allSitePages = allSitePages.OrderBy(a => a.Path).ToList();
            return allSitePages;
        }

        private List<SiteArea> GetSiteAreas(List<string> subdirectoryEntries)
        {
            List<string> areas_PagesDirectories = subdirectoryEntries.Where(dir => dir.EndsWith(AREAS_FOLDER)).ToList();
            string areasRootDir = areas_PagesDirectories.FirstOrDefault(dir => dir.EndsWith(AREAS_FOLDER));
            List<string> areasDirList = Directory.GetDirectories(areasRootDir).ToList();

            List<SiteArea> siteAreasList = new List<SiteArea>();
            areasDirList.ForEach(areaDir =>
            {
                string areaName = areaDir.Split("\\").LastOrDefault();
                CollectFromPagesFolder(areaDir, siteAreasList, areaName);
            });

            return siteAreasList;
        }

        private void CollectFromPagesFolder(string areaDir, List<SiteArea> siteAreasList, string areaName)
        {
            string areaPagesDir = Directory.GetDirectories(areaDir).FirstOrDefault(subDir => subDir.EndsWith(PAGES_FOLDER));

            if (!string.IsNullOrEmpty(areaPagesDir))
            {
                List<SiteFolder> siteFolders = GetAreaFolders(areaName, "", areaPagesDir);
                List<SitePage> sitePages = GetAreaPages(areaName, "", areaPagesDir);


                SiteArea siteArea = new SiteArea();
                siteArea.Name = areaName;
                siteArea.SiteFolders = siteFolders.ToHashSet();
                siteArea.SitePages = sitePages.ToHashSet();

                siteAreasList.Add(siteArea);
            }
        }

        private List<SiteFolder> GetAreaFolders(string areaName, string parentFolderPath, string areaPagesDir)
        {
            List<SiteFolder> siteFolders = new List<SiteFolder>();

          
                List<string> areaSubDirs = Directory.GetDirectories(areaPagesDir)
                                           .Where(areaSubDir => !areaSubDir.EndsWith(SHARED_FOLDER)).ToList();


                areaSubDirs.ForEach(areaSubDir =>
                {
                    string folderName = areaSubDir.Split("\\").LastOrDefault();
                    string folderPath = "/" + folderName;

                    SiteFolder siteFolder = new SiteFolder();
                    siteFolder.AreaName = areaName;
                    siteFolder.FolderName = folderName;
                    siteFolder.Path = parentFolderPath + folderPath;
                    siteFolder.SiteFolders = GetAreaFolders(areaName, folderPath, areaSubDir).ToHashSet();
                    siteFolder.SitePages = GetAreaPages(areaName, parentFolderPath, areaSubDir).ToHashSet();
                    siteFolders.Add(siteFolder);
                });
                return siteFolders;
        }

        private List<SitePage> GetAreaPages(string areaName, string parentFolderPath, string areaPagesDir)
        {
            List<SitePage> sitePages = new List<SitePage>();

            List<string> areaPages = Directory.GetFiles(areaPagesDir)
                .Select(pagePath => pagePath.Split("\\").LastOrDefault())
                .Where(page => !page.StartsWith("_") && page.EndsWith(PAGE_EXTENSION)).ToList();

            string folderName = areaPagesDir.Split("\\").LastOrDefault();
            if (folderName == PAGES_FOLDER) folderName = "";

            areaPages.ForEach(areaPage =>
            {
                string pageName = Path.GetFileNameWithoutExtension(areaPage);

                SitePage sitePage = new SitePage();
                sitePage.AreaName = areaName;
                sitePage.FolderName = folderName;
                sitePage.Path = !string.IsNullOrEmpty(folderName) ? parentFolderPath + "/" + folderName + "/" + pageName : "/" + pageName;
                sitePage.PageName = pageName;


                sitePages.Add(sitePage);
            });
            return sitePages;
        }

    }
}
