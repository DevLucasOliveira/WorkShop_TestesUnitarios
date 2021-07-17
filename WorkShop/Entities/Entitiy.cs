using System;

namespace WorkShop.Entities
{
    public abstract class Entity : IEquatable<Entity>
    {
        public Entity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }

        public bool Equals(Entity other)
        {
            return Id == other.Id;
        }

    }
}