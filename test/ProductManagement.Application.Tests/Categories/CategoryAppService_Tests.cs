using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Modularity;
using Xunit;

namespace ProductManagement.Categories;

public abstract class CategoryAppService_Tests<TStartupModule> : ProductManagementApplicationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly ICategoryAppService _categoryAppService;

    protected CategoryAppService_Tests()
    {
        _categoryAppService = GetRequiredService<ICategoryAppService>();
    }

    [Fact]
    public async Task Should_Get_Category_List()
    {
        // Act
        var output = await _categoryAppService.GetListAsync(new PagedAndSortedResultRequestDto());

        // Assert
        output.TotalCount.ShouldBe(2);
        output.Items.ShouldContain(i => i.Name.Contains("Monitors"));
    }
}