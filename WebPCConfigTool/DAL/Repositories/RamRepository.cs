using System.Collections.Generic;
using System.Linq;
using WebPCConfigTool.Common;
using WebPCConfigTool.Model;
using WebPCConfigTool.Model.Enums;

namespace WebPCConfigTool.DAL.Repositories
{
    /// <summary>
    /// </summary>
    public class RamRepository : BaseRepository
    {
        /// <inheritdoc />
        public RamRepository() : base()
        {
        }

        /// <inheritdoc />
        public List<Ram> GetAllRams()
        {
            return GetEntities<Ram>().ToList();
        }

        public void DeleteRam(long id)
        {
            if (id == -1) return;

            var ram = FindById<Ram>(id);
            if (ram == null)
            {
                throw new ServiceException($"Missing ram with id: {id}");
            }
            var dbContext = Context as DatabaseModelContext;
            if (dbContext != null)
            {
                dbContext.Rams.Remove(ram);
                dbContext.SaveChanges();
            }
        }

        public void InsertRam(string name, decimal price, int compType)
        {
            if (string.IsNullOrEmpty(name))
            {
                return;
            }
            var dbContext = Context as DatabaseModelContext;
            var newRam = new Ram
            {
                Name = name,
                Price = price,
                RamSize =  (RamSize)compType,
            };
            if (dbContext != null)
            {
                dbContext.Rams.Add(newRam);
                dbContext.SaveChanges();
            }
        }

        public IDictionary<int, string> GetRamSize()
        {
            return Enumeration.GetAll<RamSize>();
        }

    }
}