using AutoMapper;
using Store.Core;
using Store.Core.Dtos;
using Store.Core.Dtos.Products;
using Store.Core.Entities;
using Store.Core.ResponseStyle;
using Store.Core.Services.Contract;
using Store.Core.Specifications.Products;
using Store.Repository.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginationResponse<ProductDto>> GetAllProductsAsync(ProductSpecParams productSpecParams)
        {
            var spec = new ProductSpec(productSpecParams);
            // convert to ProductDto
            var products  = await _unitOfWork.Repository<Product , int>().GetAllWithSpecificationsAsync(spec);

            var mappedProducts = _mapper.Map<IEnumerable<ProductDto>>(products);
            
            // create a class has only Filteration and create new object from it :
            var countSpec = new ProductWithCountSpec(productSpecParams);
            
            var count = await _unitOfWork.Repository<Product, int>().GetCountAsync(countSpec);
            
            return new PaginationResponse<ProductDto>(productSpecParams.PageSize, productSpecParams.PageIndex ,count , mappedProducts);
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            var spec = new ProductSpec(id);
            var product = await _unitOfWork.Repository<Product, int>().GetByIdWithSpecificationsAsync(spec);
            var mappedProduct = _mapper.Map<ProductDto>(product);
            return mappedProduct;
        } 

        public async Task<IEnumerable<TypesAndBrandsDto>> GetAllBrandsAsync()
        {
            return _mapper.Map<IEnumerable<TypesAndBrandsDto>>(await _unitOfWork.Repository<ProductBrand, int>().GetAllAsync());
        }


        public async Task<IEnumerable<TypesAndBrandsDto>> GetAllTypesAsync()
        {
            return _mapper.Map<IEnumerable<TypesAndBrandsDto>>(await _unitOfWork.Repository<ProductType, int>().GetAllAsync());
        }

       



        // With Specifications :


    }
}
