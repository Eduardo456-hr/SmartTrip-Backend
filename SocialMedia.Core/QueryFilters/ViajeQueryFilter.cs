using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.QueryFilters
{
    public class ViajeQueryFilter
    {
        public string? Origen { get; set; }
        public string? Destino { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Estado { get; set; }
    }
}