using API.Data;
using API.Entities;
using API.Service;
using Microsoft.EntityFrameworkCore;
using Model.Dto;

namespace API.Implement
{
    public class FoodRepository : IFoodRepository
    {
        private readonly CinemaDbContext _context;

        public FoodRepository(CinemaDbContext context) 
        {
            _context = context;
        }

        public async Task<Food> Create(Food food)
        {
            await _context.Foods.AddAsync(food);
            await _context.SaveChangesAsync();
            return food;
        }

        public async Task<Food> Delete(Food food)
        {
            _context.Foods.Remove(food);
            await _context.SaveChangesAsync();
            return food;
        }

        public ICollection<Food> GetAllFood(Pagination query)
        {
            var source = _context.Foods.AsQueryable().OrderBy(x => new {x.Name, x.Size});
            return PaginatedList<Food>.Create(source, query);
        }

        public async Task<Food?> GetFoodByID(Guid ID)
        {
            return await _context.Foods.FindAsync(ID);
        }

        public async Task<Food?> GetFoodByNameAndSize(string name, string size)
        {
            return await _context.Foods.SingleOrDefaultAsync(x => x.Name == name && x.Size == size);
        }

        public async Task<Food> Update(Food food)
        {
            _context.Foods.Update(food);
            await _context.SaveChangesAsync();
            return food;
        }
    }
}
