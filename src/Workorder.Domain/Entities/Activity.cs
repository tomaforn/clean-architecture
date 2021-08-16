using Shared.Domain;

namespace Modules.Workorder.Domain.Entities
{
    public class Activity : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
