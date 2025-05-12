using SAS.EventsService.SharedKernel.DomainEvents;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAS.EventsService.SharedKernel.Entities
{
    /// <summary>
    /// The BaseEntity class serves as a foundation for all domain entities.
    /// </summary>
    public class BaseEntity<TId>
    {
        public TId Id { get; set; }

        /// <summary>
        /// Events List 
        /// </summary>
        public List<IDomainEvent> Events = new();


        #region Domain Events Management 
        /// <summary>
        /// Add a domain event to the events list 
        /// </summary>
        /// <param name="eventItem"></param>
        public void AddDomainEvent(IDomainEvent eventItem)
        {
            Events ??= new List<IDomainEvent>();
            Events.Add(eventItem);
        }

        /// <summary>
        ///  Clear the events list 
        /// </summary>
        public void ClearDomainEvents()
        {
            Events?.Clear();
        }

        /// <summary>
        /// Remove a domain events from the list of the domains events
        /// </summary>
        /// <param name="eventItem"></param>
        public void RemoveDomainEvent(IDomainEvent eventItem)
        {
            Events?.Remove(eventItem);
        }

        #endregion Domain Events Management

        #region Operators Overloading

        /// <summary>
        /// Equals Operator to Equals the entities based on there IDs
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns>the eguality of the two entities based on there IDs</returns>
        public static bool operator ==(BaseEntity first, BaseEntity second)
        {
            if (first is null && second is null)
            {
                return true;
            }

            if (first is null || second is null)
            {
                return false;
            }

            return first.Equals(second);
        }

        /// <summary>
        /// Not Equals operator 
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns>the non equlaity of two entities based on there IDs</returns>

        public static bool operator !=(BaseEntity first, BaseEntity second)
        {
            return !(first == second);
        }

        /// <summary>
        /// Equals the entity with other Entity based on there IDs
        /// </summary>
        /// <param name="other"></param>
        /// <returns>true if the two entity has the same IDs else false</returns>
        public bool Equals(BaseEntity other)
        {
            if (other is null || other.GetType() != GetType())
            {
                return false;
            }

            return other.Id == Id;
        }

        public override bool Equals(object obj)
        {
            // Check if the two have same type.
            if (obj is null || obj.GetType() != GetType())
            {
                return false;
            }

            // Check If the obj if of type Entity.
            if (obj is not BaseEntity entity)
            {
                return false;
            }

            return entity.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        #endregion Operators Overloading

    }

}
