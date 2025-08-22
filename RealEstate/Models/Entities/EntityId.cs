using System.ComponentModel.DataAnnotations;

namespace RealEstate.Models.Entities;

public class EntityId
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
}
