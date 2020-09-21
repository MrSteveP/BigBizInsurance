using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Domain.Module
{
    [Table("SystemSettings", Schema = "common")]
    public class SystemSetting
    {
        public int Id { get; set; }
        public string ApplicationId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string ValueType { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string GroupName { get; set; }
        public bool IsActive { get; set; }
        public bool IsSticky { get; set; }
        public bool Secure { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
     
        public string Desc_En { get; set; }
        public string Desc_Ar { get; set; }
        public bool IsManaged { set; get; }
    }
}
