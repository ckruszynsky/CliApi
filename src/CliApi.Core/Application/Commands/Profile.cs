using AutoMapper;
using CliApi.Core.Domain.Models;

namespace CliApi.Core.Application.Commands
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            CreateMap<Command, CommandDto>();
            CreateMap<Create.CreateCommandRequest, Command>();
            CreateMap<Update.UpdateCommandRequest, Command>();
            CreateMap<Command, Update.UpdateCommandRequest>();
        }
    }
}