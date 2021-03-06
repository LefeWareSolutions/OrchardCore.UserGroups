using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;

namespace OrchardCore.UserGroup
{
    public class AdminUserGroup : INavigationProvider
    {
        private readonly IStringLocalizer S;

        public AdminUserGroup(IStringLocalizer<AdminUserGroup> localizer)
        {
            S = localizer;
        }

        public Task BuildNavigationAsync(string name, NavigationBuilder builder)
        {
            if (!String.Equals(name, "admin", StringComparison.OrdinalIgnoreCase))
            {
                return Task.CompletedTask;
            }

            var rvd = new RouteValueDictionary
            {
                { "Area", "OrchardCore.Contents" },
                { "Options.SelectedContentType", "UserGroup" },
                { "Options.CanCreateSelectedContentType", true }
            };

            builder.Add(S["Security"], design => design
                    .Add(S["User Groups"], "1.4", menus => menus
                        .Permission(Permissions.ManageUserGroups)
                        .Action("List", "Admin", rvd)
                        .LocalNav()
                        ));

            return Task.CompletedTask;
        }
    }
}
