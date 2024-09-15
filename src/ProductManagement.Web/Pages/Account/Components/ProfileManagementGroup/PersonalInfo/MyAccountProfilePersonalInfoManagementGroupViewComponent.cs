using Microsoft.AspNetCore.Mvc;
using ProductManagement.Users;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Validation;

namespace Volo.Abp.Account.Web.Pages.Account.Components.ProfileManagementGroup.PersonalInfo;

public class MyAccountProfilePersonalInfoManagementGroupViewComponent : AccountProfilePersonalInfoManagementGroupViewComponent
{
    public MyAccountProfilePersonalInfoManagementGroupViewComponent(
        IProfileAppService profileAppService) : base(profileAppService)
    {
    }

    public override async Task<IViewComponentResult> InvokeAsync()
    {
        var user = await ProfileAppService.GetAsync();

        var model = ObjectMapper.Map<ProfileDto, MyPersonalInfoModel>(user);

        return View("~/Pages/Account/Components/ProfileManagementGroup/PersonalInfo/Default.cshtml", model);
    }

    public class MyPersonalInfoModel : ExtensibleObject, IHasConcurrencyStamp
    {
        [Required]
        [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxUserNameLength))]
        [Display(Name = "DisplayName:UserName")]
        public string UserName { get; set; }

        [Required]
        [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxEmailLength))]
        [Display(Name = "DisplayName:Email")]
        public string Email { get; set; }

        [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxNameLength))]
        [Display(Name = "DisplayName:Name")]
        public string Name { get; set; }

        [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxSurnameLength))]
        [Display(Name = "DisplayName:Surname")]
        public string Surname { get; set; }

        [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxPhoneNumberLength))]
        [Display(Name = "DisplayName:PhoneNumber")]
        public string PhoneNumber { get; set; }

        [DynamicStringLength(typeof(UserConsts), nameof(UserConsts.MaxTitleLength))]
        [Display(Name = "DisplayName:Title")]
        public string Title { get; set; }

        [HiddenInput] public string ConcurrencyStamp { get; set; }
    }
}