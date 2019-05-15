using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using UserRegistration.Models;

namespace UserRegistration.DAL
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        #region Variables

        protected UserRegisterEntites context;
        protected DbSet<TEntity> dbSet;

        #endregion

        #region Constructor

        public GenericRepository(UserRegisterEntites _context)
        {
            context = _context;
            dbSet = this.context.Set<TEntity>();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Generic Get method for entities
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> GetAll()
        {
            //IQueryable<TEntity> query = this.dbSet;
            return this.dbSet.ToList();
        }

        /// <summary>
        /// Generic GetById method for entities
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TEntity> GetById(object id)
        {
            return await this.dbSet.FindAsync(id);
        }

        /// <summary>
        /// GetFirst
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public TEntity GetFirst(Expression<Func<TEntity, bool>> where)
        {
            return this.dbSet.Where(where).FirstOrDefault();
        }

        public TEntity GetByUserId(object id)
        {
            return this.dbSet.Find(id);
        }

        /// <summary>
        /// Generic method to get many record on the basis of a condition.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<TEntity> GetMany(Expression<Func<TEntity, bool>> where)
        {
            return this.dbSet.Where(where).ToList<TEntity>();
        }

        /// <summary>
        /// Generic Insert method for entities
        /// </summary>
        /// <param name="entity"></param>
        public void Insert(TEntity entity)
        {
            this.dbSet.Add(entity);
        }

        /// <summary>
        /// Generic Delete method for entities
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            DeleteEntity(entityToDelete);
        }

        /// <summary>
        /// If Entity is detached (object exists but not being tracked by context), attach it to the context. And then remove the object from DbSet.
        /// </summary>
        /// <param name="entityToDelete"></param>
        public void DeleteEntity(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                this.dbSet.Attach(entityToDelete);
            }
            this.dbSet.Remove(entityToDelete);
        }

        /// <summary>
        /// Generic update method for the entities
        /// </summary>
        /// <param name="entity"></param>
        public void Update(TEntity entity)
        {
            this.dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public bool IsDBExists()
        {
            return context.Database.Exists();
        }
        #endregion
    }
}