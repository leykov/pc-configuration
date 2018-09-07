using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using WebPCConfigTool.DAL.Mapping;
using WebPCConfigTool.Model;

namespace WebPCConfigTool.DAL
{
    /// <summary>
    /// Implements the DbContext for the Operational Database.
    /// </summary>
    public class DatabaseModelContext : DbContext, IDbContext
    {
        #region EntitySets

        /// <summary>
        /// The list of Rams.
        /// </summary>
        public DbSet<Ram> Rams { get; set; }

        /// <summary>
        /// The list of HardDisks.
        /// </summary>
        public DbSet<HardDisk> HardDisks { get; set; }

        /// <summary>
        /// List of OperatingSystems.
        /// </summary>
        public DbSet<Model.OperatingSystem> OperatingSystems { get; set; }

        /// <summary>
        /// The list of Cpus.
        /// </summary>
        public DbSet<Cpu> Cpus { get; set; }

        /// <summary>
        /// The list of VideoCards.
        /// </summary>
        public DbSet<VideoCard> VideoCards { get; set; }

        #endregion EntitySets

        /// <summary>
        /// Logger for DB Context.
        /// </summary>
        // private GraceLogger Logger { get; }

        /// <summary>
        /// Constructs a DatabaseModelContext and setup some configurations.
        /// </summary>
        public DatabaseModelContext() : base("name=DatabaseModelContext")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Configuration.AutoDetectChangesEnabled = true;
            // Added logger to DB Layer
        }

        /// <summary>
        /// Constructs a DatabaseModelContext and setup some configurations.
        /// </summary>
        /// <param name="configService">The config service implementaion.</param>
        //public DatabaseModelContext(IConfigService configService) : this()
        //{
        //    // activate db logging
        //    if (configService.IsDBLoggingEnabled)
        //    {
        //        Database.Log = s => Logger.Info(s);
        //    }
        //}

        /// <summary>
        /// Setup enitites mappings.
        /// </summary>
        /// <param name="modelBuilder">The <see cref="DbModelBuilder"/>.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => !string.IsNullOrEmpty(type.Namespace))
                .Where(type => type.BaseType != null)
                .Where(type => type.BaseType.IsGenericType)
                .Where(type => type.BaseType.GetGenericTypeDefinition() == typeof(KpmgEntityTypeConfiguration<>));

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            // configure money and percent precisions
            modelBuilder.Properties().Where(x => x.GetCustomAttributes(false).OfType<MoneyAttribute>().Any()).Configure(c => c.HasPrecision(15, 2));
        }

        /// <inheritdoc/>
        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                if (!(entry.Entity is BaseEntity))
                {
                    continue;
                }

                var now = DateTime.UtcNow;
                var entityEntry = entry.Cast<BaseEntity>();

                entityEntry.Property(ts => ts.ModifiedOn).CurrentValue = now;

                if (entityEntry.State == EntityState.Added)
                {
                    entityEntry.Property(ts => ts.CreatedOn).CurrentValue = now;
                }
                else
                {
                    // Don't override the value in the database.
                    entityEntry.Property(ts => ts.CreatedOn).IsModified = false;
                }
            }

            return base.SaveChanges();
        }

        /// <inheritDoc />
        public void Clear()
        {
            var entities = ChangeTracker.Entries().Select(e => e.Entity);
            foreach (var e in entities)
            {
                Entry(e).State = EntityState.Detached;
            }
        }

        /// <summary>
        /// Execute stores procedure and load a list of entities at the end.
        /// </summary>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <param name="commandText">Command text.</param>
        /// <param name="parameters">Parameters.</param>
        /// <returns>Entities</returns>
        public IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters) where TEntity : Entity, new()
        {
            //add parameters to command
            if (parameters != null && parameters.Length > 0)
            {
                for (var i = 0; i <= parameters.Length - 1; i++)
                {
                    if (!(parameters[i] is DbParameter p))
                        throw new Exception("Not support parameter type");

                    commandText += i == 0 ? " " : ", ";

                    commandText += "@" + p.ParameterName;
                    if (p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Output)
                    {
                        //output parameter
                        commandText += " output";
                    }
                }
            }

            var result = Database.SqlQuery<TEntity>(commandText, parameters).ToList();

            //performance hack applied as described here -
            var acd = Configuration.AutoDetectChangesEnabled;
            try
            {
                Configuration.AutoDetectChangesEnabled = false;

                for (var i = 0; i < result.Count; i++)
                    result[i] = AttachEntityToContext(result[i]);
            }
            finally
            {
                Configuration.AutoDetectChangesEnabled = acd;
            }

            return result;
        }

        /// <summary>
        /// Creates a raw SQL query that will return elements of the given generic type.
        /// The type can be any type that has properties that match the names of the columns returned from the query,
        /// or can be a simple primitive type. The type does not have to be an entity type.
        /// The results of this query are never tracked by the context even if the type of object returned is an entity type.
        /// </summary>
        /// <typeparam name="TElement">The type of object returned by the query.</typeparam>
        /// <param name="sql">The SQL query string.</param>
        /// <param name="parameters">The parameters to apply to the SQL query string.</param>
        /// <returns>Result.</returns>
        public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            return Database.SqlQuery<TElement>(sql, parameters);
        }

        /// <summary>
        /// Attach an entity to the context or return an already attached entity (if it was already attached).
        /// </summary>
        /// <typeparam name="TEntity">TEntity.</typeparam>
        /// <param name="entity">Entity.</param>
        /// <returns>Attached entity.</returns>
        protected virtual TEntity AttachEntityToContext<TEntity>(TEntity entity) where TEntity : Entity, new()
        {
            //little hack here until Entity Framework really supports stored procedures
            //otherwise, navigation properties of loaded entities are not loaded until an entity is attached to the context
            var alreadyAttached = Set<TEntity>().Local.FirstOrDefault(x => x.Id == entity.Id);
            if (alreadyAttached == null)
            {
                //attach new entity
                Set<TEntity>().Attach(entity);
                return entity;
            }

            //entity is already loaded
            return alreadyAttached;
        }
    }
}