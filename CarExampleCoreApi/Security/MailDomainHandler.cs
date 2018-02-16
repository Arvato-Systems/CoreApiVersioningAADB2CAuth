using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarExampleCoreApi.Security
{
    public class MailDomainHandler : AuthorizationHandler<MailDomainRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MailDomainRequirement requirement)
        {
            const string claimname = "emails";
            ClaimsPrincipal user = context.User;

            if (!user.HasClaim(c => c.Type == claimname))
            {
                return Task.CompletedTask;
            }

            var mailadresses = context.User.FindFirst(claimname);

            if (mailadresses.Value.ToLowerInvariant().Contains(requirement.MailDomain.ToLowerInvariant()))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
