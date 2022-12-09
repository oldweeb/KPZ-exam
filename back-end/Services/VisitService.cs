using System.Linq.Expressions;
using AutoMapper;
using back_end.DB;
using back_end.Models;
using back_end.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace back_end.Services
{
    public class VisitService : IVisitService
    {
        private readonly VetClinicDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAnimalService _animalService;

        public VisitService(VetClinicDbContext context, IMapper mapper, IAnimalService animalService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _animalService = animalService ?? throw new ArgumentNullException(nameof(animalService));
        }

        public async Task Add(VisitDto visit)
        {
            var entity = _mapper.Map<Visit>(visit);
            Animal? animal = await _animalService.Get(visit.AnimalId);

            if (animal is null)
            {
                throw new KeyNotFoundException($"Animal with id {visit.AnimalId} does not exist");
            }

            entity.Animal = animal;
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Visit?> Get(Guid id)
        {
            return await _context.Visits.FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<Visit?> Get(Expression<Func<Visit, bool>> predicate)
        {
            return await _context.Visits.FirstOrDefaultAsync(predicate);
        }

        public IQueryable<Visit> GetAll(Expression<Func<Visit, bool>>? predicate = null)
        {
            return predicate == null ? _context.Visits.AsQueryable() : _context.Visits.Where(predicate);
        }

        public async Task Update(Guid id, VisitDto visit)
        {
            Visit? entity = await Get(id);

            if (entity is null)
            {
                throw new KeyNotFoundException($"Visit with id {id} does not exist");
            }

            entity.DateOfVisit = visit.DateOfVisit;
            entity.Diagnosis = visit.Diagnosis;

            Animal? animal = await _animalService.Get(visit.AnimalId);
            if (animal is null)
            {
                throw new KeyNotFoundException($"Animal with id {id} does not exist");
            }

            entity.Animal = animal;
            _context.Visits.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Delete(Guid id)
        {
            Visit? entity = await Get(id);

            if (entity is null)
            {
                return false;
            }

            _context.Visits.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
