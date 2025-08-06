using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAS.EventService.Domain.Entities;
using SAS.EventsService.Domain.Events.Entities;
using SAS.EventsService.Domain.Notifications.Entitties;
using SAS.EventsService.Domain.Regions.Entities;
using SAS.EventsService.Domain.UserInterests.Entities;
using System.Reflection.Emit;

namespace SAS.EventsService.Infrastructure.Persistence.EntitiesConfiguration
{
    // Event Entity Configuration
    public class EventEntityConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {

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
    
            builder.HasIndex("TopicId")
               .HasDatabaseName("IX_Event_TopicId");
            // Configure one-to-many relationship between Event and NamedEntityMention
            builder
                .HasMany(e => e.NamedEntityMentions)
                .WithOne(nem => nem.Event)
                .HasForeignKey(nem => nem.EventId)
                .IsRequired();

            // Configure many-to-many via NamedEntityMention join entity
            builder
                .HasMany(e => e.MentionedEntities)
                .WithMany() // Assuming NamedEntity does not have navigation to Event
                .UsingEntity<NamedEntityMention>(
                    j => j
                        .HasOne(nem => nem.NamedEntity)
                        .WithMany()
                        .HasForeignKey(nem => nem.NamedEntityId)
                        .IsRequired(),
                    j => j
                        .HasOne(nem => nem.Event)
                        .WithMany(e => e.NamedEntityMentions)
                        .HasForeignKey(nem => nem.EventId)
                        .IsRequired(),
                    j =>
                    {
                        j.HasKey(t => t.Id);
                        j.ToTable("NamedEntityMentions");
                    }
                );
        

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
    public class NotificationEntityConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(n => n.Id);
            builder.Property(n => n.Type).IsRequired();
            builder.Property(n => n.UserId).IsRequired();
            builder.Property(n => n.CreatedAt).IsRequired();
            builder.Property(n => n.IsRead).IsRequired();

            builder.ToTable("Notifications");

            // Configure TPH (Table-Per-Hierarchy) inheritance

            builder.HasDiscriminator<string>("Type")
                   .HasValue<EventNotification>(NotificationType.Event.ToString());
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


            builder
                .HasIndex(l => new { l.Latitude, l.Longitude })
                .HasDatabaseName("IX_Location_Lat_Lon");

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

            builder.HasIndex(t => t.Name)
              .HasDatabaseName("IX_Topic_Name")
              .IsUnique(false);
        }
    }

    // UserInterestRegion Entity Configuration
    public class UserInterestRegionEntityConfiguration : IEntityTypeConfiguration<UserInterest>
    {
        public void Configure(EntityTypeBuilder<UserInterest> builder)
        {
            builder.HasKey(u => u.Id);  // Set primary key
                                        //builder.HasOne(u => u.Region)  // Relationship with Region
                                        //       .WithMany()
                                        //       .HasForeignKey(u => u.RegionId);

        }
    }

}
