using CochaVibes.Core.DTOs;
using FluentValidation;

namespace CochaVibes.Services.Validators
{
    public class EventoIdDtoValidator : AbstractValidator<EventoIdDto>
    {
        public EventoIdDtoValidator()
        {
            RuleFor(x => x.IdEvento)
                .GreaterThan(0)
                .WithMessage("El ID del evento debe ser mayor que cero.");
        }
    }
}