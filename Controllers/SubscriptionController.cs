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

        public SubscriptionController(ISubscriptionService characterService)
        {
            _SubscriptionTypeService = characterService;
        }


        [Authorize]
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> Get() 
        {
            return Ok(await _SubscriptionTypeService.GetSubscriptionType()); 
        }
        

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> UpdateSubscription(UpdatedSubscriptionDto updatedSubscriptionDto)
        {   
            var response = await _SubscriptionTypeService.UpdateSubscription(updatedSubscriptionDto);
            if (response.Data is null){
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}