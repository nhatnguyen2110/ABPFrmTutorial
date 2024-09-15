using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ProductManagement.Categories;
using ProductManagement.Permissions;
using ProductManagement.Settings;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace ProductManagement.Products
{
    public class ProductAppService : ProductManagementAppService, IProductAppService
    {
        private readonly IRepository<Product, Guid> _productRepository;
        private readonly IRepository<Category, Guid> _categoryRepository;
        private readonly IConfiguration _configuration;
        private readonly AzureSmsServiceOptions _options;
        public ProductAppService(
            IRepository<Product, Guid> productRepository, IRepository<Category, Guid> categoryRepository, IConfiguration configuration, IOptions<AzureSmsServiceOptions> options)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _configuration = configuration;
            _options = options.Value;
        }
        [Authorize(ProductManagementPermissions.ProductCreation)]
        public async Task CreateAsync(CreateUpdateProductDto input)
        {
            await _productRepository.InsertAsync(
                    ObjectMapper.Map<CreateUpdateProductDto, Product>(input)
            );
        }
        [Authorize(ProductManagementPermissions.ProductDeletion)]
        public async Task DeleteAsync(Guid id)
        {
            await _productRepository.DeleteAsync(id);
        }

        public async Task<ProductDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Product, ProductDto>(await _productRepository.GetAsync(id));
        }

        public async Task<ListResultDto<CategoryLookupDto>> GetCategoriesAsync()
        {
            var categories = await _categoryRepository.GetListAsync();
            return new ListResultDto<CategoryLookupDto>(
                ObjectMapper.Map<List<Category>, List<CategoryLookupDto>>(categories)
            );
        }

        public async Task<PagedResultDto<ProductDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            string sender = _options.Sender;
            string ConnectionString = _options.ConnectionString;


            var queryable = await _productRepository
                .WithDetailsAsync(x => x.Category);
            queryable = (System.Linq.IQueryable<Product>)queryable
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .OrderBy(input.Sorting ?? nameof(Product.Name));
            var products = await
                AsyncExecuter.ToListAsync(queryable);
            var count = await _productRepository.GetCountAsync();
            return new PagedResultDto<ProductDto>(count, ObjectMapper.Map<List<Product>, List<ProductDto>>(products));
        }
        [Authorize(ProductManagementPermissions.ProductUpdation)]
        public async Task UpdateAsync(Guid id, CreateUpdateProductDto input)
        {
            //await AuthorizationService.CheckAsync(ProductManagementPermissions.ProductUpdation);
            //var result = await AuthorizationService.AuthorizeAsync(ProductManagementPermissions.ProductUpdation);
            ////var result = await AuthorizationService.i(ProductManagementPermissions.ProductUpdation);
            //if (!result.Succeeded)
            //{
            //    // logic
            //    throw new AbpAuthorizationException("...");
            //}
            var product = await _productRepository.GetAsync(id);
            ObjectMapper.Map(input, product);
        }
    }
}
