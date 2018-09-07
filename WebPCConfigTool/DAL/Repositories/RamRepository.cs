using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebPCConfigTool.Common;
using WebPCConfigTool.Model;

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

        ///// <inheritdoc />
        //public IndividualDto GetIndividualById(long individualId)
        //{
        //    var individualQuery = from individual in GetEntities<Ram>()

        //                          join title in GetEntities<IndividualTitle>() on individual.TitleId equals title.Id

        //                          join organisation in GetEntities<HardDisk>() on individual.OrganisationId equals organisation.Id

        //                          where individual.Id == individualId

        //                          select new IndividualDto
        //                          {
        //                              Id = individual.Id,
        //                              FirstName = individual.FirstName,
        //                              LastName = individual.LastName,
        //                              Email = individual.Email,
        //                              JobPosition = individual.JobPosition,
        //                              Mobile = individual.Mobile,
        //                              Telephone = individual.Telephone,
        //                              TitleText = title.Label,
        //                              OrganisationName = organisation.Name
        //                          };

        //    var individualDto = individualQuery.FirstOrDefault();
        //    if (individualDto == null)
        //    {
        //        throw new ServiceException($"Missing individual with id: {individualId}");
        //    }
        //    return individualDto;
        //}

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

        public void InsertRam(string name, long ramSizeId)
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
                    RamSizeId = ramSizeId,
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
