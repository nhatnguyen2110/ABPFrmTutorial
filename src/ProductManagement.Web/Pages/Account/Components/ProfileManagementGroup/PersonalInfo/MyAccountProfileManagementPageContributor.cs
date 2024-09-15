using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Account.Localization;
using Volo.Abp.Account.Web.Pages.Account.Components.ProfileManagementGroup.PersonalInfo;
using Volo.Abp.Account.Web.ProfileManagement;

namespace ProductManagement.Web.Pages.Account.Components.ProfileManagementGroup.PersonalInfo
{
    public class MyAccountProfileManagementPageContributor : IProfileManagementPageContributor
    {
        public async Task ConfigureAsync(ProfileManagementPageCreationContext context)
        {
            var l = context.ServiceProvider.GetRequiredService<IStringLocalizer<AccountResource>>();

            var oldPersonalInfo = context.Groups.FirstOrDefault(x => x.Id == "Volo-Abp-Account-PersonalInfo");
            if (oldPersonalInfo != null)
            {
                context.Groups.Remove(oldPersonalInfo);
            }
            context.Groups.Add(
                new ProfileManagementPageGroup(
                    "Volo-Abp-Account-PersonalInfo",
                    l["ProfileTab:PersonalInfo"],
                    typeof(MyAccountProfilePersonalInfoManagementGroupViewComponent)
                )
            );
        }

    }
}
