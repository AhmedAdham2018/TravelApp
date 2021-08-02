using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Travel.Application.Common.Interfaces;

namespace Travel.Application.TourLists.Commands.CreateTourList
{
    public class CreateTourListCommandValidator : AbstractValidator<CreateTourListCommand>
    {
        private readonly IAppDbContext _context;

        public CreateTourListCommandValidator(IAppDbContext context)
        {
            _context = context;

            RuleFor(v => v.City)
                .NotEmpty()
                .WithMessage("City is required.")
                .MaximumLength(200)
                .WithMessage("City must not exceed 90 characters.");

            RuleFor(v => v.Country)
                .NotEmpty().WithMessage("Country is required")
                .MaximumLength(200).WithMessage("Country must not exceed 60 characters.");

            RuleFor(v => v.About)
                .NotEmpty().WithMessage("About is required");
        }

    }
}
