using System.ComponentModel;

namespace WebPCConfigTool.Model.Enums
{
    public enum HardDiskType
    {
        /// <summary>
        /// Regular SATA.
        /// </summary>
        [Description("Regular SATA")]
        SATA = 1,

        /// <summary>
        /// SATA III.
        /// </summary>
        [Description("SATA III")]
        SATAIII = 2,

        /// <summary>
        /// Regular BULK.
        /// </summary>
        [Description("Regular BULK")]
        BULK = 3,
        
        /// <summary>
        /// External.
        /// </summary>
        [Description("External")]
        External = 4,

        /// <summary>
        /// SSD.
        /// </summary>
        [Description("SSD")]
        SSD = 5
    }
}