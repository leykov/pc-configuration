using System.Collections.Generic;
using WebPCConfigTool.Model;

namespace WebPCConfigTool.DAL.Repositories
{
    /// <summary>
    /// </summary>
    public class ComponentRepository : BaseRepository
    {
        private readonly List<Component> pcConfig;

        /// <inheritdoc />
        public ComponentRepository() : base()
        {
            pcConfig = new List<Component>();
        }

        public List<Component> GetPCConfiguration(long? idHDD, long? idRAM)
        {
            //var pcConfig = new List<Component>();
            AddComponent<HardDisk>(idHDD, pcConfig);
            AddComponent<Ram>(idRAM, pcConfig);
            return pcConfig;
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
                        Price = entity.Price
                    };

                    pcConfiguration.Add(component);
                }

            }
        }
    }

}