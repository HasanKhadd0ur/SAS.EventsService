using SAS.SharedKernel.Entities;

namespace SAS.EventsService.Domain.Events.Entities
{
    public class Review : BaseEntity<Guid>
    {
        public Guid EventId { get; set; }
        public Event Event { get; set; }

        public Guid UserId { get; set; } 
        public string UserName { get; set; } 

        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }

        public Review()
        {
            CreatedAt = DateTime.UtcNow;
        }
        public void UpdateComment(string comment, DateTime updatedAt)
        {
            Comment = comment;
            LastUpdatedAt = updatedAt;
        }

    }
}
