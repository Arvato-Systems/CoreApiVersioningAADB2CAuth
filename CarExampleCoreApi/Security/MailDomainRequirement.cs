using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarExampleCoreApi.Security
{
    public class MailDomainRequirement : IAuthorizationRequirement
    {
        public MailDomainRequirement(string mailDomain)
        {
            MailDomain = mailDomain;
        }

        public string MailDomain { get; set; }
    }
}
