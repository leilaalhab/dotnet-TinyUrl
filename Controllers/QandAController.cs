using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Services.TinyUrlService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace dotnet_rpg.Controllers
{
    [ApiController] // an attribute added to the controller 
    [Route("api/[controller]")] // how we find the specific controller when we want to call it - can be called by "api/Character"
    public class QandAController : ControllerBase
    {
        private readonly IQandAService _QandAService;

        public QandAController(IQandAService qandAService)
        {
            _QandAService = qandAService;
        }

        


        // we must implement the get method to get our game character
        // returns a specific http code along with the requested data
        [Authorize]
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetQandADto>>>> Get() // assumes its an http method
        {
            return Ok(await _QandAService.GetQandAs()); // we are also sending the status code 200 ok along with our mock character
        }


        // for a single character
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetQandADto>>> GetSingle(int id) // assumes its an http method
        {
            return Ok(await _QandAService.GetQandAbyId(id)); // we are also sending the status code 200 ok along with our mock character
        }
        

        // in this method we are sending the data via the body of this request, however in the previous method
        // the data was sent via the URL
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetQandADto>>>> AddUrl(AddQandADto newQuestion)
        {
            return Ok(await _QandAService.AddQuestion(newQuestion));
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<List<GetQandADto>>>> DeleteQandA(int Id) {
            var response = await _QandAService.DeleteQuestion(Id);
            if (response.Data is null){
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<GetQandADto>>> UpdateQandA(UpdatedQandADto updatedQuestion)
        {   
            var response = await _QandAService.UpdateQuestion(updatedQuestion);
            if (response.Data is null){
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}