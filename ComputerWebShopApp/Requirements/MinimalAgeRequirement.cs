using ComputerWebShopApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ComputerWebShopApp.Requirements
{
    public class MinimalAgeRequirement : IAuthorizationRequirement
    {
        public int MinimalAge { get; set; }

        public MinimalAgeRequirement(int minimalAge)=> MinimalAge = minimalAge;


        public MinimalAgeRequirement()
        {
            MinimalAge = 16;
        }
    }

    public class MinimalAgeAuthorizationHandler : AuthorizationHandler<MinimalAgeRequirement>
    {
        private readonly UserManager<ShopUser> userManager;

        public MinimalAgeAuthorizationHandler(UserManager<ShopUser> userManager)
        {
            this.userManager = userManager;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, 
            MinimalAgeRequirement requirement)
        {
            if (context.User != null)
            {
                ShopUser? shopUser = await userManager.GetUserAsync(context.User);
                if (shopUser != null)
                {
                    if (DateTime.Now.AddYears(-requirement.MinimalAge) >
                        shopUser.DateOfBirth.ToDateTime(new TimeOnly(0, 0)))
                        context.Succeed(requirement);
                    else
                        context.Fail();
                }
                else context.Fail();
            }
            else
                context.Fail();
        }
    }
}
