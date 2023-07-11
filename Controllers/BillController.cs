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
    public class BillController : ControllerBase
    {
        private readonly IBillService _BillService;

        public BillController(IBillService billService)
        {
            _BillService = billService;
        }


        // we must implement the get method to get our game character
        // returns a specific http code along with the requested data
        [Authorize]
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetBillDto>>>> Get() // assumes its an http method
        {
            return Ok(await _BillService.GetBills()); // we are also sending the status code 200 ok along with our mock character
        }

    }
}