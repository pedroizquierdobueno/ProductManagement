using ProductManagement.Categories;
using Xunit;

namespace ProductManagement.EntityFrameworkCore.Applications;

[Collection(ProductManagementTestConsts.CollectionDefinitionName)]
public class EfCoreCategoryAppService_Tests : CategoryAppService_Tests<ProductManagementEntityFrameworkCoreTestModule>
{
}