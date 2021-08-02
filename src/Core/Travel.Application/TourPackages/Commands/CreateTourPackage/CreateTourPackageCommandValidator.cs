using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Travel.Application.Common.Interfaces;

namespace Travel.Application.TourPackages.Commands.CreateTourPackage
{
    public class CreateTourPackageCommandValidator : AbstractValidator<CreateTourPackageCommand>
    {
        private readonly IAppDbContext _context;

        public CreateTourPackageCommandValidator(IAppDbContext context)
        {
            _context = context;

            RuleFor(v => v.Name)
               .NotEmpty().WithMessage("Name is required.")
               .MaximumLength(200).WithMessage("Name must not exceed 200 characters.")
               .MustAsync(BeUniqueName).WithMessage("The specified name already exists.");

            RuleFor(v => v.ListId)
               .NotEmpty().WithMessage("ListId is required");
        }

        public async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
        {
            return await _context.TourPackages
              .AllAsync(list => list.Name != name);
        }
    }
}
