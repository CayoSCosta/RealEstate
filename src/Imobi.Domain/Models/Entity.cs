namespace Imobi.Domain.Models
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
            Status = true;
            CriadoEm = DateTime.UtcNow;
        }

        public Guid Id { get; set; }
        public bool Status { get; set; }
        public DateTime CriadoEm { get; set; }
    }
}
