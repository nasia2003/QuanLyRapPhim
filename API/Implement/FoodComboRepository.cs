using API.Data;
using API.Entities;
using API.Service;
using Microsoft.EntityFrameworkCore;

namespace API.Implement
{
    public class FoodComboRepository : IFoodComboRepository
    {
        private readonly CinemaDbContext _context;

        public FoodComboRepository(CinemaDbContext context) 
        {
            _context = context;
        }

        public async Task<FoodCombo> Create(FoodCombo foodCombo)
        {
            await _context.FoodCombos.AddAsync(foodCombo);
            await _context.SaveChangesAsync();
            return foodCombo;
        }

        public async Task<FoodCombo> Delete(FoodCombo foodCombo)
        {
            _context.FoodCombos.Remove(foodCombo);
            await _context.SaveChangesAsync();
            return foodCombo;
        }

        public async Task<FoodCombo?> GetByKey(FoodCombo foodCombo)
        {
            return await _context.FoodCombos.FindAsync(foodCombo);
        }
    }
}
