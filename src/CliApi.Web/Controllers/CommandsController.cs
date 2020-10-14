using System.Windows.Input;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CliApi.Web.Data;
using CliApi.Web.Models;

namespace CliApi.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandRepository _repository;
        public CommandsController(ICommandRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Command>> GetAll() => Ok(_repository.GetAll());

        [HttpGet("{id}")]
        public ActionResult<Command> GetById(int id)
        {
            var commandItem = _repository.GetById(id);
            if (commandItem == null)
            {
                return NotFound();
            }
            return Ok(commandItem);
        }
    }
}