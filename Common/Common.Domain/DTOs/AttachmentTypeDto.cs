using Common.Domain.Mappers;
using Common.Domain.Module;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Domain.DTOs
{
   public class AttachmentTypeDto
    {
        public int Id { get; set; }
        public string AllowedFilesExtension { get; set; }
        public string Code { get; set; }

        public int? ImageMaxHeight { get; set; }
        public int? ImageMaxWidth { get; set; }
        public bool IsImage { get; set; }
        public bool IsMandatory { get; set; }
        public int MaxSizeInMegabytes { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }

        public static implicit operator AttachmentType(AttachmentTypeDto model)
        {
            return AttachmentTypeMapper.MapToModel(model);
        }

        public static implicit operator AttachmentTypeDto(AttachmentType classRoom)
        {
            return AttachmentTypeMapper.MapToViewModel(classRoom);
        }
    }
}
