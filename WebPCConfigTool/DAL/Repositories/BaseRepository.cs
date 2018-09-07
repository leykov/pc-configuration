using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using WebPCConfigTool.Common;
using WebPCConfigTool.Model;

namespace WebPCConfigTool.DAL.Repositories
{
    /// <summary>
    /// To be extended by all repositories. 
    /// Contains some usefull methods that can be reused.
    /// </summary>
    public class BaseRepository : IBaseRepository
    {
        /// <summary>
        /// The database model context used.
        /// </summary>
        protected IDbContext Context { get; set; }

        /// <summary>
        /// Constructs BaseRepository with database model context.
        /// </summary>
        /// <param name="context">The database model context used.</param>
        public BaseRepository()
        {
            Context = new DatabaseModelContext();
        }

        /// <summary>
		/// Gets all the entities.
		/// </summary>
		/// <returns><see cref="IQueryable{T}"/> object.</returns>
		protected IQueryable<T> GetEntities<T>(Expression<Func<T, bool>> predicate = null) where T : Entity
        {
            IQueryable<T> query = Context.Set<T>();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query;
        }

        /// <inheritdoc/>
		public List<T> FindAll<T>(Expression<Func<T, bool>> predicate = null) where T : Entity
        {
            IQueryable<T> query = Context.Set<T>();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query.ToList();
        }

        /// <inheritdoc/>
        public virtual T FindById<T>(long id) where T : Entity
        {
            try
            {
                var entity = Context.Set<T>().Local.FirstOrDefault(o => o.Id == id) ??
                    GetEntities<T>().FirstOrDefault(o => o.Id == id);

                return entity;
            }
            catch (Exception e)
            {
                //logger.Error(e);
                throw new ServiceException("An error occured during the database read.", e);
            }
        }

        /// <inheritdoc />
        public Dictionary<long, T> FindMultipleById<T>(List<long> ids) where T : Entity
        {
            if (ids.Count == 0)
            {
                return new Dictionary<long, T>();
            }

            return GetEntities<T>().Where(o => ids.Contains(o.Id)).ToDictionary(r => r.Id, r => r);
        }

        /// <inheritdoc/>
		public IQueryable<TElement> LoadCollection<T, TElement>(T entity, Expression<Func<T, ICollection<TElement>>> selector)
            where T : Entity
            where TElement : Entity
        {
            if (entity == null) throw new ArgumentNullException("entity");

            var query = Context.Entry(entity).Collection(selector).Query();
            query.Load();
            return query;
        }

        /// <inheritdoc/>
        public TProperty LoadReference<T, TProperty>(T entity, Expression<Func<T, TProperty>> navigationProperty)
            where T : Entity
            where TProperty : Entity
        {
            var property = Context.Entry(entity).Reference(navigationProperty);
            property.Load();
            return property.CurrentValue;
        }

        ///// <summary>
        ///// Prepare orderBy based on <see cref="List{SearchOrderDto}"/>.
        ///// </summary>
        ///// <param name="order">the <see cref="List{SearchOrderDto}"/></param>
        ///// <returns>orderBy as string</returns>
        //protected string PrepareOrderBy(List<SearchOrderDto> order)
        //{
        //    var orderBy = new StringBuilder();
        //    foreach (var searchOrder in order)
        //    {
        //        var ascDesc = searchOrder.Asc ? " ASC" : " DESC";
        //        if (orderBy.Length > 0)
        //        {
        //            orderBy.Append(", ");
        //        }
        //        orderBy.Append(searchOrder.Column).Append(ascDesc);
        //    }
        //    return orderBy.ToString();
        //}

        /// <summary>
        /// Creates a raw SQL query that will return elements of the given generic type.
        /// The type can be any type that has properties that match the names of the columns returned from the query,
        /// or can be a simple primitive type. The type does not have to be an entity type.
        /// The results of this query are never tracked by the context even if the type of object returned is an entity type.
        /// </summary>
        /// <typeparam name="T">The type of object returned by the query.</typeparam>
        /// <param name="query">The SQL query string.</param>
        /// <param name="parameters">The parameters to apply to the SQL query string.</param>
        /// <returns>Result.</returns>
        protected virtual IEnumerable<T> SqlQuery<T>(string query, params SqlParameter[] parameters)
        {
            return Context.SqlQuery<T>(query, parameters);
        }

        /// <summary>
        /// Formats a date for including in a SQL query.
        /// </summary>
        /// <param name="dateTime">The date object.</param>
        /// <returns>The formatted string.</returns>
        protected static string FormatSqlDate(DateTime dateTime)
        {
            return "'" + dateTime.ToString("yyyy-MM-dd") + "'";
        }

        /// <summary>
        /// Excapes and formats a string for including in a SQL query.
        /// </summary>
        /// <param name="value">The string.</param>
        /// <returns>The formatted string.</returns>
        protected static string FormatSqlString(string value)
        {
            return "'" + value.Replace("'", "''") + "'";
        }


        /// <summary>
        /// Prepares an SQL in clause based on given field and search elements.
        /// </summary>
        /// <param name="fieldName">The database field name.</param>
        /// <param name="elements">List with where clause search criteria.</param>
        /// <returns>SQL in clause.</returns>
        protected string PrepareInClause(string fieldName, List<long> elements)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append(" and ").Append(fieldName).Append(" in (");

            var i = 1;

            foreach (var element in elements)
            {
                stringBuilder.Append(element.ToString());
                if (i != elements.Count)
                { 
                    stringBuilder.Append(",");
                }

                i++;
            }

            stringBuilder.Append(")");

            return stringBuilder.ToString();

        }

        /// <summary>
        /// Get DBSet by Type as <see cref="IQueryable"/>.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected IQueryable SetByTypeAsIQueryable(Type type)
        {

            // Get the generic type definition
            var method = typeof(IDbContext).GetMethod(nameof(IDbContext.Set), BindingFlags.Public | BindingFlags.Instance);

            // Build a method with the specific type argument you're interested in
            method = method.MakeGenericMethod(type);

            return method.Invoke(Context, null) as IQueryable;
        }

    }
}
