using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Common.Application.Services.Abstraction;
using Common.Domain.Module;
using CrossCutting.Helpers;
using Microsoft.AspNetCore.Mvc;
using SharedResources= Common.Localization.Shared.Shared;
namespace BigBizInsurance.Web.Controllers
{
    [Route("[controller]")]

    public class FileController : Controller
    {
        public FileController(IAttachmentService attachmentService)
        {
            AttachmentService = attachmentService;
        }

        public IAttachmentService AttachmentService { get; }

        [HttpGet("Download/{attachmentId?}")]
        public async Task<IActionResult> Download(int attachmentId)
        {
            string filePath = AttachmentService.GetFilePath(attachmentId);
            (MemoryStream memory, string contentType, string fileName) = await DownloadHelper.GetDataToDownload(filePath);
            var fileStreamResult = new FileStreamResult(memory, contentType);
            fileStreamResult.FileDownloadName = fileName;

            if (fileStreamResult == null)
                return Content(SharedResources.FileNotFound);

            return fileStreamResult;
        }
    }
}
