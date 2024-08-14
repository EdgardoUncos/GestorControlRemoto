using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorControlRemoto.Shared.DTOs
{
    public class CompatibleVM
    {
        public int Id { get; set; }

        public string? Modelo { get; set; }

        public int IdControl { get; set; }

        public int IdMarca { get; set; }

        public string CodigoControl { get; set; } = null!;
        public string? UrlImagen { get; set; }

        public string Marca { get; set; } = null!;
        public string? Detalle { get; set; }

        public List<MarcaDTO> ListaMarcas { get; set; } = new List<MarcaDTO>();
        public List<ControlRemotoDTO> ListaControles { get; set; } = new List<ControlRemotoDTO>();

    }
}
