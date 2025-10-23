using Microsoft.AspNetCore.Mvc;
using Application.DTOs.Auth;
using Application.Interfaces.InternalServices;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Application.DTOs.Response;
using MediatR;
using Application.UseCases.Authentication.Queries;
using Application.UseCases.Authentication.Queries.Login;
using Application.UseCases.Authentication.Queries.GetCurrentUserInfo;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

    
        public AuthController( IMediator mediator)
        {
            _mediator = mediator;

        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await _mediator.Send(new LoginQuery(loginDto));

            if (result.StatusCode == 200)
                return Ok(result);
            return StatusCode(result.StatusCode, result);
        }




        //[HttpPost("refresh-token")]
        //public async Task<IActionResult> RefreshToken()
        //{
        //    var refreshToken = Request.Cookies["refreshToken"];
        //    if (string.IsNullOrEmpty(refreshToken))
        //        return Unauthorized(new BaseApiResponse<bool>(401, "Refresh token is missing") { Data = false });

        //    var query = new RefreshTokenQuery(refreshToken);
        //    var response = await _mediator.Send(query);

        //    if (response.StatusCode != 200)
        //        return StatusCode(response.StatusCode, response);

        //    return Ok(response);
        //}


        [HttpGet("current-user")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            // استخراج الإيميل من التوكن
            var email = User?.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(email))
                return Unauthorized(new BaseApiResponse(401, "Invalid token or email not found in claims."));

            var result = await _mediator.Send(new GetCurrentUserInfoQuery(email));

            if (result.StatusCode == 200)
                return Ok(result);
            return StatusCode(result.StatusCode, result);
        }

    
    }
}