using Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebPCConfigTool.Model.Enums;

namespace WebPCConfigTool.Model
{
    /// <summary>
    /// Represents Organisation.
    /// </summary>
    [Table("OperatingSystem")]
    public class OperatingSystem : BaseEntity
    {
        /// <summary>
        /// Default Constructor.
        /// </summary>
        public OperatingSystem()
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
        public OperatingSystemType OsType { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"OS {Name}, Type={OsType.GetDescription()}, {base.ToString()}";
        }
    }
}
