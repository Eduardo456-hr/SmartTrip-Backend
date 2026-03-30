using System;
using System.Collections.Generic;

namespace SocialMedia.Core.Entities;

public partial class Pasajero
{
    public int Id { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public DateTime? FechaRegistro { get; set; }
}
