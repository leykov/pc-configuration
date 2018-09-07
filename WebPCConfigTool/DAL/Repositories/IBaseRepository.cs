using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebPCConfigTool.Model;

namespace WebPCConfigTool.DAL.Repositories
{
    /// <summary>
    /// To be extended by all repositories. 
    /// Contains some usefull methods that can be reused.
    /// </summary>
    public interface IBaseRepository
    {
        /// <summary>
        /// Finds an entity with the given primary key values.
        /// If an entity with the given primary key values exists in the context, then it is
        /// returned immediately without making a request to the store.  Otherwise, a request
        /// is made to the store for an entity with the given primary key values and this entity,
        /// if found, is attached to the context and returned.  If no entity is found in the
        /// context or the store, then null is returned.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="id">Primary key for the entity to be found.</param>
        /// <returns>The entity.</returns>
        T FindById<T>(long id) where T : Entity;

        /// <summary>
		/// Gets all the entities.
		/// </summary>
		/// <returns><see cref="List{T}"/> object.</returns>
		List<T> FindAll<T>(Expression<Func<T, bool>> predicate = null) where T : Entity;

        /// <summary>
        /// Finds multiple entities with given primary key values.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="ids">The list of IDs.</param>
        /// <returns>A dictionary with the entities by their primary key.</returns>
        Dictionary<long, T> FindMultipleById<T>(List<long> ids) where T : Entity;

        /// <summary>
		/// Gets an object that represents the collection navigation property from this
		/// entity to a collection of related entities.
		/// </summary>
		/// <typeparam name="T">The type of base entity.</typeparam>
		/// <typeparam name="TElement">The type of elements in the collection.</typeparam>
		/// <param name="entity">Entity.</param>
		/// <param name="selector">An expression representing the navigation property.</param>
		/// <returns>A query representing the navigation property.</returns>
		IQueryable<TElement> LoadCollection<T, TElement>(T entity, Expression<Func<T, ICollection<TElement>>> selector)
            where T : Entity
            where TElement : Entity;

        /// <summary>
		/// Loads the entity from the database.
		/// Note that if the entity already exists in the context, then it will not overwritten with values from the database.
		/// </summary>
		/// <typeparam name="T">The type of base entity</typeparam>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="entity">Entity.</param>
		/// <param name="navigationProperty">Property expression.</param>
		TProperty LoadReference<T, TProperty>(T entity, Expression<Func<T, TProperty>> navigationProperty)
            where T : Entity
            where TProperty : Entity;
    }
}
