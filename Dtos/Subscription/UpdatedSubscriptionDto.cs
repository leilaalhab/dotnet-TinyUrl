using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Dtos.Subscription
{
    public class UpdatedSubscriptionDto
    {
        public SubscriptionTypeClass SubscriptionType { get; set; }

        public long CardNumber { get; set; }

    }
}