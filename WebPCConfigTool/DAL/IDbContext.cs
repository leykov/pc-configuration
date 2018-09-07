using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using WebPCConfigTool.Model;

namespace WebPCConfigTool.DAL
{
    /// <summary>
    /// IDbContext definition.
    /// </summary>
    public interface IDbContext
    {
        /// <summary>
        /// The DbSet.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <returns>The <see cref="DbSet{TEntity}"/>.</returns>
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        /// <summary>
        /// Database object, for more info see: <see cref="DbContext.Database"/>.
        /// </summary>
        Database Database { get; }

        /// <summary>
        /// Tracks CreatedOn, CreatedBy, ModifiedOn and ModifiedBy fields of all entitys prior calling SaveChanges of the base DbContext.
        /// </summary>
        /// <returns>The number of state entries written to the underlying database.</returns>
        int SaveChanges();

        /// <summary>
        /// Execute stores procedure and load a list of entities at the end.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>Entities</returns>
        IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters)
            where TEntity : Entity, new();

        /// <summary>
        /// Gets a <see cref="DbEntityEntry"/> object for the given entity, for more info see: <see cref="DbContext.Entry(object)"/>.
        /// </summary>
        /// <param name="entity">The Given entity object.</param>
        /// <returns>The <see cref="DbEntityEntry"/>.</returns>
        DbEntityEntry Entry(object entity);

        /// <summary>
        /// Gets a <see cref="DbEntityEntry"/> object for the given entity and type, for more info see: <see cref="DbContext.Entry{TEntity}(TEntity)"/>.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="entity">The current Entity.</param>
        /// <returns></returns>
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Creates a raw SQL query that will return elements of the given generic type.  
        /// The type can be any type that has properties that match the names of the columns returned from the query, 
        /// or can be a simple primitive type. The type does not have to be an entity type. 
        /// The results of this query are never tracked by the context even if the type of object returned is an entity type.
        /// </summary>
        /// <typeparam name="TElement">The type of object returned by the query.</typeparam>
        /// <param name="sql">The SQL query string.</param>
        /// <param name="parameters">The parameters to apply to the SQL query string.</param>
        /// <returns>Result</returns>
        IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters);

        /// <summary>
        /// Clear the database context from all attached entities.
        /// </summary>
        void Clear();

        /// <summary>
        /// Get DbSet by <see cref="Type"/>.
        /// </summary>
        /// <param name="entityType">The <see cref="Type"/> of entit.</param>
        /// <returns>The DbSet.</returns>
        DbSet Set(Type entityType);
    }
}
