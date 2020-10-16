using AutoMapper;
using CliApi.Web.Dtos;
using CliApi.Web.Models;

namespace CliApi.Web.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            CreateMap<Command, CommandReadDto>();
        }
    }
}