using Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebPCConfigTool.Model.Enums;

namespace WebPCConfigTool.Model
{
    /// <summary>
    /// Represents CPU.
    /// </summary>
    [Table("Cpu")]
    public class Cpu : BaseEntity
    {

        /// <summary>
        /// Default contructor.
        /// </summary>
        public Cpu()
        {
        }

        /// <summary>
        /// First Name of the CPU.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"Cpu {Name}, {base.ToString()}";
        }

    }
}
