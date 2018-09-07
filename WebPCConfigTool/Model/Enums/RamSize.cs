using System.ComponentModel;

namespace WebPCConfigTool.Model.Enums
{
    public enum RamSize
    {
        [Description("2048 MB")]
        M2048 = 1,

        [Description("4096 MB")]
        M4096 = 2,

        [Description("8192 MB")]
        M8192 = 3,
        
        [Description("16 G")]
        G16 = 4,

        [Description("32 G")]
        G32 = 5
    }
}