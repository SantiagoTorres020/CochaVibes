using CochaVibes.Core.DTOs;
using FluentValidation;

namespace CochaVibes.Services.Validators
{
    public class EventoBusquedaDtoValidator : AbstractValidator<EventoBusquedaDto>
    {
        public EventoBusquedaDtoValidator()
        {
            RuleFor(x => x.Texto)
                .MaximumLength(100)
                .WithMessage("El texto de búsqueda no puede exceder los 100 caracteres.");

            RuleFor(x => x.IdCategoria)
                .GreaterThan(0)
                .When(x => x.IdCategoria.HasValue)
                .WithMessage("La categoría debe ser mayor que cero.");

            RuleFor(x => x.IdUbicacion)
                .GreaterThan(0)
                .When(x => x.IdUbicacion.HasValue)
                .WithMessage("La ubicación debe ser mayor que cero.");

            RuleFor(x => x.Fecha)
                .Must(BeAValidDate)
                .When(x => !string.IsNullOrWhiteSpace(x.Fecha))
                .WithMessage("La fecha debe tener un formato válido (yyyy-MM-dd o similar).");
        }

        private bool BeAValidDate(string? fecha)
        {
            if (string.IsNullOrWhiteSpace(fecha))
                return true;

            return DateTime.TryParse(fecha, out _);
        }
    }
}