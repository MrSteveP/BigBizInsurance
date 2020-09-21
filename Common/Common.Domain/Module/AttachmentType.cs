using CrossCutting.Persistance;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Domain.Module
{
    [Table("AttachmentTypes", Schema = "common")]
    public class AttachmentType : IBaseModel
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
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
