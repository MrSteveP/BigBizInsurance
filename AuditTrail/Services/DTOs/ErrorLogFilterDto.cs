using System;

namespace AuditTrailComponent.Services.DTOs
{
    public class ErrorLogFilterDto : FilterVModel<ErrorLogDto>
    {
        public DateTime? FromDate { set; get; }
        public DateTime? ToDate { set; get; }
    }
}
