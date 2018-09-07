using System.Data.Entity.ModelConfiguration;

namespace WebPCConfigTool.DAL.Mapping
{
    /// <summary>
    /// Base mapping class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class KpmgEntityTypeConfiguration<T> : EntityTypeConfiguration<T> where T : class
    {
        /// <summary>
        /// ctor
        /// </summary>
        protected KpmgEntityTypeConfiguration()
        {
            PostInitialize();
        }

        /// <summary>
        /// Developers can override this method in custom partial classes
        /// in order to add some custom initialization code to constructors
        /// </summary>
        protected virtual void PostInitialize()
        {
            
        }
    }
}