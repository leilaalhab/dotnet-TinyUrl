using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Services.AnswerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace dotnet_rpg.Controllers
{
    [ApiController] // an attribute added to the controller 
    [Route("api/[controller]")] // how we find the specific controller when we want to call it - can be called by "api/Character"
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService _AnswerService;

        public AnswerController(IAnswerService answerService)
        {
            _AnswerService = answerService;
        }
        

        // in this method we are sending the data via the body of this request, however in the previous method
        // the data was sent via the URL
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetAnswerDto>>>> AddAnswer(AddAnswerDto newAnswer)
        {
            return Ok(await _AnswerService.AddAnswer(newAnswer));
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<List<GetAnswerDto>>>> DeleteAnswer(int Id) {
            var response = await _AnswerService.DeleteAnswer(Id);
            if (response.Data is null){
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<GetAnswerDto>>> UpdateUrlExpiration(UpdatedAnswerDto updatedAnswer)
        {   
            var response = await _AnswerService.UpdateAnswer(updatedAnswer);
            if (response.Data is null){
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}