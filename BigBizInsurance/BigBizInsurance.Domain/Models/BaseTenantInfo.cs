using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BigBizInsurance.Domain.Models
{
    public abstract class BaseTenantInfo
    {
        public int TenantId { set; get; }
    }
}
