using CrossCutting.Persistance;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace BigBizInsurance.Domain.Models
{
    public class SampleModel : BaseTenantInfo, IBaseModel
    {

        public SampleModel()
        {

        }
        public int Id { set; get; }
        public string Name { set; get; }
        public bool Active { set; get; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        //[JsonIgnore]
        //[IgnoreDataMember]
        //public ICollection<TeacherMaterial> TeacherMaterials { set; get; }


    }
}
