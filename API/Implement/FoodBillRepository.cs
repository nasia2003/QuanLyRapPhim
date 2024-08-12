using API.Data;
using API.Entities;
using API.Service;

namespace API.Implement
{
    public class FoodBillRepository : IItemBillRepository<FoodBill>
    {
        private readonly CinemaDbContext _context;

        public FoodBillRepository(CinemaDbContext context)
        {
            _context = context;
        }

        public async Task<FoodBill> Create(FoodBill ItemBill)
        {
            await _context.FoodBills.AddAsync(ItemBill);
            await _context.SaveChangesAsync();
            return ItemBill;
        }

        public async Task<FoodBill> Delete(FoodBill ItemBill)
        {
            _context.FoodBills.Remove(ItemBill);
            await _context.SaveChangesAsync();
            return ItemBill;
        }

        public async Task<FoodBill?> GetByKey(string BillID, Guid ItemID)
        {
            return await _context.FoodBills.FindAsync(BillID, ItemID);
        }
    }
}
