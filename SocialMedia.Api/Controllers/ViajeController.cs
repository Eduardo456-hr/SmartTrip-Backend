using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Api.Responses;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Exceptions;
using SocialMedia.Core.QueryFilters;
using SocialMedia.Services.Interfaces;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViajeController : ControllerBase
    {
        private readonly IViajeService _service;
        private readonly IMapper _mapper;
        private readonly IValidator<CrearViajeDto> _validator;

        public ViajeController(IViajeService service, IMapper mapper, IValidator<CrearViajeDto> validator)
        {
            _service = service;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CrearViajeDto dto)
        {
            var validation = await _validator.ValidateAsync(dto);

            if (!validation.IsValid)
            {
                return BadRequest(new
                {
                    message = "Error de validación",
                    errors = validation.Errors.Select(e => new { field = e.PropertyName, error = e.ErrorMessage })
                });
            }

            try
            {
                var entity = _mapper.Map<Viaje>(dto);
                await _service.InsertViaje(entity);

                return Ok(new ApiResponse<ViajeDto>(_mapper.Map<ViajeDto>(entity)));
            }
            catch (BusinessException ex)
            {
                return StatusCode((int)ex.StatusCode, new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ViajeQueryFilter filters)
        {
            var viajes = await _service.GetViajesDisponiblesAsync(filters);
            return Ok(new ApiResponse<IEnumerable<ViajeDto>>(_mapper.Map<IEnumerable<ViajeDto>>(viajes)));
        }

        [HttpGet("historial/{conductorId}")]
        public async Task<IActionResult> Historial(int conductorId)
        {
            var viajes = await _service.GetHistorialByConductorAsync(conductorId);
            return Ok(new ApiResponse<IEnumerable<ViajeDto>>(_mapper.Map<IEnumerable<ViajeDto>>(viajes)));
        }
    }
}