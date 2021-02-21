using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIHTest.Models;
using EIHTest.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using EIHTest.Interfaces;

namespace EIHTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _service;
        private readonly AppSettings _appSettings;

        public UserController(IUserService userService, IOptions<AppSettings> appSettings)
        {
            _service = userService;
            _appSettings = appSettings.Value;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(User uobj)
        {
            var response = _service.Authenticate(uobj);

            
            if (response == null)
                return BadRequest(new { message = "UserId or password is incorrect" });

            TokenMgr tokenMgr = new TokenMgr();

            var t= tokenMgr.GenerateToken(_appSettings, response.UserId);

            response.Token = t;

            return Ok(response);
        }

        [Authorize]
        [HttpGet("users")]
        public IActionResult GetAll()
        {
            var users = _service.GetAll();
            return Ok(users);
        }
    }
}
