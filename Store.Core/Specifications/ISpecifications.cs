using Store.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Specifications
{
    public interface ISpecifications<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public Expression<Func<TEntity, bool>> Filter { get; set; } // = Where 

        public List<Expression<Func<TEntity, object>>> Includes { get; set; } // = Include 

        public Expression<Func<TEntity, object>> OrderByAsec  { get; set; }

        public Expression<Func<TEntity, object>> OrderByDesc  { get; set; }

        public int Take { get; set; }

        public int Skip { get; set; }

        public bool IsPaginationEnabled { get; set; }



    }

        // _context.Products.Where(P => P.Id == id as int?).Include(P => P.Type).Include(P => P.Brand).FirstOrDefaultAsync(P => P.Id == id as int?) as TEntity;
}
