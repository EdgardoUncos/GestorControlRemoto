using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorControlRemoto.Shared.DTOs
{
    public class ControlRemotoDTO
    {
        public int Id { get; set; }

        public string? CodigoControl { get; set; } = null!;

        public string? Detalle { get; set; }

        public string? UrlImagen { get; set; }

    }
}
