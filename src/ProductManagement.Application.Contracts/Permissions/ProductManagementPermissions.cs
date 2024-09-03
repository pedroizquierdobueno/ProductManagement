namespace ProductManagement.Permissions;

public static class ProductManagementPermissions
{
    public const string GroupName = "ProductManagement";

    // Add new permission names
    public const string ProductCreation = GroupName + ".Creation";
    public const string ProductEdition = GroupName + ".Edition";
    public const string ProductDeletion = GroupName + ".Deletion";
}
