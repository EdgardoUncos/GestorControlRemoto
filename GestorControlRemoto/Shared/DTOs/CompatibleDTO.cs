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
    public class CompatibleDTO
    {
        public int Id { get; set; }

        public string? Modelo { get; set; }

        public int IdControl { get; set; }

        public int IdMarca { get; set; }

        public ControlRemotoDTO IdControlNavigation { get; set; } = null!;

        public MarcaDTO IdMarcaNavigation { get; set; } = null!;
    }
}
