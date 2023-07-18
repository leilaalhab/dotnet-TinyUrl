using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace dotnet_rpg.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        private readonly ILogger _logger;

        public AuthController(IAuthRepository authRepo,  ILogger<AuthController> logger)
        {
            _authRepo = authRepo;
            _logger = logger;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto request)
        {
            _logger.LogInformation($"User {request.Username} is trying to register");
            var response = await _authRepo.Register(
                new User{Username = request.Username}, request.Password
            );
            if(!response.Success)
            {
                return BadRequest(response);
            }
            
            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<int>>> Login(UserLoginDto request)
        {
            _logger.LogInformation($"User {request.Username} is trying to log in");
            var response = await _authRepo.Login(
                request.Username, request.Password
            );
            return Ok(response);
        }
    }
}