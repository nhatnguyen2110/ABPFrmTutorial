using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Modularity;
using Xunit;

namespace ProductManagement.Products
{
    public abstract class ProductAppService_Tests<TStartupModule> : ProductManagementApplicationTestBase<TStartupModule> 
        where TStartupModule : IAbpModule
    {
        private readonly IProductAppService _productAppService;

        protected ProductAppService_Tests()
        {
            _productAppService = GetRequiredService<IProductAppService>();
        }

        [Fact]
        public async Task Should_Get_Product_List()
        {
            var output = await _productAppService.GetListAsync(
                new PagedAndSortedResultRequestDto()
                );
            output.TotalCount.ShouldBe(3);
            output.Items.ShouldContain(x => x.Name.Contains("Acme Monochrome Laser Printer"));
        }
    }
}