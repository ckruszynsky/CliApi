using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CliApi.Core.Data.Contracts;
using CliApi.Core.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CliApi.Core.Application.Commands
{
    public class List
    {
        public class CommandEnvelope
        {
            public List<CommandDto> Commands { get; set; }
            public int Count { get; set; }
        }

        public class Query : IRequest<CommandEnvelope>
        {
            public int? Limit { get; set; }
            public int? Offset { get; set; }
        }

        public class Handler : IRequestHandler<Query, CommandEnvelope>
        {
            private readonly IDbContextResolver _contextResolver;
            private readonly IMapper _mapper;

            public Handler(IDbContextResolver contextResolver, IMapper mapper)
            {
                _contextResolver = contextResolver;
                _mapper = mapper;
            }
            public async Task<CommandEnvelope> Handle(Query request, CancellationToken cancellationToken)
            {
                var context = _contextResolver.GetContext();

                var queryable = context.Set<Command>().AsQueryable();

                var commands = await queryable
                    .Skip(request.Offset ?? 0)
                    .Take(request.Limit ?? 25)
                    .ToListAsync(cancellationToken);

                return new CommandEnvelope
                {
                    Commands = _mapper.Map<List<CommandDto>>(commands),
                    Count = queryable.Count()
                };
            }
        }

    }
}