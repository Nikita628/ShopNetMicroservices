namespace Order.Infrastructure.Database.Models
{
    public abstract class ModelBase
    {
        public int Id { get; protected set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}