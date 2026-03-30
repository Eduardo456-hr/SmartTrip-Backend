using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
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
    }
}
