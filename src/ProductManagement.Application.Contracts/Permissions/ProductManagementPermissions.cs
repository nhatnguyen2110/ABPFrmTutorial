namespace ProductManagement.Permissions;

public static class ProductManagementPermissions
{
    public const string GroupName = "ProductManagement";



    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";
    public const string ProductCreation = GroupName + ".ProductCreation";
    public const string ProductDeletion = GroupName + ".ProductDeletion";
    public const string ProductUpdation = GroupName + ".ProductUpdation";
}
