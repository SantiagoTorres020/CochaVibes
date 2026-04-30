using CochaVibes.Api.Responses;
using CochaVibes.Core.DTOs;
using CochaVibes.Core.QueryFilters;
using CochaVibes.Services.Interfaces;
using CochaVibes.Services.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CochaVibes.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsistenciaController : ControllerBase
    {
        private readonly IAsistenciaService _asistenciaService;

        private readonly IValidator<EventoIdDto> _eventoIdValidator;

        private readonly IValidator<AsistenciaDto> _crearValidator;

        private readonly IValidator<AsistenciaDto> _actualizarValidator;

        private readonly IValidator<AsistenciaQueryFilter> _asistenciaFilterValidator;

        public AsistenciaController(
            IAsistenciaService asistenciaService,
            IValidator<EventoIdDto> eventoIdValidator,
            CrearAsistenciaDtoValidator crearValidator,
            ActualizarAsistenciaDtoValidator actualizarValidator,
            IValidator<AsistenciaQueryFilter> asistenciaFilterValidator)
        {
            _asistenciaService = asistenciaService;
            _eventoIdValidator = eventoIdValidator;
            _crearValidator = crearValidator;
            _actualizarValidator = actualizarValidator;
            _asistenciaFilterValidator = asistenciaFilterValidator;
        }

        [HttpGet("dto/mapper/dapper")]
        public async Task<IActionResult> GetAsistenciasFiltradasDapper([FromQuery] AsistenciaQueryFilter filtro)
        {
            var validationResult = await _asistenciaFilterValidator.ValidateAsync(filtro);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var asistencias = await _asistenciaService.GetAsistenciasFiltradasAsync(filtro);
            var response = new ApiResponse<IEnumerable<AsistenciaListaDto>>(asistencias);

            return Ok(response);
        }

        [HttpGet("dto/mapper/dapper/evento/{idEvento:int}")]
        public async Task<IActionResult> GetAsistenciasByEventoDapper(int idEvento)
        {
            var dto = new EventoIdDto { IdEvento = idEvento };
            var validationResult = await _eventoIdValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var asistencias = await _asistenciaService.GetAsistenciasByEventoAsync(idEvento);
            var response = new ApiResponse<IEnumerable<AsistenciaListaDto>>(asistencias);

            return Ok(response);
        }

        [HttpGet("dto/mapper/dapper/evento/{idEvento:int}/cantidad")]
        public async Task<IActionResult> GetCantidadAsistenciasByEventoDapper(int idEvento)
        {
            var dto = new EventoIdDto { IdEvento = idEvento };
            var validationResult = await _eventoIdValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var cantidad = await _asistenciaService.GetCantidadAsistenciasByEventoAsync(idEvento);
            var response = new ApiResponse<int>(cantidad);

            return Ok(response);
        }

        [HttpGet("dto/mapper/dapper/usuario/{idUsuario:int}/evento/{idEvento:int}")]
        public async Task<IActionResult> GetAsistenciaDapper(int idUsuario, int idEvento)
        {
            var asistencia = await _asistenciaService.GetAsistenciaAsync(idUsuario, idEvento);

            if (asistencia == null)
            {
                return NotFound(new
                {
                    message = "Asistencia no encontrada."
                });
            }

            var response = new ApiResponse<AsistenciaDto>(asistencia);

            return Ok(response);
        }

        [HttpPost("dto/mapper/dapper")]
        public async Task<IActionResult> CrearAsistenciaDapper([FromBody] AsistenciaDto asistenciaDto)
        {
            var validationResult = await _crearValidator.ValidateAsync(asistenciaDto);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            await _asistenciaService.InsertAsistencia(asistenciaDto);

            var response = new ApiResponse<AsistenciaDto>(asistenciaDto);

            return Ok(response);
        }

        [HttpPut("dto/mapper/dapper/usuario/{idUsuario:int}/evento/{idEvento:int}")]
        public async Task<IActionResult> ActualizarAsistenciaDapper(
            int idUsuario,
            int idEvento,
            [FromBody] AsistenciaDto asistenciaDto)
        {
            if (idUsuario != asistenciaDto.IdUsuario || idEvento != asistenciaDto.IdEvento)
            {
                return BadRequest(new
                {
                    message = "El ID del usuario o del evento no coincide."
                });
            }

            var validationResult = await _actualizarValidator.ValidateAsync(asistenciaDto);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            await _asistenciaService.UpdateAsistencia(asistenciaDto);

            var response = new ApiResponse<AsistenciaDto>(asistenciaDto);

            return Ok(response);
        }

        [HttpDelete("dto/mapper/dapper/usuario/{idUsuario:int}/evento/{idEvento:int}")]
        public async Task<IActionResult> EliminarAsistenciaDapper(int idUsuario, int idEvento)
        {
            await _asistenciaService.DeleteAsistencia(idUsuario, idEvento);

            return NoContent();
        }
    }
}