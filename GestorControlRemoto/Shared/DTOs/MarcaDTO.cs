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
    public class MarcaDTO
    {
        public int Id { get; set; }

        public string Marca1 { get; set; } = null!;

        public ICollection<CompatibleDTO> Compatible { get; set; } = new List<CompatibleDTO>();
    }
}

