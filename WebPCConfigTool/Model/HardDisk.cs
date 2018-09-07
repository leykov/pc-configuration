using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebPCConfigTool.Model.Enums;

namespace WebPCConfigTool.Model
{
    /// <summary>
    /// Represents Organisation.
    /// </summary>
    [Table("HardDisk")]
    public class HardDisk : BaseEntity
    {
        /// <summary>
        /// Default Constructor.
        /// </summary>
        public HardDisk()
        {
        }

        /// <summary>
        /// The Name of the Organisation.
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// HDD type.
        /// </summary>
        public HardDiskType DiskType { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"HDD {Name},  {base.ToString()}";
        }
    }
}
