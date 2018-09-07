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
    public class CpuRepository : BaseRepository
    {

        /// <inheritdoc />
        public CpuRepository() : base()
        {
        }


        public List<Cpu> GetAllCpus()
        {
            return GetEntities<Cpu>().ToList();
        }


    }
}
