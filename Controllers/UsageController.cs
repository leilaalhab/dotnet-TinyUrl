using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
   
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsageController : ControllerBase
    {
       private readonly  IUsageService _usageService;
        public UsageController(IUsageService usageService)
        {
            _usageService = usageService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetUsageDto>>> Addusage(AddUsageDto newUsage) {
            return Ok(await _usageService.AddUsage(newUsage));
        }
    }
}