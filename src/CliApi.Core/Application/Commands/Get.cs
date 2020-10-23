using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CliApi.Core.Data.Contracts;
using CliApi.Core.Domain.Models;
using MediatR;

namespace CliApi.Core.Application.Commands
{
    public class Get
    {
        public class GetCommandRequest : IRequest<CommandDto>
        {
            public int Id { get; set; }
        }

        public class GetCommandRequestHandler : IRequestHandler<GetCommandRequest, CommandDto>
        {
            private readonly IDbContextProvider _DbContextProvider;
            private readonly IMapper _mapper;
            public GetCommandRequestHandler(IDbContextProvider DbContextProvider, IMapper mapper)
            {
                _mapper = mapper;
                _DbContextProvider = DbContextProvider;

            }
            public async Task<CommandDto> Handle(GetCommandRequest request, CancellationToken cancellationToken)
            {
                var dbSet = _DbContextProvider.GetContext().Set<Command>();
                var commandModel = await dbSet.FindAsync(request.Id);
                
                if(commandModel == null){
                    throw new System.Exception($"Command with ID: {request.Id} could not be found.");
                }
                
                var commandDto = _mapper.Map<CommandDto>(commandModel);
                return commandDto;
            }
        }
    }
}