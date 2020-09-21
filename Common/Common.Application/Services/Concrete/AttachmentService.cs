using Common.Application.Persistance;
using Common.Application.Services.Abstraction;
using Common.Domain.DTOs;
using Common.Domain.Module;
using CrossCutting.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MessageResources = Common.Localization.Messages.Messages;

namespace Common.Application.Services.Concrete
{
    public sealed class AttachmentService : IAttachmentService
    {
        public AttachmentService(ICommonUnitOfWork commonUnitOfWork, ISystemSettingsService systemSettingsService)
        {
            CommonUnitOfWork = commonUnitOfWork;
            SystemSettingsService = systemSettingsService;
        }

        public ICommonUnitOfWork CommonUnitOfWork { get; }
        public ISystemSettingsService SystemSettingsService { get; }

        public static class TempMessages
        {
            public static string ErrorOccured = MessageResources.AttachmentErrorOccured;
            public static string AttachmentSizeError = MessageResources.AttachmentSizeError;
            public static string Attachment_ErrorMessage_MaxSize = MessageResources.Attachment_ErrorMessage_MaxSize ;
            public static string AttachmentNotAllowedExtension = MessageResources.AttachmentNotAllowedExtension;
        }

   
        public string GetFilePath(int attachmentId)
        {
            var filePath = CommonUnitOfWork.AttachmentRepository.GetFirstBySelector(a => a.FilePath, a => a.Id == attachmentId);
            return $"{this.SystemSettingsService.AttachmentSystemSettings.AttachmentPath}{filePath}";
        }
        public async Task<ReturnResult<Attachment>> AddOrUpdateAttachment(
      IFormFile file,
      int attType,
      HttpContext httpContext,
      int? attachmentId = null,
      int? thumbnailCropSize = null)
        {
            var result = new ReturnResult<Attachment>();
            var attachmentType = CommonUnitOfWork.AttachmentTypeRepository.GetById(attType);

            if (file == null)
            {
                result.AddErrorItem(string.Empty, TempMessages.ErrorOccured);
                return result;
            }

            if (file.Length <= 0)
            {
                result.AddErrorItem(string.Empty, TempMessages.AttachmentSizeError);
                // return result;
            }

            if (file.Length > (attachmentType.MaxSizeInMegabytes * 1024 * 1024))
            {
                result.AddErrorItem(string.Empty, string.Format(TempMessages.Attachment_ErrorMessage_MaxSize, attachmentType.MaxSizeInMegabytes));
                //return result;
            }

            if (this.SystemSettingsService.AttachmentSystemSettings.SaveFilesToDatabase
                && string.IsNullOrEmpty(this.SystemSettingsService.AttachmentSystemSettings.AttachmentPath))
            {
                throw new Exception(
                    "File can not be saved. Current Settings is. SaveFileToDatabase=true and Attachment Path is Missing");
            }

            var allowedFilesExtension = attachmentType.AllowedFilesExtension.Split(',');

            if (!allowedFilesExtension.Select(ext => ext.ToLower())
                .Contains(Path.GetExtension(file.FileName)
                .Replace(".","").ToLower()))
            {
                result.AddErrorItem(string.Empty, string.Format(TempMessages.AttachmentNotAllowedExtension, attachmentType.AllowedFilesExtension));
                //return result;
            }
            if (!result.IsValid)
            {
                return result;
            }

            result.Value = await this.AddOrUpdateAttachment(
                file.FileName,
                file.ContentType,
                file,
                attType,
                 httpContext,
                attachmentId);
            return result;
        }

        private async Task<Attachment> AddOrUpdateAttachment(
          string fileName,
          string contentType,
          IFormFile file,
          int attType,
           HttpContext httpContext,
          int? attachmentId = null,
          string titleAr = null,
          string titleEn = null,
          string descriptionAr = null,
          string descriptionEn = null,
          int? itemOrder = null,
          int? thumbnailCropSize = null)
        {
            var isUpdateFile = attachmentId.HasValue && attachmentId.HasValue;

            var attachment = isUpdateFile
                                 ? CommonUnitOfWork.AttachmentRepository.GetById(attachmentId.Value)
                                 : new Attachment();

            if (attachment == null)
            {
                throw new Exception("The Attachment File You are trying to update Does Not Exist in the database");
            }

            attachment.TitleAr = titleAr;
            attachment.TitleEn = titleEn;
            attachment.DescriptionAr = descriptionAr ?? attachment.DescriptionAr;
            attachment.DescriptionEn = descriptionEn ?? attachment.DescriptionEn;
            attachment.ItemOrder = itemOrder ?? attachment.ItemOrder;
            attachment.ContentType = contentType;
            attachment.Extention = new FileInfo(fileName).Extension;
            attachment.FileName = fileName;
            attachment.AttachmentTypeId = attType;

            // in updating delete old file
            if (isUpdateFile)
            {
                this.DeleteAttachmentFromFileSystem(attachment.FilePath);
            }

            attachment.FilePath = this.SystemSettingsService.AttachmentSystemSettings.SaveFilesToDatabase
                                      ? null
                                      : await this.SaveAttachmentToFileSystem(attachment, file);
            if (isUpdateFile)
            {
                CommonUnitOfWork.AttachmentRepository.Update(attachment);
                 CommonUnitOfWork.Save();
            }
            else
            {
                attachment.ItemOrder = itemOrder ?? 1;
                CommonUnitOfWork.AttachmentRepository.Add(attachment);
                 CommonUnitOfWork.Save();
            }
            return attachment;
        }

        public void DeleteAttachmentFromFileSystem(string fileRelativePath)
        {
            if (string.IsNullOrEmpty(fileRelativePath))
            {
                return;
            }

            var filePath = $@"{this.SystemSettingsService.AttachmentSystemSettings.AttachmentPath}{fileRelativePath}";
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        private async Task<string> SaveAttachmentToFileSystem(Attachment attach, IFormFile file, string attachmentFilePath = null)
        {
            var relativeFolderPath = string.IsNullOrEmpty(attachmentFilePath) ?
                $"\\{DateTime.Now.Year}\\{DateTime.Now.Month}\\{DateTime.Now.Day}"
            : attachmentFilePath;
            var fullFolderPath = $"{this.SystemSettingsService.AttachmentSystemSettings.AttachmentPath}{relativeFolderPath}";
            var fileName = /*attach.Id*/Guid.NewGuid() + attach.Extention;
            var fileRelativePath = $@"{relativeFolderPath}\{fileName}";

            if (!Directory.Exists(fullFolderPath))
            {
                Directory.CreateDirectory(fullFolderPath);
            }

            var path = Path.Combine(fullFolderPath, fileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileRelativePath;
        }

        public async Task<ReturnResult> DeleteAttachmentAsync(int attachmentId)
        {
            if (attachmentId.IsNullOrDefault<Guid>())
            {
                throw new ArgumentNullException(nameof(attachmentId));
            }
            var result = new ReturnResult();
            var attachment = await CommonUnitOfWork.AttachmentRepository.GetByIdAsync(attachmentId);

            if (attachment != null)
            {
                this.DeleteAttachmentFromFileSystem(attachment.FilePath);
                CommonUnitOfWork.AttachmentRepository.DeleteAsync(attachment);
            }
            CommonUnitOfWork.Save();

            return result;
        }
    }
}
