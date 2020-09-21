using AuditTrailComponent.Persistance.Models;
using AuditTrailComponent.Services.DTOs;

namespace AuditTrailComponent.Services.Mapper
{
    public static class ErrorLogMapper
    {
        public static ErrorLogDto MapToDto(ErrorLog errorLog)
        {
            return new ErrorLogDto
            {
                Date = errorLog.Date,
                Id = errorLog.Id,
                Message = errorLog.Message,
                StackTrace = errorLog.StackTrace
            };
        }

        public static ErrorLog MapToModel(ErrorLogDto errorLog)
        {
            return new ErrorLog
            {
                Date = errorLog.Date,
                Id = errorLog.Id,
                Message = errorLog.Message,
                StackTrace = errorLog.StackTrace
            };
        }
    }
}
