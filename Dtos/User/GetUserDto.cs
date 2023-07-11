using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Dtos.User
{
    public class GetUserDto
    {
        public string  Username { get; set; } = string.Empty;

        public SubscriptionTypeClass subscriptionType {get; set; }

    }
}