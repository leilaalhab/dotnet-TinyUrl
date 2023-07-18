using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Services.AnswerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace dotnet_rpg.Controllers
{
    [ApiController] 
    [Route("api/[controller]")]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService _AnswerService;

        private readonly ILogger _logger;


        public AnswerController(IAnswerService answerService, ILogger<AnswerController> logger)
        {
            _AnswerService = answerService;
            _logger = logger;
        }
        
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetAnswerDto>>>> AddAnswer(AddAnswerDto newAnswer)
        {
            _logger.LogInformation($"The user {_AnswerService.getUserId()} wants to add an answer to question {newAnswer.questionId}");
            return Ok(await _AnswerService.AddAnswer(newAnswer));
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<List<GetAnswerDto>>>> DeleteAnswer(int Id) {
             _logger.LogInformation($"The user {_AnswerService.getUserId()} wants to delete an answer {Id}");
            var response = await _AnswerService.DeleteAnswer(Id);
            if (response.Data is null){
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<GetAnswerDto>>> UpdateAnswer(UpdatedAnswerDto updatedAnswer)
        {   
             _logger.LogInformation($"The user {_AnswerService.getUserId()} wants to update an answer {updatedAnswer.Id}");
            var response = await _AnswerService.UpdateAnswer(updatedAnswer);
            if (response.Data is null){
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}