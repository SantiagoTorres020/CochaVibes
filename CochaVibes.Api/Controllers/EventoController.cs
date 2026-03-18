using AutoMapper;
using CochaVibes.Core.DTOs;
using CochaVibes.Core.Entities;
using CochaVibes.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CochaVibes.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IEventoRepository _eventoRepository;
        private readonly IMapper _mapper;

        public EventoController(IEventoRepository eventoRepository, IMapper mapper)
        {
            _eventoRepository = eventoRepository;
            _mapper = mapper;
        }

        #region Sin DTOs
        [HttpGet]
        public async Task<IActionResult> GetEventos()
        {
            var eventos = await _eventoRepository.GetAllEventosAsync();
            return Ok(eventos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventoById(int id)
        {
            var evento = await _eventoRepository.GetEventoByIdAsync(id);

            if (evento == null)
                return NotFound("Evento no encontrado");

            return Ok(evento);
        }

        [HttpPost]
        public async Task<IActionResult> InsertEvento(Evento evento)
        {
            await _eventoRepository.InsertEvento(evento);
            return Created($"api/evento/{evento.IdEvento}", evento);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEvento(Evento evento)
        {
            await _eventoRepository.UpdateEvento(evento);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEvento(Evento evento)
        {
            await _eventoRepository.DeleteEvento(evento);
            return NoContent();
        }
        #endregion

        #region Con DTOs
        [HttpGet("dto")]
        public async Task<IActionResult> GetDtoEventos()
        {
            var eventos = await _eventoRepository.GetAllEventosAsync();

            var eventosDto = eventos.Select(e => new EventoDto
            {
                IdEvento = e.IdEvento,
                Titulo = e.Titulo,
                Descripcion = e.Descripcion,
                Fecha = e.Fecha.ToString("yyyy-MM-dd"),
                HoraInicio = e.HoraInicio.ToString(),
                HoraFin = e.HoraFin.ToString(),
                Precio = e.Precio,
                Estado = e.Estado,
                IdCategoria = e.IdCategoria,
                IdUsuario = e.IdUsuario,
                IdUbicacion = e.IdUbicacion
            });

            return Ok(eventosDto);
        }

        [HttpGet("dto/{id}")]
        public async Task<IActionResult> GetDtoEventoById(int id)
        {
            var evento = await _eventoRepository.GetEventoByIdAsync(id);

            if (evento == null)
                return NotFound("Evento no encontrado");

            var eventoDto = new EventoDto
            {
                IdEvento = evento.IdEvento,
                Titulo = evento.Titulo,
                Descripcion = evento.Descripcion,
                Fecha = evento.Fecha.ToString("yyyy-MM-dd"),
                HoraInicio = evento.HoraInicio.ToString(),
                HoraFin = evento.HoraFin.ToString(),
                Precio = evento.Precio,
                Estado = evento.Estado,
                IdCategoria = evento.IdCategoria,
                IdUsuario = evento.IdUsuario,
                IdUbicacion = evento.IdUbicacion
            };

            return Ok(eventoDto);
        }

        [HttpPost("dto")]
        public async Task<IActionResult> InsertDtoEvento(EventoDto eventoDto)
        {
            var evento = new Evento
            {
                IdEvento = eventoDto.IdEvento,
                Titulo = eventoDto.Titulo,
                Descripcion = eventoDto.Descripcion,
                Fecha = Convert.ToDateTime(eventoDto.Fecha),
                HoraInicio = TimeSpan.Parse(eventoDto.HoraInicio),
                HoraFin = TimeSpan.Parse(eventoDto.HoraFin),
                Precio = eventoDto.Precio,
                Estado = eventoDto.Estado,
                IdCategoria = eventoDto.IdCategoria,
                IdUsuario = eventoDto.IdUsuario,
                IdUbicacion = eventoDto.IdUbicacion
            };

            await _eventoRepository.InsertEvento(evento);
            return Created($"api/evento/{evento.IdEvento}", evento);
        }

        [HttpPut("dto/{id}")]
        public async Task<IActionResult> UpdateDtoEvento(int id, [FromBody] EventoDto eventoDto)
        {
            if (id != eventoDto.IdEvento)
                return BadRequest("El ID del evento no coincide");

            var evento = await _eventoRepository.GetEventoByIdAsync(id);
            if (evento == null)
                return NotFound("Evento no encontrado");

            evento.Titulo = eventoDto.Titulo;
            evento.Descripcion = eventoDto.Descripcion;
            evento.Fecha = Convert.ToDateTime(eventoDto.Fecha);
            evento.HoraInicio = TimeSpan.Parse(eventoDto.HoraInicio);
            evento.HoraFin = TimeSpan.Parse(eventoDto.HoraFin);
            evento.Precio = eventoDto.Precio;
            evento.Estado = eventoDto.Estado;
            evento.IdCategoria = eventoDto.IdCategoria;
            evento.IdUsuario = eventoDto.IdUsuario;
            evento.IdUbicacion = eventoDto.IdUbicacion;

            await _eventoRepository.UpdateEvento(evento);
            return NoContent();
        }

        [HttpDelete("dto/{id}")]
        public async Task<IActionResult> DeleteDtoEvento(int id)
        {
            var evento = await _eventoRepository.GetEventoByIdAsync(id);
            if (evento == null)
                return NotFound("Evento no encontrado");

            await _eventoRepository.DeleteEvento(evento);
            return NoContent();
        }
        #endregion

        #region Con DTO Mapper
        [HttpGet("dto/mapper")]
        public async Task<IActionResult> GetDtoMapperEventos()
        {
            var eventos = await _eventoRepository.GetAllEventosAsync();
            var eventosDto = _mapper.Map<IEnumerable<EventoDto>>(eventos);
            return Ok(eventosDto);
        }

        [HttpGet("dto/mapper/{id}")]
        public async Task<IActionResult> GetDtoMapperEventoById(int id)
        {
            var evento = await _eventoRepository.GetEventoByIdAsync(id);

            if (evento == null)
                return NotFound("Evento no encontrado");

            var eventoDto = _mapper.Map<EventoDto>(evento);
            return Ok(eventoDto);
        }

        [HttpPost("dto/mapper")]
        public async Task<IActionResult> InsertDtoMapperEvento(EventoDto eventoDto)
        {
            var evento = _mapper.Map<Evento>(eventoDto);
            await _eventoRepository.InsertEvento(evento);
            return Created($"api/evento/{evento.IdEvento}", evento);
        }

        [HttpPut("dto/mapper/{id}")]
        public async Task<IActionResult> UpdateDtoMapperEvento(int id, [FromBody] EventoDto eventoDto)
        {
            if (id != eventoDto.IdEvento)
                return BadRequest("El ID del evento no coincide");

            var evento = await _eventoRepository.GetEventoByIdAsync(id);
            if (evento == null)
                return NotFound("Evento no encontrado");

            evento.Titulo = eventoDto.Titulo;
            evento.Descripcion = eventoDto.Descripcion;
            evento.Fecha = Convert.ToDateTime(eventoDto.Fecha);
            evento.HoraInicio = TimeSpan.Parse(eventoDto.HoraInicio);
            evento.HoraFin = TimeSpan.Parse(eventoDto.HoraFin);
            evento.Precio = eventoDto.Precio;
            evento.Estado = eventoDto.Estado;
            evento.IdCategoria = eventoDto.IdCategoria;
            evento.IdUsuario = eventoDto.IdUsuario;
            evento.IdUbicacion = eventoDto.IdUbicacion;

            await _eventoRepository.UpdateEvento(evento);
            return NoContent();
        }

        [HttpDelete("dto/mapper/{id}")]
        public async Task<IActionResult> DeleteDtoMapperEvento(int id)
        {
            var evento = await _eventoRepository.GetEventoByIdAsync(id);
            if (evento == null)
                return NotFound("Evento no encontrado");

            await _eventoRepository.DeleteEvento(evento);
            return NoContent();
        }
        #endregion
    }
}