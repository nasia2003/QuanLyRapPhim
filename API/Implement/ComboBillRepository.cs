using API.Data;
using API.Entities;
using API.Service;

namespace API.Implement
{
    public class ComboBillRepository : IItemBillRepository<ComboBill>
    {
        private readonly CinemaDbContext _context;

        public ComboBillRepository(CinemaDbContext context) 
        {
            _context = context;
        }

        public async Task<ComboBill> Create(ComboBill ItemBill)
        {
            await _context.ComboBills.AddAsync(ItemBill);
            await _context.SaveChangesAsync();
            return ItemBill;
        }

        public async Task<ComboBill> Delete(ComboBill ItemBill)
        {
            _context.ComboBills.Remove(ItemBill);
            await _context.SaveChangesAsync();
            return ItemBill;
        }

        public async Task<ComboBill?> GetByKey(string BillID, Guid ItemID)
        {
            return await _context.ComboBills.FindAsync(BillID, ItemID);
        }
    }
}
