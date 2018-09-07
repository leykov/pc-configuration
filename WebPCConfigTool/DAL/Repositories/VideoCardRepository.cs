using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebPCConfigTool.Common;
using WebPCConfigTool.Model;

namespace WebPCConfigTool.DAL.Repositories
{
    /// <summary>
    /// Implementation of <see cref="VideoCardRepository"/>.
    /// </summary>
    public class VideoCardRepository : BaseRepository
    {

        /// <inheritdoc />
        public VideoCardRepository() : base()
        {
        }


        public List<VideoCard> GetAllCards()
        {
            return GetEntities<VideoCard>().ToList();
        }


    }
}
