using System;
using System.Collections.Generic;

namespace SocialMedia.Core.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Correo { get; set; } = null!;
        public string Contrasena { get; set; } = null!;
        public string Rol { get; set; } = null!;
        public DateTime FechaRegistro { get; set; }


        public virtual Pasajero? Pasajero { get; set; }
        public virtual Conductore? Conductor { get; set; }
    }
}