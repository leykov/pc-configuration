using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPCConfigTool.Model
{
    /// <summary>
    /// Represents Individual or User.
    /// </summary>
    public class Component  
    { 
        /// <summary>
        /// Default contructor.
        /// </summary>
        public Component()
        {
        }

        public string Name { get; set; }
        public decimal Price { get; set; }

        ///// <summary>
        ///// Direct access to <see cref="Ram"/> entity id.
        ///// </summary>
        //[ForeignKey("Ram")]
        //public long RamId { get; set; }

        ///// <summary>
        ///// Foreign Key to Ram.
        ///// </summary>
        //public virtual Ram Ram { get; set; }

        ///// <summary>
        ///// Direct access to <see cref="HardDisk"/> entity id.
        ///// </summary>
        //[ForeignKey("HardDisk")]
        //public long HardDiskId { get; set; }

        ///// <summary>
        ///// Foreign Key to HardDisk.
        ///// </summary>
        //public virtual HardDisk HardDisk { get; set; }
    }
}
