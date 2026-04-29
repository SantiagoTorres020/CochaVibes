using CochaVibes.Core.DTOs;
using FluentValidation;

namespace CochaVibes.Services.Validators
{
    public class CrearComentarioDtoValidator : AbstractValidator<ComentarioDto>
    {
        public CrearComentarioDtoValidator()
        {
            RuleFor(x => x.Contenido)
                .NotEmpty().WithMessage("El contenido del comentario es obligatorio.")
                .MaximumLength(500).WithMessage("El contenido no puede exceder los 500 caracteres.");

            RuleFor(x => x.Fecha)
                .Must(BeAValidDate)
                .When(x => !string.IsNullOrWhiteSpace(x.Fecha))
                .WithMessage("La fecha debe tener un formato válido.");

            RuleFor(x => x.Estado)
                .NotEmpty().WithMessage("El estado es obligatorio.")
                .Must(BeAValidEstado).WithMessage("El estado debe ser: visible, oculto o eliminado.");

            RuleFor(x => x.IdUsuario)
                .GreaterThan(0).WithMessage("El ID de usuario debe ser mayor que cero.");

            RuleFor(x => x.IdEvento)
                .GreaterThan(0).WithMessage("El ID de evento debe ser mayor que cero.");
        }

        private bool BeAValidDate(string? fecha)
        {
            if (string.IsNullOrWhiteSpace(fecha))
                return true;

            return DateTime.TryParse(fecha, out _);
        }

        private bool BeAValidEstado(string estado)
        {
            var estadosValidos = new[] { "visible", "oculto", "eliminado" };
            return estadosValidos.Contains(estado.Trim(), StringComparer.OrdinalIgnoreCase);
        }
    }
}