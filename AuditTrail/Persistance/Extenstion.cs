using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;

namespace AuditTrailComponent.Persistance
{
    public static class Extenstion
    {
        public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query, IEnumerable<Expression<Func<T, object>>> inculdes) where T : class
        {
            if (inculdes?.Count() > 0)
            {
                foreach (var include in inculdes)
                {
                    query.Include(include);
                }
            }

            return query;
        }

        public static Dictionary<string, List<ValidationResult>> ValidateEntities(this DbContext context)
        {
            var entities = (from entry in context.ChangeTracker.Entries()
                            where entry.State == EntityState.Modified || entry.State == EntityState.Added
                            select entry.Entity);
            var EntitiesValidations = new Dictionary<string, List<ValidationResult>>();
            var validationResults = new List<ValidationResult>();
            foreach (var entity in entities)
            {
                if (!Validator.TryValidateObject(entity, new ValidationContext(entity), validationResults))
                {
                    EntitiesValidations.Add(entity.GetType().ToString(), validationResults);
                    //foreach (var validationError in validationResults)
                    //{
                    //    var errorMessage =
                    //        $"Entity: {entity.GetType().ToString()}\nProperty: {validationError.MemberNames}\n{validationError.ErrorMessage}";
                    //    var EntitiesException = new Exception(errorMessage);
                    //    EntitiesException.Data["PropertyName"] = validationError.MemberNames;
                    //    EntitiesException.Data["EntityName"] = entity;
                    //    // Logger.Error(exception, errorMessage);
                    //    throw EntitiesException;
                    //}
                }
            }

            return EntitiesValidations;
        }

    }

}
