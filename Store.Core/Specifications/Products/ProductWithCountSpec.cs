using Store.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Specifications.Products
{
    public class ProductWithCountSpec : BaseSpecification<Product , int>
    {

        // this ctor getting the all product 
        public ProductWithCountSpec(ProductSpecParams productSpecParams) : base(


                // get the number of products that match these conditions to put in in Count var
              P =>
              (string.IsNullOrEmpty(productSpecParams.Search) || P.Name.ToLower().Contains(productSpecParams.Search))
              &&
              (!productSpecParams.BrandId.HasValue || productSpecParams.BrandId == P.BrandId)
              &&
              (!productSpecParams.TypeId.HasValue || productSpecParams.TypeId == P.TypeId)
              )
        {

           

        }
    }
}
