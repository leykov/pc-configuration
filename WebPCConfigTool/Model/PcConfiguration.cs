using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPCConfigTool.Model
{
    /// <summary>
    /// Represents PcConfiguration.
    /// </summary>
    [Table("PcConfiguration")]
    public class PcConfiguration : BaseEntity
    { 
        /// <summary>
        /// Default contructor.
        /// </summary>
        public PcConfiguration()
        {
        }

        /// <summary>
        /// The Name of the PcConfiguration.
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
