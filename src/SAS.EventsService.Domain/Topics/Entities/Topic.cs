using SAS.EventsService.SharedKernel.Entities;

namespace SAS.EventService.Domain.Entities
{
    public class Topic : BaseEntity<Guid>
    {
        public string Name { get; set; }

        public void UpdateName(String Name)
        {
            this.Name = Name;
        }
    }
}
