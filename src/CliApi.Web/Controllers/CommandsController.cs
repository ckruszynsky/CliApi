using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CliApi.Core.Data;
using CliApi.Core.Domain.Models;
using CliApi.Core.Application.Commands;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CliApi.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : BaseController
    {
        private readonly ICommandRepository _repository;
        private readonly IMapper _mapper;
        public CommandsController(ICommandRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List.CommandEnvelope>> GetAll() 
        {
            return await Mediator.Send(new List.Query());
        }

        [HttpGet("{id}", Name = "GetById")]
        public ActionResult<CommandDto> GetById(int id)
        {
            var commandItem = _repository.GetById(id);
            if (commandItem == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CommandDto>(commandItem));
        }

        [HttpPost]
        public ActionResult<CommandDto> Create(CommandCreateDto commandCreateDto)
        {
            var commandModel = _mapper.Map<Command>(commandCreateDto);
            _repository.Create(commandModel);
            _repository.SaveChanges();

            var commandReadDto = _mapper.Map<CommandDto>(commandModel);

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

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var commandModel = _repository.GetById(id);
            if (commandModel == null)
            {
                return NotFound();
            }

            _repository.Delete(commandModel);
            _repository.SaveChanges();
            return NoContent();
        }
    }
}