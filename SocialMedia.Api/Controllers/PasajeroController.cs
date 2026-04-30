using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using FluentValidation;
using SocialMedia.Api.Responses;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Services.Interfaces;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasajeroController : ControllerBase
    {
        private readonly IPasajeroService _pasajeroService;
        private readonly IMapper _mapper;
        private readonly IValidator<PasajeroDto> _validator;

        public PasajeroController(IPasajeroService pasajeroService, IMapper mapper, IValidator<PasajeroDto> validator)
        {
            _pasajeroService = pasajeroService;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpPost]
        public async Task<IActionResult> InsertPasajero(PasajeroDto pasajeroDto)
        {
            var validationResult = await _validator.ValidateAsync(pasajeroDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(new
                {
                    message = "Error de validación",
                    errors = validationResult.Errors.Select(e => new { field = e.PropertyName, error = e.ErrorMessage })
                });
            }

            try
            {
                var pasajero = _mapper.Map<Pasajero>(pasajeroDto);
                await _pasajeroService.InsertPasajero(pasajero);
                var resultDto = _mapper.Map<PasajeroDto>(pasajero);

                return Ok(new ApiResponse<PasajeroDto>(resultDto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al registrar el pasajero", error = ex.Message });
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePasajero(int id)
        {
            try
            {
                var pasajero = await _pasajeroService.GetPasajeroByIdAsync(id);
                if (pasajero == null)
                {
                    return NotFound(new { message = "Pasajero no encontrado" });
                }
                await _pasajeroService.DeletePasajero(pasajero);
                return Ok(new { message = "Pasajero eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al eliminar el pasajero", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePasajero(int id, [FromBody] PasajeroDto pasajeroDto)
        {
            pasajeroDto.Id = id;

            var validationResult = await _validator.ValidateAsync(pasajeroDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(new
                {
                    message = "Error de validación",
                    errors = validationResult.Errors.Select(e => new { field = e.PropertyName, error = e.ErrorMessage })
                });
            }

            try
            {
                var existingPasajero = await _pasajeroService.GetPasajeroByIdAsync(id);

                if (existingPasajero == null)
                {
                    return NotFound(new { message = "Pasajero no encontrado" });
                }

                // CORRECCIÓN: Mapeamos los cambios del DTO DIRECTAMENTE sobre el objeto existente
                _mapper.Map(pasajeroDto, existingPasajero);

                // Ahora actualizamos el objeto que ya está siendo rastreado
                await _pasajeroService.UpdatePasajero(existingPasajero);

                var resultDto = _mapper.Map<PasajeroDto>(existingPasajero);

                return Ok(new
                {
                    message = "Pasajero actualizado correctamente",
                    data = resultDto
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al actualizar el pasajero", error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPasajeros()
        {
            try
            {
                var pasajeros = await _pasajeroService.GetAllPasajerosAsync();
                var pasajerosDto = _mapper.Map<IEnumerable<PasajeroDto>>(pasajeros);
                return Ok(new ApiResponse<IEnumerable<PasajeroDto>>(pasajerosDto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener los pasajeros", error = ex.Message });
            }

        }
    }
}
