using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CliApi.Core.Data.Contracts;
using CliApi.Core.Domain.Models;
using MediatR;

namespace CliApi.Core.Application.Commands
{

    public class Update
    {
        public class UpdateCommandRequest : IRequest
        {
            [Required]
            public int Id { get; set; }
            [Required] [MaxLength(250)] public string HowTo { get; set; }
            [Required] public string Platform { get; set; }
            [Required] public string CommandLine { get; set; }
        }

        public class Handler : IRequestHandler<UpdateCommandRequest>
        {
            private readonly IDbContextProvider _contextResolver;
            private readonly IMapper _mapper;

            public Handler(IDbContextProvider contextResolver, IMapper mapper)
            {
                _contextResolver = contextResolver;
                _mapper = mapper;
            }
            public async Task<Unit> Handle(UpdateCommandRequest request, CancellationToken cancellationToken)
            {
                var context = _contextResolver.GetContext();

                var commandModel = await context.Set<Command>()
                    .FindAsync(request.Id);

                if (commandModel == null)
                {
                    throw new Exception($"The command with ID: {request.Id} was not found and could not be updated.");
                }

                _mapper.Map(request, commandModel);

                var success = await context.SaveChangesAsync(cancellationToken) > 0;

                if (success)
                {
                    return Unit.Value;
                }
                throw new Exception("There was a problem updating the command.");
            }
        }
    }
}