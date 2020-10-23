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
    public class Create
    {
        public class CreateCommandRequest : IRequest<int>
        {
            [Required] [MaxLength(250)] public string HowTo { get; set; }
            [Required] public string Platform { get; set; }
            [Required] public string CommandLine { get; set; }
        }

        public class Handler : IRequestHandler<CreateCommandRequest, int>
        {
            private readonly IDbContextResolver _contextResolver;
            private readonly IMapper _mapper;

            public Handler(IDbContextResolver contextResolver, IMapper mapper)
            {
                _contextResolver = contextResolver;
                _mapper = mapper;
            }
            public async Task<int> Handle(CreateCommandRequest request, CancellationToken cancellationToken)
            {
                var commandModel = _mapper.Map<Command>(request);
                var context = _contextResolver.GetContext();

                await context.Set<Command>().AddAsync(commandModel, cancellationToken);

                var success = await context.SaveChangesAsync(cancellationToken) > 0;

                if (success)
                {
                    return commandModel.Id;
                }
                throw new Exception("There was a problem saving the command.");
            }
        }

    }
}