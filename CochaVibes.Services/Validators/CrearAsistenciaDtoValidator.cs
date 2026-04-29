using CochaVibes.Core.DTOs;
using FluentValidation;

namespace CochaVibes.Services.Validators
{
    public class CrearAsistenciaDtoValidator : AbstractValidator<AsistenciaDto>
    {
        public CrearAsistenciaDtoValidator()
        {
            RuleFor(x => x.IdUsuario)
                .GreaterThan(0).WithMessage("El ID de usuario debe ser mayor que cero.");

            RuleFor(x => x.IdEvento)
                .GreaterThan(0).WithMessage("El ID de evento debe ser mayor que cero.");

            RuleFor(x => x.Estado)
                .NotEmpty().WithMessage("El estado de asistencia es obligatorio.")
                .Must(BeAValidEstado).WithMessage("El estado debe ser: confirmada, pendiente o cancelada.");
        }

        private bool BeAValidEstado(string estado)
        {
            var estadosValidos = new[] { "confirmada", "pendiente", "cancelada" };
            return estadosValidos.Contains(estado.Trim(), StringComparer.OrdinalIgnoreCase);
        }
    }
}