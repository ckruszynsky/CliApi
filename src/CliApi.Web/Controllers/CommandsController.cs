using System;
using System.Collections.Generic;
using System.Windows.Input;
using AutoMapper;
using CliApi.Web.Data;
using CliApi.Web.Dtos;
using CliApi.Web.Models;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpGet("{id}", Name = "GetById")]
        public ActionResult<Command> GetById(int id)
        {
            var commandItem = _repository.GetById(id);
            if (commandItem == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CommandReadDto>(commandItem));
        }

        [HttpPost]
        public ActionResult<CommandReadDto> Create(CommandCreateDto commandCreateDto)
        {
            var commandModel = _mapper.Map<Command>(commandCreateDto);
            _repository.Create(commandModel);
            _repository.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);

            return CreatedAtRoute(nameof(GetById), new
            {
                Id = commandReadDto.Id
            }, commandReadDto);
        }

        [HttpPut]
        public ActionResult Update(int id, CommandUpdateDto commandUpdateDto)
        {
            var commandModel = _repository.GetById(id);
            if (commandModel == null)
            {
                return NotFound();
            }

            _mapper.Map(commandUpdateDto, commandModel);
            _repository.Update(commandModel);
            _repository.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDocument)
        {
            var commandModel = _repository.GetById(id);
            if (commandModel == null)
            {
                return NotFound();
            }
            var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModel);
            patchDocument.ApplyTo(commandToPatch, ModelState);
            if (!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(commandToPatch, commandModel);
            _repository.Update(commandModel);
            _repository.SaveChanges();
            return NoContent();
        }
    }
}