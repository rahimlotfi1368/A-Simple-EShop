using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
namespace DataAccessLayer
{
    public class Repository<T> : IRepository<T> where T : Models.BaseEntity// Microsoft.AspNetCore.Identity.IdentityUser<Guid>//Models.BaseEntity
    {

        //************************************************************
        protected DbSet<T> DbSet { get; set; }
        protected Models.DataBaseContext DataBaseContext { get; set; }

        public Repository(Models.DataBaseContext dataBaseContext)
        {
            if (dataBaseContext==null)
            {
                dataBaseContext = new Models.DataBaseContext();
            }

            DataBaseContext = dataBaseContext;
            DbSet = DataBaseContext.Set<T>();
        }
        //************************************************************

        public bool Insert(T entity)
        {
            try
            {
                DbSet.Add(entity);
                return true;
            }
            catch (System.Exception)
            {
                return false;
                throw;
            }
        }
        /// <summary>
        /// If you have an entity that you know already exists in the database but which is not currently being tracked by the context then you can tell the context to track the entity using the Attach method on DbSet.
        /// The entity will be in the Unchanged state in the context.=> DbSet.Attach(entity);
        /// 
        /// If you have an entity that you know already exists in the database but to which changes may have been made then you can tell the context to attach the entity and set its state to Modified.
        /// When you change the state to Modified all the properties of the entity will be marked as modified and all the property values will be sent to the database when SaveChanges is called.
        /// =>DataBaseContext.Entry(entity).State = EntityState.Modified; 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(T entity)
        {
            try
            {
                DbSet.Attach(entity);
                DataBaseContext.Entry(entity).State = EntityState.Modified;
                return true;
            }
            catch (System.Exception)
            {
                return false;
                throw;
            }
        }

        public bool Delete(T entity)
        {
            try
            {
                if (DataBaseContext.Entry(entity).State==EntityState.Detached)
                {
                    DbSet.Attach(entity);
                }

                DbSet.Remove(entity);

                return true;
            }
            catch (System.Exception)
            {
                return false;
                throw;
            }
        }

        //*****************************************

        public bool DeleteById(System.Guid id)
        {
            try
            {
                var theEntity = GetById(id);
                Delete(theEntity);
                return true;
            }
            catch (System.Exception)
            {
                return false;
                throw;
            }
        }

        public T GetById(System.Guid id)
        {
            T theEntity = DbSet
                        .Where(current => current.Id == id)
                        .FirstOrDefault();

            return (theEntity);
        }

        //*****************************************
              
        //This code Needs to be Impreoved
        public virtual System.Collections.Generic.IEnumerable<T> Get(
           Expression<Func<T, bool>> filter = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           string includeProperties = "")
        {
            IQueryable<T> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

             

        //*****************************************
        public bool Save()
        {
            try
            {
                DataBaseContext.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {
                return false;
                throw;
            }
        }

        public bool IsExist(Guid id)
        {
            bool result = DbSet
                        .Where(current => current.Id == id)
                        .Any();

            return result;
        }

    }
}
