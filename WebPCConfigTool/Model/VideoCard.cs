using Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebPCConfigTool.Model.Enums;

namespace WebPCConfigTool.Model
{
    /// <summary>
    /// Represents VC.
    /// </summary>
    [Table("VideoCard")]
    public class VideoCard : BaseEntity
    {

        /// <summary>
        /// Default contructor.
        /// </summary>
        public VideoCard()
        {
        }

        /// <summary>
        /// First Name of the VC.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"Video Card {Name}, {base.ToString()}";
        }

    }
}
