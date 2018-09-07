namespace WebPCConfigTool.DAL.Migrations
{
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Data.SqlClient;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using WebPCConfigTool.Model;
    using WebPCConfigTool.Model.Enums;

    /// <summary>
    /// Database migration configurations.
    /// </summary>
    public sealed class Configuration : DbMigrationsConfiguration<DatabaseModelContext>
    {
        /// <summary>
        /// Default constructor with some configurations.
        /// </summary>
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        /// <summary>
        /// Runs after upgrading to the latest migration to allow seed data to be updated.
        /// </summary>
        /// <param name="context">The database context which can be used to seed the database.</param>
        protected override void Seed(DatabaseModelContext context)
        {
            InitRamSizes(context);
            InitDefaultRams(context);
            InitDefaultHDDs(context);
        }

        private void InitDefaultRams(DatabaseModelContext context)
        {
            if (context.Rams.Count()>0)
            {
                return;
            }
            var rams = new List<Ram>
            {
               new Ram {
                   Name = "Transcend DDR4",
                   RamSizeId = 2,
                   Price = (decimal)352.59
                },
               new Ram {
                   Name = "Transcend DDR4",
                   RamSizeId = 3,
                   Price = (decimal)552.79
                },
               new Ram {
                   Name = "Transcend DDR4",
                   RamSizeId = 4,
                   Price = (decimal)852.9
                },
               new Ram {
                   Name = "Kingston DDR4",
                   RamSizeId = 2,
                   Price = (decimal)222.59
                },
            };
            rams.ForEach(org => context.Rams.AddOrUpdate(ram => ram.Name, org));
            context.SaveChanges();
        }

        private void InitRamSizes(DatabaseModelContext context)
        {
            if (context.RamSizes.Count() > 0)
            {
                return;
            }

            var ramSizes = new List<RamSize> {
                 new RamSize{Label = "2048 MB" },
                 new RamSize{Label = "4096 MB" },
                 new RamSize{Label = "8192 MB" },
                 new RamSize{Label = "16 G" },
                 new RamSize{Label = "32 G" },
            };
            ramSizes.ForEach(size => context.RamSizes.AddOrUpdate(title => title.Label, size));
            context.SaveChanges();
        }

        private void InitDefaultHDDs(DatabaseModelContext context)
        {
            if (context.HardDisks.Count() > 0)
            {
                return;
            }
            var hdds = new List<HardDisk>
            {
               new HardDisk {
                   Name = "Toshiba P300 - High-Performance",
                   DiskType = HardDiskType.SATA,
                   Price = (decimal)352.59
                },
               new HardDisk {
                   Name = "Samsung",
                   DiskType = HardDiskType.SSD,
                   Price = (decimal)552.79
                },
               new HardDisk {
                   Name = "Toshiba P500",
                   DiskType = HardDiskType.SATA,
                   Price = (decimal)852.9
                },
               new HardDisk {
                   Name = "Kingston",
                   DiskType = HardDiskType.BULK,
                   Price = (decimal)222.59
                },
            };
            hdds.ForEach(hdd => context.HardDisks.AddOrUpdate(d => d.Name, hdd));
            context.SaveChanges();
        }
    }
}