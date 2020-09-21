using AuditTrailComponent.Persistance.Models;
using AuditTrailComponent.Persistance.UnitOfWork;
using AuditTrailComponent.Services.Abstraction;
using AuditTrailComponent.Services.DTOs;
using AuditTrailComponent.Services.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AuditTrailComponent.Services.Concrete
{
    sealed internal class ErrorLogService : IErrorLogInternalService, IErrorLogPublicService
    {
        IAuditUnitOfWork AuditUnitOfWork { get; }

        public ErrorLogService(IAuditUnitOfWork auditUnitOfWork)
        {
            AuditUnitOfWork = auditUnitOfWork;
        }

        public async Task Log(Exception ex)
        {
            ErrorLog errorLog = new ErrorLog();
            errorLog.Date = DateTime.Now;
            errorLog.StackTrace = ex.StackTrace;
            errorLog.Message = ex.Message + "|" + ex.InnerException?.Message;

            await AuditUnitOfWork.ErrorLogRepository.AddAsync(errorLog);
            await AuditUnitOfWork.SaveAsync();
        }

        public async Task<ErrorLogFilterDto> Search(ErrorLogFilterDto model)
        {
            var expression = GetSearchExpression(model);
            (var query, int totalCount) = await AuditUnitOfWork.ErrorLogRepository.GetPagedByFiltersAsync(
                model.PageNumber,
                model.jtPageSize.Value,
                expression,
                a => a.OrderByDescending(d => d.Date));

            model.Items = query.Select(errorLog => ErrorLogMapper.MapToDto(errorLog)).ToList();

            model.TotalCount = totalCount;

            return model;
        }

        List<Expression<Func<ErrorLog, bool>>> GetSearchExpression(ErrorLogFilterDto model)
        {
            List<Expression<Func<ErrorLog, bool>>> filterList = new List<Expression<Func<ErrorLog, bool>>>();

            if (model.FromDate != null)
                filterList.Add(c => c.Date.Date >= model.FromDate);

            if (model.ToDate != null)
                filterList.Add(c => c.Date.Date <= model.ToDate);

            return filterList;

        }
    }
}
