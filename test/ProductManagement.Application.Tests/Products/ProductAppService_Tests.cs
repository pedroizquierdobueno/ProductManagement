using Shouldly;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Modularity;
using Volo.Abp.Validation;
using Xunit;

namespace ProductManagement.Products;

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
		// Act
		var output = await _productAppService.GetListAsync(new PagedAndSortedResultRequestDto());

		// Assert
		output.TotalCount.ShouldBe(3);
		output.Items.ShouldContain(i => i.Name.Contains("Acme Monochrome Laser Printer"));
	}
}