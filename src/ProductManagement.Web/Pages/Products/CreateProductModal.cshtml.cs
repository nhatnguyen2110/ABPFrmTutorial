using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductManagement.Products;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Timing;

namespace ProductManagement.Web.Pages.Products
{
    public class CreateProductModalModel : ProductManagementPageModel
    {
        [BindProperty]
        public CreateEditProductViewModel Product { get; set; }

        public SelectListItem[] Categories { get; set; }
        private readonly IProductAppService _productAppService;
        private readonly IClock _clock;

        public CreateProductModalModel(IProductAppService productAppService, IClock clock)
        {
            _productAppService = productAppService;
            _clock = clock;
        }

        public async Task OnGetAsync()
        {
            Product = new CreateEditProductViewModel
            {
                ReleaseDate = _clock.Now,
                StockState = ProductStockState.PreOrder
            };
            var categoryLookup = await _productAppService.GetCategoriesAsync();
            Categories = categoryLookup.Items.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToArray();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var obj = ObjectMapper.Map<CreateEditProductViewModel, CreateUpdateProductDto>(Product);
            await _productAppService.CreateAsync(obj);
            return NoContent();
        }
    }
}