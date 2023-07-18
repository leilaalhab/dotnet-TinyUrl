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

        private readonly ILogger _logger;

        public BillController(IBillService billService, ILogger<BillController> logger)
        {
            _BillService = billService;

            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetBillDto>>>> Get() 
        {
            _logger.LogInformation($"getting all bills of user {_BillService.getUserId()}");
            return Ok(await _BillService.GetBills()); 
        }

    }
}