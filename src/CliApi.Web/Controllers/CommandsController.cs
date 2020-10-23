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

        [HttpGet("{id}", Name = "Get")]
        public ActionResult<CommandDto> Get(int id)
        {
            var commandItem = _repository.GetById(id);
            if (commandItem == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CommandDto>(commandItem));
        }

        [HttpPost]
        public async Task<ActionResult<CommandDto>> Create(Create.CreateCommandRequest request)
        {
            var id = await Mediator.Send(request);

            return CreatedAtRoute( nameof(Get),new{ Id = id},request);
        }

        [HttpPut]
        public async Task<ActionResult> Update(int id, Update.UpdateCommandRequest request)
        {

            request.Id = id;
            await Mediator.Send(request);
            return NoContent();
        }
               
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send
            (
                new Delete.DeleteCommandRequest
                {
                    Id = id
                }
            );
            return NoContent();
        }
    }
}