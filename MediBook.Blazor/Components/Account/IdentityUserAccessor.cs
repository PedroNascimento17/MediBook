using MediBook.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace MediBook.Blazor.Components.Account
{
    internal sealed class IdentityUserAccessor(UserManager<ApplicationUser> userManager, IdentityRedirectManager redirectManager)
    {
        public async Task<ApplicationUser?> GetRequiredUserAsync(HttpContext context)
        {
            var user = await userManager.GetUserAsync(context.User);
            return user;
        }
    }
}
