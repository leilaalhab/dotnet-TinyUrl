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
    [Route("api/[controller]")] // how we find the specific controller when we want to call it - can be called by "api/TinyUrl"
    public class TinyUrlController : ControllerBase
    {
        private readonly ITinyUrlService _TinyUrlService;

        private readonly ILogger _logger;


        public TinyUrlController(ITinyUrlService characterService, ILogger<TinyUrlController> logger)
        {
            _TinyUrlService = characterService;
            _logger = logger;
        }

        
        [Authorize]
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetTinyUrlDto>>>> Get() 
        {    
            _logger.LogInformation($"User {_TinyUrlService.getUserId()} is trying to access all of their tinyURLs");
            var result = await _TinyUrlService.GetAllUrls();
            return Ok(result); 
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetTinyUrlDto>>> GetSingle(int Id) 
        {
            _logger.LogInformation($"User {_TinyUrlService.getUserId()} is trying to access tinyURL with Id {Id}");
            return Ok(await _TinyUrlService.GetUrlById(Id)); 
        }
        
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetTinyUrlDto>>>> AddUrl(AddTinyUrlDto newUrl)
        {
             _logger.LogInformation($"User {_TinyUrlService.getUserId()} is trying to add a new URL");
            return Ok(await _TinyUrlService.AddUrl(newUrl));
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<List<GetTinyUrlDto>>>> DeleteUrl(int Id) {
             _logger.LogInformation($"User {_TinyUrlService.getUserId()} is trying to delete tinyURL with Id {Id}");
            var response = await _TinyUrlService.DeleteUrl(Id);
            if (response.Data is null){
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<GetTinyUrlDto>>> UpdateUrlExpiration(UpdatedTinyUrlDto updatedTinyUrl)
        {   
            _logger.LogInformation($"User {_TinyUrlService.getUserId()} is trying to update tinyURL with Id {updatedTinyUrl.Id}");
            var response = await _TinyUrlService.SetExpiration(updatedTinyUrl);
            if (response.Data is null){
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}