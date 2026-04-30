using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Entities;

public partial class Viaje
{
    public int Id { get; set; }

    public int ConductorId { get; set; }

    public string Origen { get; set; } = null!;

    public string Destino { get; set; } = null!;

    public DateTime FechaViaje { get; set; }

    public int AsientosDisponibles { get; set; }

    public decimal Precio { get; set; }

    public string Estado { get; set; } = "Disponible";

    public DateTime? FechaRegistro { get; set; }

    public virtual Conductore? Conductor { get; set; }
}