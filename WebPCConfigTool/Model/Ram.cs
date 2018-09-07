using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        /// Direct access to <see cref="RamSize"/> entity id.
        /// </summary>
        [ForeignKey("RamSize")]
        public long RamSizeId { get; set; }

        /// <summary>
        /// Foreign Key to RamSize.
        /// </summary>
        public virtual RamSize RamSize { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"RAM {Name},  {base.ToString()}";
        }

    }
}
