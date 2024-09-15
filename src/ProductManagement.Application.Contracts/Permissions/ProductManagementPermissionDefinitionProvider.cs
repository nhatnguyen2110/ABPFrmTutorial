using ProductManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace ProductManagement.Permissions;

public class ProductManagementPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(ProductManagementPermissions.GroupName, L(ProductManagementPermissions.GroupName));

        //Define your own permissions here. Example:
        //myGroup.AddPermission(ProductManagementPermissions.MyPermission1, L("Permission:MyPermission1"));
        myGroup.AddPermission(ProductManagementPermissions.ProductCreation, L(ProductManagementPermissions.ProductCreation));
        myGroup.AddPermission(ProductManagementPermissions.ProductDeletion, L(ProductManagementPermissions.ProductDeletion));
        myGroup.AddPermission(ProductManagementPermissions.ProductUpdation, L(ProductManagementPermissions.ProductUpdation));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ProductManagementResource>(name);
    }
}
