using DapperDemo.DB.Repositories;
using DapperDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DapperDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository repository;

        public EmployeeController(IEmployeeRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Employee employee)
        {
           var result = repository.Add(employee);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = repository.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var result = repository.Get(id);
            return Ok(result);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Employee employee)
        {
           var result = repository.Update(employee);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            repository.Delete(id);
            return Ok();
        }
    }
}
