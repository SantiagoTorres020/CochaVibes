using AutoMapper;
using CochaVibes.Api.Responses;
using CochaVibes.Core.DTOs;
using CochaVibes.Services.Interfaces;
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

        public EventoController(
            IEventoService eventoService,
            IMapper mapper,
            IValidator<EventoBusquedaDto> busquedaValidator,
            IValidator<EventoIdDto> idValidator)
        {
            _eventoService = eventoService;
            _mapper = mapper;
            _busquedaValidator = busquedaValidator;
            _idValidator = idValidator;
        }

        // CU1: Visualizar y buscar eventos
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

        // CU2: Ver detalle de un evento
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
    }
}