using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Services.TinyUrlService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace dotnet_rpg.Controllers
{
    [ApiController] 
    [Route("api/[controller]")] 
    public class QandAController : ControllerBase
    {
        private readonly IQandAService _QandAService;

        private readonly ILogger _logger;

        public QandAController(IQandAService qandAService, ILogger<QandAController> logger)
        {
            _QandAService = qandAService;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetQandADto>>>> Get()
        {
             _logger.LogInformation($"User {_QandAService.getUserId()} is trying to access all of their questions");
            return Ok(await _QandAService.GetQandAs());
        }


        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetQandADto>>> GetSingle(int id) 
        {
            _logger.LogInformation($"User {_QandAService.getUserId()} is trying to access their question {id}");

            return Ok(await _QandAService.GetQandAbyId(id));
        }
        

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetQandADto>>>> AddQuestion(AddQandADto newQuestion)
        {
            _logger.LogInformation($"User {_QandAService.getUserId()} is trying to add a new question");

            return Ok(await _QandAService.AddQuestion(newQuestion));
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<List<GetQandADto>>>> DeleteQandA(int Id) {
            _logger.LogInformation($"User {_QandAService.getUserId()} is trying to delete their question {Id}");
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
            _logger.LogInformation($"User {_QandAService.getUserId()} is trying to update their question {updatedQuestion.Id}");
            var response = await _QandAService.UpdateQuestion(updatedQuestion);
            if (response.Data is null){
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}