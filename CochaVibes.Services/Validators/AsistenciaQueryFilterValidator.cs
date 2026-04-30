using CochaVibes.Core.QueryFilters;
using FluentValidation;

namespace CochaVibes.Services.Validators
{
    public class AsistenciaQueryFilterValidator : AbstractValidator<AsistenciaQueryFilter>
    {
        public AsistenciaQueryFilterValidator()
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
                .WithMessage("El estado debe ser: confirmada, pendiente o cancelada.");
        }

        private bool BeAValidEstado(string? estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
                return true;

            var estadosValidos = new[] { "confirmada", "pendiente", "cancelada" };

            return estadosValidos.Contains(estado.Trim(), StringComparer.OrdinalIgnoreCase);
        }
    }
}