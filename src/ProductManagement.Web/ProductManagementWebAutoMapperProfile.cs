using AutoMapper;
using ProductManagement.Products;
using ProductManagement.Web.Pages.Products;
using Volo.Abp.Account;
using Volo.Abp.Account.Web.Pages.Account.Components.ProfileManagementGroup.PersonalInfo;

namespace ProductManagement.Web;

public class ProductManagementWebAutoMapperProfile : Profile
{
    public ProductManagementWebAutoMapperProfile()
    {
        //Define your object mappings here, for the Web project
        CreateMap<CreateEditProductViewModel, CreateUpdateProductDto>();
        CreateMap<ProductDto, CreateEditProductViewModel>();
        CreateMap<ProfileDto, MyAccountProfilePersonalInfoManagementGroupViewComponent.MyPersonalInfoModel>().MapExtraProperties();

    }
}
