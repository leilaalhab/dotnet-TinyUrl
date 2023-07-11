using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Services.SubscriptionService
{
    public interface ISubscriptionService
    {
        Task<ServiceResponse<GetUserDto>> UpdateSubscription(UpdatedSubscriptionDto updatedSubscriptionDto);

        Task<ServiceResponse<GetUserDto>> GetSubscriptionType();


    }
}