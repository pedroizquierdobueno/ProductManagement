using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductManagement.Products;
using Volo.Abp.Application.Dtos;
using ProductManagement.Categories;

namespace ProductManagement.Web.Pages.Products;

public class EditProductModalModel : ProductManagementPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }
    [BindProperty]
    public CreateEditProductViewModel Product { get; set; }
    public SelectListItem[] Categories { get; set; }

    private readonly IProductAppService _productAppService;

    public EditProductModalModel(IProductAppService productAppService)
    {
        _productAppService = productAppService;
    }

    public async Task OnGetAsync()
    {
        ProductDto productDto = await _productAppService.GetAsync(Id);

        Product = ObjectMapper.Map<ProductDto, CreateEditProductViewModel>(productDto);

        ListResultDto<CategoryLookupDto> categoryLookup = await _productAppService.GetCategoriesAsync();

        Categories = categoryLookup.Items.Select(i => new SelectListItem(i.Name, i.Id.ToString())).ToArray();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _productAppService.UpdateAsync(Id, ObjectMapper.Map<CreateEditProductViewModel, CreateUpdateProductDto>(Product));

        return NoContent();
    }
}