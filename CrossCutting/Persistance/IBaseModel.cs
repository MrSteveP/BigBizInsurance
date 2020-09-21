using System;
using System.Collections.Generic;
using System.Text;

namespace CrossCutting.Persistance
{
    public interface IBaseModel
    {
        public string CreatedBy { set; get; }
        public DateTime? CreatedOn { set; get; }


        public string UpdatedBy { set; get; }
        public DateTime? UpdatedOn { set; get; }
    }
}
