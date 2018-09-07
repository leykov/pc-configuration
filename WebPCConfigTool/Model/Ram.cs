using Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebPCConfigTool.Model.Enums;

namespace WebPCConfigTool.Model
{
    /// <summary>
    /// Represents Individual or User.
    /// </summary>
    [Table("Ram")]
    public class Ram : BaseEntity
    {

        /// <summary>
        /// Default contructor.
        /// </summary>
        public Ram()
        {
        }

        /// <summary>
        /// First Name of the Individual.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Foreign Key to RamSize.
        /// </summary>
        [Required]
        public RamSize RamSize { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"RAM {Name}, Size={RamSize.GetDescription()}, {base.ToString()}";
        }

    }
}
