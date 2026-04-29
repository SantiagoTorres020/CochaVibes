using AutoMapper;
using CochaVibes.Api.Responses;
using CochaVibes.Core.DTOs;
using CochaVibes.Core.Entities;
using CochaVibes.Services.Interfaces;
using CochaVibes.Services.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CochaVibes.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentarioController : ControllerBase
    {
        private readonly IComentarioService _comentarioService;

        private readonly IMapper _mapper;

        private readonly IValidator<EventoIdDto> _eventoIdValidator;

        private readonly IValidator<ComentarioIdDto> _comentarioIdValidator;

        private readonly IValidator<ComentarioDto> _crearValidator;

        private readonly IValidator<ComentarioDto> _actualizarValidator;

        public ComentarioController(
            IComentarioService comentarioService,
            IMapper mapper,
            IValidator<EventoIdDto> eventoIdValidator,
            IValidator<ComentarioIdDto> comentarioIdValidator,
            CrearComentarioDtoValidator crearValidator,
            ActualizarComentarioDtoValidator actualizarValidator)
        {
            _comentarioService = comentarioService;
            _mapper = mapper;
            _eventoIdValidator = eventoIdValidator;
            _comentarioIdValidator = comentarioIdValidator;
            _crearValidator = crearValidator;
            _actualizarValidator = actualizarValidator;
        }

        [HttpGet("dto/mapper/dapper/evento/{idEvento:int}")]
        public async Task<IActionResult> GetComentariosByEventoDapper(int idEvento)
        {
            var dto = new EventoIdDto { IdEvento = idEvento };
            var validationResult = await _eventoIdValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var comentarios = await _comentarioService.GetComentariosByEventoAsync(idEvento);
            var response = new ApiResponse<IEnumerable<ComentarioListaDto>>(comentarios);

            return Ok(response);
        }

        [HttpGet("dto/mapper/dapper/evento/{idEvento:int}/cantidad")]
        public async Task<IActionResult> GetCantidadComentariosByEventoDapper(int idEvento)
        {
            var dto = new EventoIdDto { IdEvento = idEvento };
            var validationResult = await _eventoIdValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var cantidad = await _comentarioService.GetCantidadComentariosByEventoAsync(idEvento);
            var response = new ApiResponse<int>(cantidad);

            return Ok(response);
        }

        [HttpGet("dto/mapper/dapper/{id:int}")]
        public async Task<IActionResult> GetComentarioByIdDapper(int id)
        {
            var dto = new ComentarioIdDto { IdComentario = id };
            var validationResult = await _comentarioIdValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var comentario = await _comentarioService.GetComentarioDetalleByIdAsync(id);

            if (comentario == null)
            {
                return NotFound(new
                {
                    message = "Comentario no encontrado."
                });
            }

            var response = new ApiResponse<ComentarioListaDto>(comentario);

            return Ok(response);
        }

        [HttpPost("dto/mapper/dapper")]
        public async Task<IActionResult> CrearComentarioDapper([FromBody] ComentarioDto comentarioDto)
        {
            var validationResult = await _crearValidator.ValidateAsync(comentarioDto);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var comentario = _mapper.Map<Comentario>(comentarioDto);

            await _comentarioService.InsertComentario(comentario);

            var resultDto = _mapper.Map<ComentarioDto>(comentario);
            var response = new ApiResponse<ComentarioDto>(resultDto);

            return Ok(response);
        }

        [HttpPut("dto/mapper/dapper/{id:int}")]
        public async Task<IActionResult> ActualizarComentarioDapper(int id, [FromBody] ComentarioDto comentarioDto)
        {
            if (id != comentarioDto.IdComentario)
            {
                return BadRequest(new
                {
                    message = "El ID del comentario no coincide."
                });
            }

            var idDto = new ComentarioIdDto { IdComentario = id };
            var idValidation = await _comentarioIdValidator.ValidateAsync(idDto);

            if (!idValidation.IsValid)
                throw new ValidationException(idValidation.Errors);

            var validationResult = await _actualizarValidator.ValidateAsync(comentarioDto);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var comentarioExistente = await _comentarioService.GetComentarioByIdAsync(id);

            if (comentarioExistente == null)
            {
                return NotFound(new
                {
                    message = "Comentario no encontrado."
                });
            }

            _mapper.Map(comentarioDto, comentarioExistente);

            _comentarioService.UpdateComentario(comentarioExistente);

            var resultDto = _mapper.Map<ComentarioDto>(comentarioExistente);
            var response = new ApiResponse<ComentarioDto>(resultDto);

            return Ok(response);
        }

        [HttpDelete("dto/mapper/dapper/{id:int}")]
        public async Task<IActionResult> EliminarComentarioDapper(int id)
        {
            var dto = new ComentarioIdDto { IdComentario = id };
            var validationResult = await _comentarioIdValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var comentario = await _comentarioService.GetComentarioByIdAsync(id);

            if (comentario == null)
            {
                return NotFound(new
                {
                    message = "Comentario no encontrado."
                });
            }

            await _comentarioService.DeleteComentario(id);

            return NoContent();
        }
    }
}