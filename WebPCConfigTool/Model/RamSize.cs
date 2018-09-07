using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPCConfigTool.Model
{
    /// <summary>
    /// Represents Ram Size.
    /// </summary>
    [Table("RamSize")]
    public class RamSize : Entity
    {
        /// <summary>
        /// The Individual Label.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Label { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"RamSize[ Label={Label}, {base.ToString()} ]";
        }
    }
}
