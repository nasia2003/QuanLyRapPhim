using API.Data;
using API.Entities;
using API.Service;
using Microsoft.EntityFrameworkCore;
using Model.Dto;

namespace API.Implement
{
    public class BillRepository : IBillRepository
    {
        private CinemaDbContext _context;

        public BillRepository(CinemaDbContext context) 
        {
            _context = context;
        }

        public async Task<Bill> Create(Bill bill)
        {
            await _context.AddAsync(bill);
            await _context.SaveChangesAsync();
            return bill;
        }

        public ICollection<Bill> GetAllBill(Pagination query)
        {
            var source = _context.Bills
                         .Include(b => b.FoodBills)
                         .ThenInclude(fb => fb.Food)
                         .Include(b => b.TicketBills)
                         .ThenInclude(tb => tb.Ticket)
                         .Include(b => b.ComboBills)
                         .ThenInclude(cb => cb.Combo)
                         .AsQueryable()
                         .OrderBy(x => x.ID);
            var list = PaginatedList<Bill>.Create(source, query);
            return list;
        }

        public async Task<Bill?> GetBillByID(string ID)
        {
            return await _context.Bills.FindAsync(ID);
        }
    }
}
