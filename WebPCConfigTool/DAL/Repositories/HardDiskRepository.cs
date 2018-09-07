using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebPCConfigTool.Common;
using WebPCConfigTool.Model;

namespace WebPCConfigTool.DAL.Repositories
{
    /// <summary>
    /// Implementation of <see cref="HardDiskRepository"/>.
    /// </summary>
    public class HardDiskRepository : BaseRepository
    {

        /// <inheritdoc />
        public HardDiskRepository() : base()
        {
        }


        public List<HardDisk> GetAllDisks()
        {
            return GetEntities<HardDisk>().ToList();
        }


    }
}
