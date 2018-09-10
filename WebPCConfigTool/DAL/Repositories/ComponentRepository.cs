using System;
using System.Collections.Generic;
using System.Linq;
using WebPCConfigTool.Common;
using WebPCConfigTool.Model;

namespace WebPCConfigTool.DAL.Repositories
{
    /// <summary>
    /// </summary>
    public class ComponentRepository : BaseRepository
    {
        /// <inheritdoc />
        public ComponentRepository() : base()
        {
        }

        public List<Component> GetPCConfiguration(long? idHDD, long? idRAM, long? idOS, long? idCPU, long? idVC)
        {
            var pcConfiguration = new List<Component>();
            AddComponent<HardDisk>(idHDD, pcConfiguration);
            AddComponent<Ram>(idRAM, pcConfiguration);
            AddComponent<Model.OperatingSystem>(idOS, pcConfiguration);
            AddComponent<Cpu>(idCPU, pcConfiguration);
            AddComponent<VideoCard>(idVC, pcConfiguration);
            return pcConfiguration;
        }

        public void InsertComponents(long? idHDD, long? idRAM, long? idOS, long? idCPU, long? idVC)
        {
            var components = GetPCConfiguration(idHDD, idRAM, idOS, idCPU, idVC);
            var pcConfig = InsertPcConfiguration("PC configuration");
            // set total price
            pcConfig.Price = components.Sum(c => c.Price*c.Quantity);
            pcConfig.Name = $"{pcConfig.Name}-{pcConfig.Id}";
            components.ForEach(c => c.PcConfigurationId = pcConfig.Id);
            var dbContext = Context as DatabaseModelContext;
            if (dbContext != null)
            {
                dbContext.Components.AddRange(components);
                dbContext.SaveChanges();
            }
            else
            {
                throw new ServiceException("DbContext is null.");
            }
        }

        /// <summary>
        /// Get components by PcConfiguration id.
        /// </summary>
        /// <param name="idPcConfiguration">PcConfiguration id</param>
        /// <returns></returns>
        public List<Component> GetComponents(long idPcConfiguration)
        {
            return GetEntities<Component>().Where(c => c.PcConfigurationId == idPcConfiguration ).ToList();
        }

        private void AddComponent<T>(long? id, List<Component> pcConfiguration)  where T : BaseEntity
        {
            if (id != null)
            {
                var entity = FindById<T>(id.Value);
                if (entity != null)
                {
                    var component = new Component
                    {
                        Name = entity.ToString(),
                        Price = entity.Price,
                        Quantity = 1
                    };

                    pcConfiguration.Add(component);
                }

            }
        }

        private PcConfiguration InsertPcConfiguration(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(); 
            }
            var dbContext = Context as DatabaseModelContext;
            var config = new PcConfiguration
            {
                Name = name,
            };
            if (dbContext != null)
            {
                var newconfig = dbContext.PcConfigurations.Add(config);
                dbContext.SaveChanges();
                config = newconfig;
            }
            else
            {
                throw new ServiceException("DbContext is null.");
            }

            return config;
        }

    }

}