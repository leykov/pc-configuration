using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPCConfigTool.Model
{
    /// <summary>
    /// Represents Component.
    /// </summary>
    [Table("Component")]
    public class Component : BaseEntity
    { 
        /// <summary>
        /// Default contructor.
        /// </summary>
        public Component()
        {
        }

        /// <summary>
        /// The Name of the Component.
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// Quantity
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Direct access to <see cref="PcConfiguration"/> entity id.
        /// </summary>
        [ForeignKey("PcConfiguration")]
        public long PcConfigurationId { get; set; }

        /// <summary>
        /// Foreign Key to PcConfiguration.
        /// </summary>
        public virtual PcConfiguration PcConfiguration { get; set; }

    }
}
