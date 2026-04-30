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
    public class ConductorController : ControllerBase
    {
        private readonly IConductorService _conductorService;
        private readonly IMapper _mapper;
        private readonly IValidator<ConductorDto> _validator;

        public ConductorController(IConductorService conductorService, IMapper mapper, IValidator<ConductorDto> validator)
        {
            _conductorService = conductorService;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllConductores()
        {
            var conductores = await _conductorService.GetAllConductoresAsync();
            return Ok(conductores);
        }

        [HttpPost]
        public async Task<IActionResult> InsertConductor(ConductorDto conductorDto)
        {
            // 1. Validar reglas de negocio con FluentValidation
            var validationResult = await _validator.ValidateAsync(conductorDto);
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

            try
            {
                // 2. Mapear DTO a la Entidad Conductore
                var conductor = _mapper.Map<Conductore>(conductorDto);

                // 3. Insertar en la Base de Datos
                await _conductorService.InsertConductor(conductor);

                // 4. Mapear la Entidad creada de vuelta a DTO para mostrarla
                var resultDto = _mapper.Map<ConductorDto>(conductor);

                // 5. Estandarizar la respuesta JSON
                var response = new ApiResponse<ConductorDto>(resultDto);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al registrar el conductor", error = ex.Message });
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConductor(int id, ConductorDto conductorDto)
        {
            try
            {
                var conductor = _mapper.Map<Conductore>(conductorDto);

                await _conductorService.UpdateConductor(id, conductor);

                return Ok(new { message = "Conductor actualizado correctamente" });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConductor(int id)
        {
            try
            {
                await _conductorService.DeleteConductor(id);
                return Ok(new { message = "Conductor eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
