using ProductManagement.Products;
using Xunit;

namespace ProductManagement.EntityFrameworkCore.Applications;

[Collection(ProductManagementTestConsts.CollectionDefinitionName)]
public class EfCoreProductAppService_Tests : ProductAppService_Tests<ProductManagementEntityFrameworkCoreTestModule>
{
}