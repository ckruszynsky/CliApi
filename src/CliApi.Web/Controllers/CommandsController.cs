using System;
using System.Collections.Generic;
using System.Windows.Input;
using AutoMapper;
using CliApi.Web.Data;
using CliApi.Web.Dtos;
using CliApi.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CliApi.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandRepository _repository;
        private readonly IMapper _mapper;
        public CommandsController(ICommandRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Command>> GetAll() => Ok(_mapper.Map<IEnumerable<CommandReadDto>>(_repository.GetAll()));

        [HttpGet("{id}")]
        public ActionResult<Command> GetById(int id)
        {
            var commandItem = _repository.GetById(id);
            if (commandItem == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CommandReadDto>(commandItem));
        }
    }
}