using System.Collections.Generic;
using System.Linq;
using WebPCConfigTool.Model;

namespace WebPCConfigTool.DAL.Repositories
{
    /// <summary>
    /// Implementation of <see cref="RamSizeRepository"/>.
    /// </summary>
    public class RamSizeRepository : BaseRepository
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public RamSizeRepository() : base()
        {
        }

        /// <inheritdoc />
        public List<RamSize> GetAll()
        {
            return GetEntities<RamSize>().ToList();
        }
    }
}
