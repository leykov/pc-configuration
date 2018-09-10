using System.Collections.Generic;
using System.Linq;
using WebPCConfigTool.Common;
using WebPCConfigTool.Model;
using WebPCConfigTool.Model.Enums;

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

        public void DeleteOs(long id)
        {
            if (id == -1) return;

            var os = FindById<OperatingSystem>(id);
            if (os == null)
            {
                throw new ServiceException($"Missing Os with id: {id}");
            }
            var dbContext = Context as DatabaseModelContext;
            if (dbContext != null)
            {
                dbContext.OperatingSystems.Remove(os);
                dbContext.SaveChanges();
            }
        }

        public void InsertOs(string name, decimal price, int compType)
        {
            if (string.IsNullOrEmpty(name))
            {
                return;
            }
            var dbContext = Context as DatabaseModelContext;
            var newOs = new OperatingSystem
            {
                Name = name,
                Price = price,
                OsType = (OperatingSystemType)compType,
            };
            if (dbContext != null)
            {
                dbContext.OperatingSystems.Add(newOs);
                dbContext.SaveChanges();
            }
        }

        public IDictionary<int, string> GetOsType()
        {
            return Enumeration.GetAll<OperatingSystemType>();
        }

    }
}