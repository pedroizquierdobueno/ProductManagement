using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductManagement.Categories;
using ProductManagement.Products;
using Volo.Abp.Application.Dtos;

namespace ProductManagement.Web.Pages.Products;

public class CreateProductModalModel(IProductAppService productAppService) : ProductManagementPageModel
{
    [BindProperty]
    public CreateEditProductViewModel Product { get; set; }
    public SelectListItem[] Categories { get; set; }

    private readonly IProductAppService _productAppService = productAppService;

    public async Task OnGetAsync()
    {
        Product = new CreateEditProductViewModel
        {
            ReleaseDate = Clock.Now,
            StockState = ProductStockState.PreOrder
        };

        ListResultDto<CategoryLookupDto> categoryLookup = await _productAppService.GetCategoriesAsync();

        Categories = categoryLookup.Items.Select(i => new SelectListItem(i.Name, i.Id.ToString())).ToArray();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _productAppService.CreateAsync(ObjectMapper.Map<CreateEditProductViewModel, CreateUpdateProductDto>(Product));

        return NoContent();
    }
}