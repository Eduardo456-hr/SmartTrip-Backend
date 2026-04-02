using System;

namespace SocialMedia.Core.Entities
{
    public partial class Pasajero
    {
        public int UsuarioId { get; set; }

        public string Nombres { get; set; } = null!;

        public string Apellidos { get; set; } = null!;

        public string Telefono { get; set; } = null!;

        public virtual Usuario Usuario { get; set; } = null!;
    }
}