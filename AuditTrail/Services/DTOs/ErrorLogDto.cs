using AuditTrailComponent.Persistance.Models;
using AuditTrailComponent.Services.Mapper;
using System;

namespace AuditTrailComponent.Services.DTOs
{
    public class ErrorLogDto
    {
        public int Id { set; get; }
        public DateTime Date { set; get; }
        public string StackTrace { set; get; }
        public string Message { set; get; }
        public static implicit operator ErrorLogDto(ErrorLog errorLog)
        {
            return ErrorLogMapper.MapToDto(errorLog);
        }

        public static implicit operator ErrorLog(ErrorLogDto errorLog)
        {
            return ErrorLogMapper.MapToModel(errorLog);
        }
    }
}
