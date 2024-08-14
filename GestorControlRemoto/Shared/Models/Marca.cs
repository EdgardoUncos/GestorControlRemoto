using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GestorControlRemoto.Shared.Models;

public partial class Marca
{
    [Key]
    public int Id { get; set; }

    [Column("Marca")]
    [StringLength(50)]
    [Unicode(false)]
    public string Marca1 { get; set; } = null!;

    [InverseProperty("IdMarcaNavigation")]
    public virtual ICollection<Compatible> Compatible { get; set; } = new List<Compatible>();
}
