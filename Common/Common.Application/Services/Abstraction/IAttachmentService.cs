using Common.Domain.DTOs;
using Common.Domain.Module;
using CrossCutting.Core;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Common.Application.Services.Abstraction
{
    public interface IAttachmentService
    {
        string GetFilePath(int attachmentId);
        Task<ReturnResult> DeleteAttachmentAsync(int attachmentId);
        Task<ReturnResult<Attachment>> AddOrUpdateAttachment(
      IFormFile file,
      int attType,
      HttpContext httpContext,
      int? attachmentId = null,
      int? thumbnailCropSize = null);
    }
}
