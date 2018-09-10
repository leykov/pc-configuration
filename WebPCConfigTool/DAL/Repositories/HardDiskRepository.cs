using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebPCConfigTool.Common;
using WebPCConfigTool.Model;
using WebPCConfigTool.Model.Enums;

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



        public void DeleteHardDisk(long id)
        {
            if (id == -1) return;

            var HardDisk = FindById<HardDisk>(id);
            if (HardDisk == null)
            {
                throw new ServiceException($"Missing HardDisk with id: {id}");
            }
            var dbContext = Context as DatabaseModelContext;
            if (dbContext != null)
            {
                dbContext.HardDisks.Remove(HardDisk);
                dbContext.SaveChanges();
            }
        }

        public void InsertHardDisk(string name, decimal price, int compType)
        {
            if (string.IsNullOrEmpty(name))
            {
                return;
            }
            var dbContext = Context as DatabaseModelContext;
            var newHardDisk = new HardDisk
            {
                Name = name,
                Price = price,
                DiskType = (HardDiskType)compType,
            };
            if (dbContext != null)
            {
                dbContext.HardDisks.Add(newHardDisk);
                dbContext.SaveChanges();
            }
        }

        public IDictionary<int, string> GetHddType()
        {
            return Enumeration.GetAll<HardDiskType>();
        }


    }
}
