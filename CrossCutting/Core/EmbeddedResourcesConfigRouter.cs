using CrossCutting.Core.Extensions;
using CrossCutting.Core.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using System.IO;
using System.Linq;
using System.Text;

namespace CrossCutting.Core
{
    /// <summary>
    /// The embedded resources config router.
    /// </summary>
    public static class EmbeddedResourcesConfigRouter
    {

        /// <summary>
        /// The register globalization routes.
        /// </summary>
        /// <param name="app">
        /// The app.
        /// </param>
        /// <returns>
        /// The <see cref="IRouter"/>.
        /// </returns>
        public static IRouter RegisterGlobalizationRoutes(IApplicationBuilder app)
        {
            // from Usama Nada
            var routeBuilder = new RouteBuilder(app);

            // Routing For Widgets Embedded Resources
            routeBuilder.MapGet(
                "Globalization/Resources/v{versionNumber}/{*filepath}",
                httpContext =>
                {
                    var filePath = httpContext.Request.Path;
                    var extension = Path.GetExtension(filePath);

                    const int AssemblyNameIndex = 4;
                    const int ResourceTypeFullNameIndex = 5;
                    const int CultureNameIndex = 6;
                    const int FormatIndex = 7;

                    var arr = httpContext.Request.Path.Value.Split('/');
                    var assemblyName = arr.SafeGet(AssemblyNameIndex);
                    var resourceTypeFullName = arr.SafeGet(ResourceTypeFullNameIndex);
                    var cultureName = arr.SafeGet(CultureNameIndex);
                    var format = !string.IsNullOrEmpty(arr.SafeGet(FormatIndex))
                                     ? arr.SafeGet(FormatIndex)
                                     : "javascript";

                    string resourceJavaScriptVarName = null;
                    if (resourceTypeFullName != null && resourceTypeFullName.Contains(".")
                                                     && format == "javascript")
                    {
                        resourceJavaScriptVarName =
                            resourceTypeFullName.Substring(resourceTypeFullName.LastIndexOf('.') + 1);
                        if (resourceTypeFullName.Count(a => a == '.') > 1)
                        {

                        }
                    }

                    resourceJavaScriptVarName = resourceJavaScriptVarName ?? "applicationResources";

                    var fileContents = ResxConverter.ToJson(assemblyName, resourceTypeFullName, cultureName);

                    if (format == "javascript")
                    {
                        // this code makes just one object for appResource file and separated objects for every module.
                        fileContents = "var $" + resourceTypeFullName?.Replace(".", "_") + " = " + fileContents + ";" +
                                       "if(typeof $" + resourceJavaScriptVarName.ToCamelCase() + " === 'undefined')" +
                                       "{var $" + resourceJavaScriptVarName.ToCamelCase() + " = " + fileContents + "; }" +
                                       "else" +
                                       "{ $" + resourceJavaScriptVarName.ToCamelCase() + " = Object.assign({},$" + resourceJavaScriptVarName.ToCamelCase() + ", $" + resourceTypeFullName?.Replace(".", "_") + ");}";
                    }

                    httpContext.Response.ContentType = "application/json";
                    httpContext.Response.Headers.Add("charset", "utf-8");
                    var data = Encoding.UTF8.GetBytes(fileContents);
                    return httpContext.Response.Body.WriteAsync(data, 0, data.Length);
                });

            return routeBuilder.Build();
        }
    }
}
