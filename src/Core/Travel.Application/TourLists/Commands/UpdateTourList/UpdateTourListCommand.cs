using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Travel.Application.Common.Exceptions;
using Travel.Application.Common.Interfaces;
using Travel.Domain.Entities;

namespace Travel.Application.TourLists.Commands.UpdateTourList
{
    public class UpdateTourListCommand : IRequest
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string About { get; set; }
    }

    public class UpdateTourListCommandHandler : IRequestHandler<UpdateTourListCommand>
    {
        private readonly IAppDbContext _context;

        public UpdateTourListCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateTourListCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.TourLists.FindAsync(request.Id);
            if (entity == null)
               throw new NotFoundException(nameof(TourList), request.Id);
            

            entity.City = request.City;
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}