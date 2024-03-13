﻿using CleanArchMvc.API.Models;
using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IAuthenticate _authenticate;

        public TokenController(IAuthenticate authenticate)
        {
            this._authenticate = authenticate ?? 
                throw new ArgumentNullException(nameof(authenticate));
        }
        [HttpPost("LoginUser")]
        public async Task<ActionResult<UserToken>> Login([FromBody] LoginModel userInfo)
        {
            var result = await _authenticate.Authenticate(userInfo.Email, userInfo.Password);

            if (result)
            {
                //return GenerateToken(userInfo);
                return Ok("Login is ok");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Login attempt.");
                return BadRequest(ModelState);
            }
        }
    }
}
