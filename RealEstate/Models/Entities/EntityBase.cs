using System.ComponentModel.DataAnnotations;

namespace RealEstate.Models.Entities;

public class EntityBase : EntityId
{
    public Guid CriadoPor { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public Guid AtualizadoPor { get; set; }
    public DateTime? AtualizadoEm { get; set; }
    public bool Ativo { get; set; } = true;
}
