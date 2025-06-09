using Microsoft.EntityFrameworkCore;
using SAS.EventService.Domain.Entities;
using SAS.EventsService.Domain.Events.Entities;
using SAS.EventsService.Domain.Regions.Entities;
using SAS.EventsService.SharedKernel.DomainEvents;

namespace SAS.EventsService.Infrastructure.Persistence.AppDataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {

        }

        // DbSets for entities
        public DbSet<Event> Events { get; set; }  
        public DbSet<Message> Messages { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Region> Regions{ get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<UserInterest> UserInterests { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            modelBuilder.Ignore<List<IDomainEvent>>();
            base.OnModelCreating(modelBuilder);


            modelBuilder.HasAnnotation("Relational:Collation", "Arabic_CI_AS");

        }

    }
}
