using API.Data;
using API.Entities;
using API.Service;
using Microsoft.EntityFrameworkCore;
using Model.Dto;

namespace API.Implement
{
    public class ComboRepository : IComboRepository
    {
        private readonly CinemaDbContext _context;

        public ComboRepository(CinemaDbContext context) 
        {
            _context = context;
        }

        public async Task<Combo> Create(Combo combo)
        {
            await _context.Combos.AddAsync(combo);
            await _context.SaveChangesAsync();
            return combo;
        }

        public async Task<Combo> Delete(Combo combo)
        {
            _context.Remove(combo);
            await _context.SaveChangesAsync();
            return combo;
        }

        public ICollection<Combo> GetAllCombo(Pagination query)
        {
            var source = _context.Combos
                         .Include(c => c.FoodCombos)
                         .ThenInclude(fc => fc.Food)
                         .AsQueryable()
                         .OrderBy(x => x.Name);
            var list = PaginatedList<Combo>.Create(source, query);
            return list;
        }

        public async Task<Combo?> GetComboByID(Guid ID)
        {
            return await _context.Combos
                         .Include(c => c.FoodCombos)
                         .ThenInclude(fc => fc.Food)
                         .SingleOrDefaultAsync(x => x.ID == ID);
        }

        public async Task<Combo?> GetComboByName(string name)
        {
            return await _context.Combos.Include(c => c.FoodCombos)
                         .ThenInclude(fc => fc.Food)
                         .SingleOrDefaultAsync(x => x.Name == name);
        }

        public async Task<Combo> Update(Combo combo)
        {
            _context.Combos.Update(combo);
            await _context.SaveChangesAsync();
            return combo;
        }
    }
}
