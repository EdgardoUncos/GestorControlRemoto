using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace GestorControlRemoto.Shared.Models;

public partial class Compatible
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Modelo { get; set; }

    public int IdControl { get; set; }

    public int IdMarca { get; set; }

    [JsonIgnore]
    [ForeignKey("IdControl")]
    [InverseProperty("Compatible")]
    public virtual ControlRemoto? IdControlNavigation { get; set; } = null!;

    [JsonIgnore]
    [ForeignKey("IdMarca")]
    [InverseProperty("Compatible")]
    public virtual Marca? IdMarcaNavigation { get; set; } = null!;
}
