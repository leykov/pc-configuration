using Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebPCConfigTool.Model.Enums;

namespace WebPCConfigTool.Model
{
    /// <summary>
    /// Represents HardDisk.
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
        /// The Name of the HardDisk.
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
            return $"HDD {Name}, Type={DiskType.GetDescription()}, {base.ToString()}";
        }
    }
}
