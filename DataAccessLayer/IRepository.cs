﻿using System.Linq;
using System.Data;
namespace DataAccessLayer
{
    public interface IRepository<T> where T: Models.BaseEntity //Microsoft.AspNetCore.Identity.IdentityUser<System.Guid>
    {
        bool Insert(T entity);
        bool Update(T entity);
        bool Delete(T entity);

        bool DeleteById(System.Guid id);

        T GetById(System.Guid id);
              
        System.Collections.Generic.IEnumerable<T> Get
            (System.Linq.Expressions.Expression<System.Func<T, bool>> filter = null,
            System.Func<System.Linq.IQueryable<T>, System.Linq.IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");
                 
        bool Save();
                
        bool IsExist(System.Guid id);
        //*****************************************


    }
}
