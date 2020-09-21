using Common.Domain.DTOs;
using Common.Domain.Module;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Domain.Mappers
{
    public static class AttachmentTypeMapper
    {
        public static AttachmentType MapToModel(AttachmentTypeDto viewModel)
        {
            if (viewModel == null) return null;

            var attachmentType = new AttachmentType();
            attachmentType.AllowedFilesExtension = viewModel.AllowedFilesExtension;
            attachmentType.Code = viewModel.Code;
            attachmentType.ImageMaxHeight = viewModel.ImageMaxHeight;
            attachmentType.ImageMaxWidth = viewModel.ImageMaxWidth;
            attachmentType.IsImage = viewModel.IsImage;
            attachmentType.IsMandatory = viewModel.IsMandatory;
            attachmentType.MaxSizeInMegabytes = viewModel.MaxSizeInMegabytes;
            attachmentType.NameAr = viewModel.NameAr;
            attachmentType.NameEn = viewModel.NameEn;

            if (viewModel.Id > 0)
                attachmentType.Id = viewModel.Id;

            return attachmentType;
        }

        public static AttachmentTypeDto MapToViewModel(AttachmentType model)
        {
            if (model == null) return null;

            return new AttachmentTypeDto()
            {
                Id = model.Id,
                AllowedFilesExtension = model.AllowedFilesExtension,
                Code = model.Code,
                ImageMaxHeight = model.ImageMaxHeight,
                ImageMaxWidth = model.ImageMaxWidth,
                IsImage = model.IsImage,
                IsMandatory = model.IsMandatory,
                MaxSizeInMegabytes = model.MaxSizeInMegabytes,
                NameAr = model.NameAr,
                NameEn = model.NameEn
            };
        }
    }
}
