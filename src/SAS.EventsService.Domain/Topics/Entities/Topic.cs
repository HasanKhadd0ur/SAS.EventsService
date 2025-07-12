using SAS.EventsService.SharedKernel.Entities;

namespace SAS.EventService.Domain.Entities
{
    public class Topic : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string IconUrl { get; set; }
        public string Description { get; set; }

        public void UpdateName(String Name)
        {
            this.Name = Name;
        }
        public void UpdateIconUrl(String newIconUrl)
        {
            this.IconUrl = newIconUrl;
        }

        public void UpdateDescription(String Description)
        {
            this.Description = Description;
        }
    }
}
