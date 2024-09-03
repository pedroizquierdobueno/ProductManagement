using ProductManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace ProductManagement.Permissions;

public class ProductManagementPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        PermissionGroupDefinition myGroup = context.AddGroup(ProductManagementPermissions.GroupName, L("ProductManagement"));
        
        // Define permissions here
        myGroup.AddPermission(ProductManagementPermissions.ProductCreation, L("Permission:ProductCreation"));
        myGroup.AddPermission(ProductManagementPermissions.ProductEdition, L("Permission:ProductEdition"));
        myGroup.AddPermission(ProductManagementPermissions.ProductDeletion, L("Permission:ProductDeletion"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ProductManagementResource>(name);
    }
}