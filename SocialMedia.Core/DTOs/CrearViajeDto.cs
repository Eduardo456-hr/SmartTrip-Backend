using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.DTOs
{
    public class CrearViajeDto
    {
        public int ConductorId { get; set; }
        public string Origen { get; set; } = null!;
        public string Destino { get; set; } = null!;
        public DateTime FechaViaje { get; set; }
        public int AsientosDisponibles { get; set; }
        public decimal Precio { get; set; }
    }
}