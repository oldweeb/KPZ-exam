using System.Linq.Expressions;
using AutoMapper;
using back_end.DB;
using back_end.Models;
using back_end.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace back_end.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly VetClinicDbContext _context;
        private readonly IMapper _mapper;

        public AnimalService(VetClinicDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Add(AnimalDto animal)
        {
            var entity = _mapper.Map<Animal>(animal);
            var type = await GetType(type => type.Name == animal.Type);

            if (type is null)
            {
                type = await AddType(animal.Type);
            }

            entity.AnimalType = type;
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<AnimalType> AddType(string name)
        {
            if (await _context.AnimalTypes.AnyAsync(at => at.Name == name))
            {
                throw new InvalidOperationException("Animal type already exists.");
            }

            var type = new AnimalType()
            {
                Name = name
            };

            type = (await _context.AddAsync(type)).Entity;
            await _context.SaveChangesAsync();
            return type;
        }

        public async Task<Animal?> Get(Guid id)
        {
            return await _context.Animals.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Animal?> Get(Expression<Func<Animal, bool>> predicate)
        {
            return await _context.Animals.FirstOrDefaultAsync(predicate);
        }

        public async Task<AnimalType?> GetType(Expression<Func<AnimalType, bool>> predicate)
        {
            return await _context.AnimalTypes.FirstOrDefaultAsync(predicate);
        }

        public IQueryable<Animal> GetAll(Expression<Func<Animal, bool>>? predicate = null)
        {
            return predicate == null ? _context.Animals.AsQueryable() : _context.Animals.Where(predicate);
        }

        public async Task Update(Guid id, AnimalDto animal)
        {
            Animal? entity = await Get(id);

            if (entity is null)
            {
                throw new KeyNotFoundException($"Animal with id {id} does not exist");
            }

            entity.DateOfBirth = animal.DateOfBirth;
            entity.OwnerName = animal.OwnerName;
            entity.Name = animal.Name;

            AnimalType? type = await GetType(at => at.Name == animal.Type);
            if (type is null)
            {
                type = await AddType(animal.Type);
            }

            entity.AnimalType = type;
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Delete(Guid id)
        {
            Animal? animal = await Get(id);

            if (animal is null)
            {
                return false;
            }

            _context.Animals.Remove(animal);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
