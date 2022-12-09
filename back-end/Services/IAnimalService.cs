using System.Linq.Expressions;
using back_end.Models;
using back_end.Models.Dtos;

namespace back_end.Services
{
    public interface IAnimalService
    {
        Task Add(AnimalDto animal);
        Task<AnimalType> AddType(string name);
        Task<Animal?> Get(Guid id);
        Task<Animal?> Get(Expression<Func<Animal, bool>> predicate);
        Task<AnimalType?> GetType(Expression<Func<AnimalType, bool>> predicate);
        IQueryable<Animal> GetAll(Expression<Func<Animal, bool>>? predicate = null);
        Task Update(Guid id, AnimalDto animal);
        Task<bool> Delete(Guid id);
    }
}
