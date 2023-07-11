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
    public class TinyUrlController : ControllerBase
    {
        private readonly ITinyUrlService _TinyUrlService;

        public TinyUrlController(ITinyUrlService characterService)
        {
            _TinyUrlService = characterService;
        }

        


        // we must implement the get method to get our game character
        // returns a specific http code along with the requested data
        [Authorize]
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetTinyUrlDto>>>> Get() // assumes its an http method
        {
            return Ok(await _TinyUrlService.GetAllUrls()); // we are also sending the status code 200 ok along with our mock character
        }


        // for a single character
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetTinyUrlDto>>> GetSingle(int id) // assumes its an http method
        {
            return Ok(await _TinyUrlService.GetUrlById(id)); // we are also sending the status code 200 ok along with our mock character
        }
        

        // in this method we are sending the data via the body of this request, however in the previous method
        // the data was sent via the URL
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetTinyUrlDto>>>> AddUrl(AddTinyUrlDto newUrl)
        {
            return Ok(await _TinyUrlService.AddUrl(newUrl));
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<List<GetTinyUrlDto>>>> DeleteUrl(int Id) {
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
            var response = await _TinyUrlService.SetExpiration(updatedTinyUrl);
            if (response.Data is null){
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}