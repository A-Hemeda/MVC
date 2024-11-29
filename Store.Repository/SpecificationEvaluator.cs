using Microsoft.EntityFrameworkCore;
using Store.Core.Entities;
using Store.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository
{
    public static class SpecificationEvaluator<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        // Create and return Query
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity, TKey> specifications)
        {
            var query = inputQuery;

            if (specifications.Filter is not null)
            {
                query = query.Where(specifications.Filter);

            }

            if(specifications.OrderByAsec is not null)
            {
                query = query.OrderBy(specifications.OrderByAsec);
            }

            if(specifications.OrderByDesc is not null)
            {
                query = query.OrderByDescending(specifications.OrderByDesc);
            }

            if(specifications.IsPaginationEnabled)
            {
                query = query.Skip(specifications.Skip).Take(specifications.Take);
            }

            query = specifications.Includes.Aggregate(query,(currentQuery, IncludeQuery) => currentQuery.Include(IncludeQuery));

            return query;
        }
        // the fixed part here in every query is >> [_Context.Products] ==> Nname of DbConetxt and Name of Table 
        // _context.Products.Where(P => P.Id == id as int?).Include(P => P.Type).Include(P => P.Brand).FirstOrDefaultAsync(P => P.Id == id as int?) as TEntity;

    }
}
