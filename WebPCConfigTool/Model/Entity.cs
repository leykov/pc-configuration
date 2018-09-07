using System;
using System.ComponentModel.DataAnnotations;

namespace WebPCConfigTool.Model
{
    /// <summary>
    /// Entity is the root of all entities. It has the Id field.
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// Primary key of the entity.
        /// </summary>
        [Key]
        [Required]
        public long Id { get; set; }

        /// <summary>
        /// Useful operators not equal.
        /// </summary>
        /// <param name="x">The entity x.</param>
        /// <param name="y">The entity y.</param>
        /// <returns>True or false.</returns>
        public static bool operator !=(Entity x, Entity y)
        {
            return !(x == y);
        }

        /// <summary>
        /// Useful operators equal.
        /// </summary>
        /// <param name="x">The entity x.</param>
        /// <param name="y">The entity y.</param>
        /// <returns>True or false.</returns>
        public static bool operator ==(Entity x, Entity y)
        {
            return Equals(x, y);
        }

        /// <summary>
        /// Override Equals.
        /// </summary>
        /// <param name="obj">The other entity.</param>
        /// <returns>True or false.</returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as Entity);
        }

        /// <summary>
        /// Check if two entities are equals.
        /// </summary>
        /// <param name="other">The other entity.</param>
        /// <returns>True or false.</returns>
        public virtual bool Equals(Entity other)
        {
            if (other == null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            { 
                return true;
            }

            if (!IsTransient(this) &&
                !IsTransient(other) &&
                Equals(Id, other.Id))
            {
                var otherType = other.GetUnproxiedType();
                var thisType = GetUnproxiedType();
                return thisType.IsAssignableFrom(otherType) ||
                        otherType.IsAssignableFrom(thisType);
            }

            return false;
        }

        /// <summary>
        /// Override get hashcode.
        /// </summary>
        /// <returns>The generated hashcode.</returns>
        public override int GetHashCode()
        {
            if (Equals(Id, default(int)))
            {
                return base.GetHashCode();
            }

            return Id.GetHashCode();
        }

        /// <summary>
        /// Override of default ToString().
        /// </summary>
        /// <returns>String representation of this class.</returns>
        public override string ToString()
        {
            return $"Id={Id}";
        }

        private static bool IsTransient(Entity obj)
        {
            return obj != null && Equals(obj.Id, default(int));
        }

        private Type GetUnproxiedType()
        {
            return GetType();
        }

    }
}
