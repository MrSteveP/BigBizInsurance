using System;
using System.Collections.Generic;
using System.Text;

namespace BigBizInsurance.Domain.Models
{
   public  class TenantModel
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public bool Active { set; get; }
    }
}
