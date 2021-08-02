using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Travel.Application.Common.Exceptions;
using Travel.Application.Common.Interfaces;
using Travel.Domain.Entities;

namespace Travel.Application.TourPackages.Commands.DeleteTourPackage
{
    public class DeleteTourPackageCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteTourPackageCommandHandler : IRequestHandler<DeleteTourPackageCommand>
    {
        private readonly IAppDbContext _context;

        public DeleteTourPackageCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteTourPackageCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.TourPackages
                .Where(t => t.Id == request.Id)
                .SingleOrDefaultAsync();

            if(entity == null) throw new NotFoundException(nameof(TourPackage), request.Id);

            _context.TourPackages.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
