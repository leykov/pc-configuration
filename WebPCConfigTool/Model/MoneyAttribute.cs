using System;

namespace WebPCConfigTool.Model
{
    /// <summary>
    /// The Money attribute class allows us to decorate our Entity Models to specify decimal precision values for the database column used for storing money.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class MoneyAttribute : Attribute
    {


    }
}
