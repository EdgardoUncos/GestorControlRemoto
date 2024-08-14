using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace GestorControlRemoto.Shared.Models;

public partial class ControlRemoto
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string CodigoControl { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? Detalle { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? UrlImagen { get; set; }

   
    [InverseProperty("IdControlNavigation")]
    public virtual ICollection<Compatible>? Compatible { get; set; } = new List<Compatible>();
}
