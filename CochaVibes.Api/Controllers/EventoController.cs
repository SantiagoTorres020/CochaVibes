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
    public class EventoController : ControllerBase
    {
        private readonly IEventoService _eventoService;
        private readonly IMapper _mapper;
        private readonly IValidator<EventoBusquedaDto> _busquedaValidator;
        private readonly IValidator<EventoIdDto> _idValidator;
        private readonly IValidator<EventoDto> _crearValidator;
        private readonly IValidator<EventoDto> _actualizarValidator;

        public EventoController(
            IEventoService eventoService,
            IMapper mapper,
            IValidator<EventoBusquedaDto> busquedaValidator,
            IValidator<EventoIdDto> idValidator,
            CrearEventoDtoValidator crearValidator,
            ActualizarEventoDtoValidator actualizarValidator)
        {
            _eventoService = eventoService;
            _mapper = mapper;
            _busquedaValidator = busquedaValidator;
            _idValidator = idValidator;
            _crearValidator = crearValidator;
            _actualizarValidator = actualizarValidator;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarEventos([FromQuery] EventoBusquedaDto filtro)
        {
            var validationResult = await _busquedaValidator.ValidateAsync(filtro);

            if (!validationResult.IsValid)
            {
                return BadRequest(new
                {
                    message = "Error de validación",
                    errors = validationResult.Errors.Select(e => new
                    {
                        field = e.PropertyName,
                        error = e.ErrorMessage
                    })
                });
            }

            var eventos = await _eventoService.BuscarEventosAsync(filtro);
            var eventosDto = _mapper.Map<IEnumerable<EventoListaDto>>(eventos);

            var response = new ApiResponse<IEnumerable<EventoListaDto>>(eventosDto);
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEventoDetalle(int id)
        {
            var dto = new EventoIdDto { IdEvento = id };
            var validationResult = await _idValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                return BadRequest(new
                {
                    message = "Error de validación",
                    errors = validationResult.Errors.Select(e => new
                    {
                        field = e.PropertyName,
                        error = e.ErrorMessage
                    })
                });
            }

            var evento = await _eventoService.GetEventoDetalleByIdAsync(id);

            if (evento == null)
            {
                return NotFound(new
                {
                    message = "Evento no encontrado o no disponible para consulta."
                });
            }

            var eventoDto = _mapper.Map<EventoDetalleDto>(evento);
            var response = new ApiResponse<EventoDetalleDto>(eventoDto);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CrearEvento([FromBody] EventoDto eventoDto)
        {
            var validationResult = await _crearValidator.ValidateAsync(eventoDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(new
                {
                    message = "Error de validación",
                    errors = validationResult.Errors.Select(e => new
                    {
                        field = e.PropertyName,
                        error = e.ErrorMessage
                    })
                });
            }

            var evento = _mapper.Map<Evento>(eventoDto);

            await _eventoService.InsertEvento(evento);

            var resultDto = _mapper.Map<EventoDto>(evento);
            var response = new ApiResponse<EventoDto>(resultDto);

            return Ok(response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> ActualizarEvento(int id, [FromBody] EventoDto eventoDto)
        {
            if (id != eventoDto.IdEvento)
            {
                return BadRequest(new
                {
                    message = "El ID del evento no coincide."
                });
            }

            var idDto = new EventoIdDto { IdEvento = id };
            var idValidation = await _idValidator.ValidateAsync(idDto);

            if (!idValidation.IsValid)
            {
                return BadRequest(new
                {
                    message = "Error de validación",
                    errors = idValidation.Errors.Select(e => new
                    {
                        field = e.PropertyName,
                        error = e.ErrorMessage
                    })
                });
            }

            var validationResult = await _actualizarValidator.ValidateAsync(eventoDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(new
                {
                    message = "Error de validación",
                    errors = validationResult.Errors.Select(e => new
                    {
                        field = e.PropertyName,
                        error = e.ErrorMessage
                    })
                });
            }

            var eventoExistente = await _eventoService.GetEventoDetalleByIdAsync(id);

            if (eventoExistente == null)
            {
                return NotFound(new
                {
                    message = "Evento no encontrado."
                });
            }

            _mapper.Map(eventoDto, eventoExistente);
            await _eventoService.UpdateEvento(eventoExistente);

            var resultDto = _mapper.Map<EventoDto>(eventoExistente);
            var response = new ApiResponse<EventoDto>(resultDto);

            return Ok(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> EliminarEvento(int id)
        {
            var dto = new EventoIdDto { IdEvento = id };
            var validationResult = await _idValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                return BadRequest(new
                {
                    message = "Error de validación",
                    errors = validationResult.Errors.Select(e => new
                    {
                        field = e.PropertyName,
                        error = e.ErrorMessage
                    })
                });
            }

            var evento = await _eventoService.GetEventoDetalleByIdAsync(id);

            if (evento == null)
            {
                return NotFound(new
                {
                    message = "Evento no encontrado."
                });
            }

            await _eventoService.DeleteEvento(id);

            var response = new ApiResponse<bool>(true);
            return Ok(response);
        }
    }
}