namespace WebPCConfigTool.DAL.Migrations
{
    using System;
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
            InitRams(context);
            InitHDDs(context);
            InitOSs(context);
            InitCpus(context);
            InitVideoCards(context);
        }

        private void InitOSs(DatabaseModelContext context)
        {
            if (context.OperatingSystems.Count() > 0)
            {
                return;
            }
            var operatingSystems = new List<Model.OperatingSystem>
            {
               new Model.OperatingSystem {
                   Name = "MS DOS",
                   OsType = OperatingSystemType.DOS,
                   Price = (decimal)52.5
                },
               new Model.OperatingSystem {
                   Name = "Windows HOME 10",
                   OsType = OperatingSystemType.Windows,
                   Price = (decimal)352.20
                },
               new Model.OperatingSystem {
                   Name = "Windows PRO 10",
                   OsType = OperatingSystemType.Windows,
                   Price = (decimal)857.9
                },
               new Model.OperatingSystem {
                   Name = "Ubunty 11",
                   OsType = OperatingSystemType.Linux,
                   Price = (decimal)99.99
                },
            };
            operatingSystems.ForEach(org => context.OperatingSystems.AddOrUpdate(os => os.Name, org));
            context.SaveChanges();
        }

        private void InitRams(DatabaseModelContext context)
        {
            if (context.Rams.Count()>0)
            {
                return;
            }
            var rams = new List<Ram>
            {
               new Ram {
                   Name = "Transcend DDR4",
                   RamSize = RamSize.M4096,
                   Price = (decimal)352.59
                },
               new Ram {
                   Name = "Transcend DDR4",
                   RamSize = RamSize.M8192,
                   Price = (decimal)552.79
                },
               new Ram {
                   Name = "Transcend DDR4",
                   RamSize = RamSize.G16,
                   Price = (decimal)852.9
                },
               new Ram {
                   Name = "Kingston DDR4",
                   RamSize = RamSize.M4096,
                   Price = (decimal)222.59
                },
            };
            rams.ForEach(org => context.Rams.AddOrUpdate(ram => ram.Name, org));
            context.SaveChanges();
        }

        private void InitCpus(DatabaseModelContext context)
        {
            if (context.Cpus.Count() > 0)
            {
                return;
            }
            var cpus = new List<Cpu>
            {
               new Cpu {
                   Name = "Intel Desktop Core i7-8086K",
                   Price = (decimal)952.59
                },
               new Cpu {
                   Name = "Intel Desktop Pentium G5400",
                   Price = (decimal)152.79
                },
               new Cpu {
                   Name = "Intel Desktop Core i5-8600",
                   Price = (decimal)852.9
                },
               new Cpu {
                   Name = "AMD Desktop Ryzen 7 8C/16T 2700X",
                   Price = (decimal)212.59
                },
            };
            cpus.ForEach(org => context.Cpus.AddOrUpdate(Cpu => Cpu.Name, org));
            context.SaveChanges();
        }


        private void InitVideoCards(DatabaseModelContext context)
        {
            if (context.VideoCards.Count() > 0)
            {
                return;
            }
            var videoCards = new List<VideoCard>
            {
               new VideoCard {
                   Name = "XFX AMD Radeon RX 580 GTS",
                   Price = (decimal)952.59
                },
               new VideoCard {
                   Name = "SAPPHIRE AMD Radeon PULSE ",
                   Price = (decimal)152.79
                },
               new VideoCard {
                   Name = "MSI AMD Radeon RX 560 OC",
                   Price = (decimal)852.9
                },
               new VideoCard {
                   Name = "Sapphire NITRO R7 370 4G GDDR5 PCI-E",
                   Price = (decimal)212.59
                },
            };
            videoCards.ForEach(org => context.VideoCards.AddOrUpdate(card => card.Name, org));
            context.SaveChanges();
        }

        private void InitHDDs(DatabaseModelContext context)
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