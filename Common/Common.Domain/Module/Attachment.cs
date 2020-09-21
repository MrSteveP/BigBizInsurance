using CrossCutting.Persistance;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Domain.Module
{
    [Table("Attachments", Schema = "common")]
    public class Attachment:IBaseModel
    {
        public int Id { set; get; }
        public string FileName { set; get; }
        public string Extention { set; get; }
        public string FilePath { set; get; }
        public string ContentType { set; get; }
        public string TitleAr { set; get; }
        public string TitleEn { set; get; }
        public string DescriptionAr { set; get; }
        public string DescriptionEn { set; get; }
        public int ItemOrder { set; get; }
        public int? AttachmentTypeId { set; get; }
        public DateTime? CreatedOn { set; get; }
        public string CreatedBy { set; get; }
        public DateTime? UpdatedOn { set; get; }
        public string UpdatedBy { set; get; }
        public AttachmentType AttachmentType { set; get; }
    }
}
