using CochaVibes.Core.DTOs;
using FluentValidation;

namespace CochaVibes.Services.Validators
{
    public class ComentarioIdDtoValidator : AbstractValidator<ComentarioIdDto>
    {
        public ComentarioIdDtoValidator()
        {
            RuleFor(x => x.IdComentario)
                .GreaterThan(0)
                .WithMessage("El ID del comentario debe ser mayor que cero.");
        }
    }
}