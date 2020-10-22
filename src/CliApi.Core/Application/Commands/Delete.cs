using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CliApi.Core.Data.Contracts;
using CliApi.Core.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CliApi.Core.Application.Commands
{
    public class Delete
    {
        public class DeleteCommandRequest : IRequest
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<DeleteCommandRequest>
        {
            private readonly IDbContextResolver _dbContextResolver;

            public Handler(IDbContextResolver dbContextResolver)
            {
                _dbContextResolver = dbContextResolver;
            }
            public async Task<Unit> Handle(DeleteCommandRequest request, CancellationToken cancellationToken)
            {
                DbSet<Command> dbSet = _dbContextResolver.GetContext()
                    .Set<Command>();

                var commandModel = await dbSet.FindAsync(request.Id);

                if(commandModel == null)
                {
                    throw new Exception($"Command with ID: {request.Id} could not be found.");
                }

                dbSet.Remove(commandModel);

                var success = await _dbContextResolver.GetContext().SaveChangesAsync(cancellationToken) > 0;

                if(success)
                {
                    return Unit.Value;
                }

                throw new Exception($"Command with ID: {request.Id} could not be deleted.");

            }
        }
    }
}
