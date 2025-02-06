using System.Linq.Expressions;

namespace OA.Data.Interface
{
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Gets a TEntity object by TEntity id
        /// </summary>
        /// <param name="id">Key: unique identify</param>
        /// <returns>Single TEntity </returns>
        TEntity GetById(int id);

        /// <summary>
        /// Gets a TEntity object by TEntity id
        /// </summary>
        /// <returns>Single TEntity </returns>
        TEntity GetOne();

        /// <summary>
        /// Get a Llist off all TEntity
        /// </summary>
        /// <returns>List of TEntity</returns>
        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> GetAllFiltered(bool search);

        /// <summary>
        /// Get list of TEntity
        /// </summary>
        /// <param name="predicate">filter</param>
        /// <returns></returns>
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Saves/add an TEntity
        /// </summary>
        /// <param name="entity">TEntity to be saved</param>
        void Insert(TEntity entity);

        /// <summary>
        /// Add a list of entity to the database
        /// </summary>
        /// <param name="entities">List of entities to insert</param>
        void InserRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Update the TEntity
        /// </summary>
        /// <param name="entity">TEntity to be updated</param>
        void Update(TEntity entity);

        /// <summary>
        /// Delete/Remove a TEntity
        /// </summary>
        /// <param name="entity">a TEntity to be removed</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Deletes an entity by its ID
        /// </summary>
        /// <param name="id">Entity identify</param>
        void DeleteById(int id);
    }
}
