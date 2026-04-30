using CochaVibes.Core.QueryFilters;
using FluentValidation;

namespace CochaVibes.Services.Validators
{
    public class ComentarioQueryFilterValidator : AbstractValidator<ComentarioQueryFilter>
    {
        public ComentarioQueryFilterValidator()
        {
            RuleFor(x => x.IdEvento)
                .GreaterThan(0)
                .When(x => x.IdEvento.HasValue)
                .WithMessage("El ID del evento debe ser mayor que cero.");

            RuleFor(x => x.IdUsuario)
                .GreaterThan(0)
                .When(x => x.IdUsuario.HasValue)
                .WithMessage("El ID del usuario debe ser mayor que cero.");

            RuleFor(x => x.Estado)
                .Must(BeAValidEstado)
                .When(x => !string.IsNullOrWhiteSpace(x.Estado))
                .WithMessage("El estado debe ser: visible, oculto o eliminado.");

            RuleFor(x => x.Fecha)
                .Must(BeAValidDate)
                .When(x => !string.IsNullOrWhiteSpace(x.Fecha))
                .WithMessage("La fecha debe tener un formato válido.");

            RuleFor(x => x.Texto)
                .MaximumLength(100)
                .WithMessage("El texto de búsqueda no puede exceder los 100 caracteres.");
        }

        private bool BeAValidEstado(string? estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
                return true;

            var estadosValidos = new[] { "visible", "oculto", "eliminado" };

            return estadosValidos.Contains(estado.Trim(), StringComparer.OrdinalIgnoreCase);
        }

        private bool BeAValidDate(string? fecha)
        {
            if (string.IsNullOrWhiteSpace(fecha))
                return true;

            return DateTime.TryParse(fecha, out _);
        }
    }
}