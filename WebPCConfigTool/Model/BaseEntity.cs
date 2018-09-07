using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPCConfigTool.Model
{
    /// <summary>
    /// BaseEntity is the root of all entities having information about when it was created and modified.
    /// </summary>
    public class BaseEntity : Entity
    {
        /// <summary>
        /// Date and time then the entity was created.
        /// </summary>
        [Required]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Date and time then the entity was last modified.
        /// </summary>
        [Required]
        public DateTime ModifiedOn { get; set; }

        /// <summary>
        /// The price of entity.
        /// </summary>
        [Money]
        [Required]
        public decimal Price { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{base.ToString()}";
        }
    }
}
