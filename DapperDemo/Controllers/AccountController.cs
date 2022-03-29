using DapperDemo.DB.Repositories;
using DapperDemo.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DapperDemo.Controllers
{
    [Route("api")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository repository;

        public AccountController(IAccountRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost("register")]
        public IActionResult SignUp(AccountDTO account)
        {
            var result = repository.SignUp(account);

            if (result == -1)
                return BadRequest("Username already exists");

            return Ok(result);
        }

        [HttpPost("login")]
        public IActionResult Login(AccountDTO account)
        {
            var token = repository.Login(account);

            if (token == null)
                return BadRequest("Either username or password is incorrect");

            return Ok(token);
        }
    }
}
