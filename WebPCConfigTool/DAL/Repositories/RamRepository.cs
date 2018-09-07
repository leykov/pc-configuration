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

        public void DeleteRam(long ramId)
        {
            if (ramId == -1) return;

            var ram = FindById<Ram>(ramId);
            if (ram == null)
            {
                throw new ServiceException($"Missing ram with id: {ramId}");
            }
            var dbContext = Context as DatabaseModelContext;
            if (dbContext != null)
            {
                dbContext.Rams.Remove(ram);
                dbContext.SaveChanges();
            }
        }

        public void InsertRam(string name, RamSize ramSize)
        {
            if (string.IsNullOrEmpty(name))
            {
                return;
            }
            //if (FindByEmail(email) == null)
            {
                var dbContext = Context as DatabaseModelContext;
                var newRam = new Ram
                {
                    Name = name,
                    RamSize = ramSize,
                };
                if (dbContext != null)
                {
                    dbContext.Rams.Add(newRam);
                    dbContext.SaveChanges();
                }
            }
            //else
            //{
            //    throw new ServiceException($"Individual with email: {email} already exist.");
            //}
        }
    }
}