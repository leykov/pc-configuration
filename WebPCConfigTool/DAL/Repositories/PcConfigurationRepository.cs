using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebPCConfigTool.Common;
using WebPCConfigTool.Model;

namespace WebPCConfigTool.DAL.Repositories
{
    /// <summary>
    /// Implementation of <see cref="CpuRepository"/>.
    /// </summary>
    public class PcConfigurationRepository : BaseRepository
    {

        /// <inheritdoc />
        public PcConfigurationRepository() : base()
        {
        }


        public List<PcConfiguration> GetAllConfigurations()
        {
            return GetEntities<PcConfiguration>().ToList();
        }


    }
}
