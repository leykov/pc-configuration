using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebPCConfigTool.Model.Enums
{
    public enum OperatingSystemType
    {
        /// <summary>
        /// Windows OS.
        /// </summary>
        [Description("Windows")]
        Windows = 1,
        /// <summary>
        /// Linux OS.
        /// </summary>
        [Description("Linux")]
        Linux = 2,
        /// <summary>
        /// DOS .
        /// </summary>
        [Description("DOS")]
        DOS = 3,
    }
}