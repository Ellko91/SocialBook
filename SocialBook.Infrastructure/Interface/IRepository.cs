using SocialBook.Infrastructure.Base;
using System.Linq.Expressions;
using System.Linq;
using System;

namespace SocialBook.Infrastructure.Interface
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();

        IQueryable<T> GetByCriteria(Expression<Func<T, bool>> predicate = null,
                                    Func<IQueryable<T>, IOrderedEnumerable<T>> orderBy = null,
                                    string includeProperties = "");

        T GetOne(Expression<Func<T, bool>> predicate);

        T Create(T entity);

        int Update(T entity);

        int Delete(T entity);

        int SaveChanges();
    }
}
