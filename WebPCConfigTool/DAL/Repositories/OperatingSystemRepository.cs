using System.Collections.Generic;
using System.Linq;
using WebPCConfigTool.Model;

namespace WebPCConfigTool.DAL.Repositories
{
    /// <summary>
    /// Implementation of <see cref="OperatingSystemRepository"/>.
    /// </summary>
    public class OperatingSystemRepository : BaseRepository
    {
        /// <inheritdoc />
        public OperatingSystemRepository() : base()
        {
        }

        public List<OperatingSystem> GetAllOSs()
        {
            return GetEntities<OperatingSystem>().ToList();
        }
    }
}