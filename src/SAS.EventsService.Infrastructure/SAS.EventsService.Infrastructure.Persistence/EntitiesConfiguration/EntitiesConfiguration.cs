using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAS.EventService.Domain.Entities;
using SAS.EventsService.Domain.Events.Entities;
using SAS.EventsService.Domain.Regions.Entities;

namespace SAS.EventsService.Infrastructure.Persistence.EntitiesConfiguration
{
    // Event Entity Configuration
    public class EventEntityConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.Id);  // Set primary key

            // Configure value object (EventInfo)
            builder.OwnsOne(e => e.EventInfo, ei =>
            {
                ei.Property(e => e.Title)
                  .HasColumnName("Title")
                  .IsRequired()
                  .HasMaxLength(200);

                ei.Property(e => e.Summary)
                  .HasColumnName("Summary")
                  .HasMaxLength(1000);

                ei.Property(e => e.SentimentScore)
                  .HasColumnName("SentimentScore");
            });

            // Configure DateTime properties
            builder.Property(e => e.CreatedAt).IsRequired();
            builder.Property(e => e.LastUpdatedAt).IsRequired();

            // Relationships
            builder.HasOne(e => e.Location)
                   .WithMany()
                   .HasForeignKey("LocationId");  // Shadow FK if not declared explicitly

            builder.HasOne(e => e.Region)
                   .WithMany()
                   .HasForeignKey("RegionId");

            builder.HasOne(e => e.Topic)
                   .WithMany()
                   .HasForeignKey("TopicId");


        }
    }

    // Message Entity Configuration
    public class MessageEntityConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(m => m.Id);  // Set primary key
            builder.Property(m => m.Content).IsRequired();  // Required property
            builder.HasOne(m => m.Event)  // Relationship with Event
                   .WithMany(e => e.Messages)
                   .HasForeignKey(m => m.EventId);
        }
    }

    // Location Entity Configuration
    public class LocationEntityConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.HasKey(l => l.Id);  // Set primary key
            builder.Property(l => l.Latitude).HasColumnType("decimal(9,6)");  // Latitude column type
            builder.Property(l => l.Longitude).HasColumnType("decimal(9,6)");  // Longitude column type
            builder.Property(l => l.City).IsRequired().HasMaxLength(100);  // Required property
            builder.Property(l => l.Country).IsRequired().HasMaxLength(100);  // Required property

        }
    }

    // Region Entity Configuration
    public class RegionEntityConfiguration : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder.HasKey(r => r.Id);  // Set primary key
            builder.Property(r => r.Name).IsRequired().HasMaxLength(100);  // Required property
        }
    }

    // Topic Entity Configuration
    public class TopicEntityConfiguration : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            builder.HasKey(t => t.Id);  // Set primary key
            builder.Property(t => t.Name).IsRequired().HasMaxLength(100);  // Required property
        }
    }

    // UserInterestRegion Entity Configuration
    public class UserInterestRegionEntityConfiguration : IEntityTypeConfiguration<UserInterestRegion>
    {
        public void Configure(EntityTypeBuilder<UserInterestRegion> builder)
        {
            builder.HasKey(u => u.Id);  // Set primary key
            builder.HasOne(u => u.Region)  // Relationship with Region
                   .WithMany()
                   .HasForeignKey(u => u.RegionId);
        }
    }
}
