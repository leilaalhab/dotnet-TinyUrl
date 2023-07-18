using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _SubscriptionTypeService;
        private readonly ILogger _logger;


        public SubscriptionController(ISubscriptionService characterService, ILogger<SubscriptionController> logger)
        {
            _SubscriptionTypeService = characterService;
            _logger = logger;
        }


        [Authorize]
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> Get() 
        {
            _logger.LogInformation($"Accessing the subscription type of the user {_SubscriptionTypeService.getUserId}");
            return Ok(await _SubscriptionTypeService.GetSubscriptionType()); 
        }
        

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> UpdateSubscription(UpdatedSubscriptionDto updatedSubscriptionDto)
        {   
            _logger.LogInformation($"Updating the subscription type of the user {_SubscriptionTypeService.getUserId}");
            var response = await _SubscriptionTypeService.UpdateSubscription(updatedSubscriptionDto);
            if (response.Data is null){
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}