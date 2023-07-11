using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Services.UsageService
{
    public interface IUsageService
    {
        Task<ServiceResponse<GetTinyUrlDto>> AddUsage(AddUsageDto newUsage);

    }
}