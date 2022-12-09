using System.Linq.Expressions;
using back_end.Models;
using back_end.Models.Dtos;

namespace back_end.Services
{
    public interface IVisitService
    {
        Task Add(VisitDto visit);
        Task<Visit?> Get(Guid id);
        Task<Visit?> Get(Expression<Func<Visit, bool>> predicate);
        IQueryable<Visit> GetAll(Expression<Func<Visit, bool>>? predicate = null);
        Task Update(Guid id, VisitDto visit);
        Task<bool> Delete(Guid id);
    }
}
