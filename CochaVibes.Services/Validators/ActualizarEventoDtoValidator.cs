using CochaVibes.Core.DTOs;
using FluentValidation;

namespace CochaVibes.Services.Validators
{
    public class ActualizarEventoDtoValidator : AbstractValidator<EventoDto>
    {
        public ActualizarEventoDtoValidator()
        {
            RuleFor(x => x.IdEvento)
                .GreaterThan(0).WithMessage("El ID del evento debe ser mayor que cero.");

            RuleFor(x => x.Titulo)
                .NotEmpty().WithMessage("El título es obligatorio.")
                .MaximumLength(150).WithMessage("El título no puede exceder los 150 caracteres.");

            RuleFor(x => x.Descripcion)
                .NotEmpty().WithMessage("La descripción es obligatoria.")
                .MaximumLength(1000).WithMessage("La descripción no puede exceder los 1000 caracteres.");

            RuleFor(x => x.Fecha)
                .NotEmpty().WithMessage("La fecha es obligatoria.")
                .Must(BeAValidDate).WithMessage("La fecha debe tener un formato válido (yyyy-MM-dd o similar).");

            RuleFor(x => x.HoraInicio)
                .NotEmpty().WithMessage("La hora de inicio es obligatoria.")
                .Must(BeAValidTime).WithMessage("La hora de inicio debe tener un formato válido (HH:mm:ss).");

            RuleFor(x => x.HoraFin)
                .NotEmpty().WithMessage("La hora de fin es obligatoria.")
                .Must(BeAValidTime).WithMessage("La hora de fin debe tener un formato válido (HH:mm:ss).");

            RuleFor(x => x)
                .Must(HaveValidTimeRange)
                .WithMessage("La hora de fin debe ser mayor a la hora de inicio.");

            RuleFor(x => x.Precio)
                .GreaterThanOrEqualTo(0).WithMessage("El precio no puede ser negativo.");

            RuleFor(x => x.Estado)
                .NotEmpty().WithMessage("El estado es obligatorio.")
                .Must(BeAValidEstado).WithMessage("El estado debe ser: activo, cancelado o finalizado.");

            RuleFor(x => x.IdCategoria)
                .GreaterThan(0).WithMessage("El ID de categoría debe ser mayor que cero.");

            RuleFor(x => x.IdUsuario)
                .GreaterThan(0).WithMessage("El ID de usuario debe ser mayor que cero.");

            RuleFor(x => x.IdUbicacion)
                .GreaterThan(0).WithMessage("El ID de ubicación debe ser mayor que cero.");
        }

        private bool BeAValidDate(string fecha)
        {
            return DateTime.TryParse(fecha, out _);
        }

        private bool BeAValidTime(string hora)
        {
            return TimeSpan.TryParse(hora, out _);
        }

        private bool HaveValidTimeRange(EventoDto dto)
        {
            if (!TimeSpan.TryParse(dto.HoraInicio, out var inicio))
                return false;

            if (!TimeSpan.TryParse(dto.HoraFin, out var fin))
                return false;

            return fin > inicio;
        }

        private bool BeAValidEstado(string estado)
        {
            var estadosValidos = new[] { "activo", "cancelado", "finalizado" };
            return estadosValidos.Contains(estado.Trim(), StringComparer.OrdinalIgnoreCase);
        }
    }
}