using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace CrossCutting.Core
{
    public static class EntityFrameworkExtensions
    {
        public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query, IEnumerable<Expression<Func<T, object>>> inculdes) where T : class
        {
            if (inculdes?.Count() > 0)
            {
                foreach (var include in inculdes)
                {
                    query= query.Include(include);
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
                 
                }
            }

            return EntitiesValidations;
        }

        public static List<T> MapToList<T>(this IDataReader dr)
        {
            List<T> list = new List<T>();
            T obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (!object.Equals(dr[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, dr[prop.Name], null);
                    }
                }
                list.Add(obj);
            }
            return list;
        }
    }
}
