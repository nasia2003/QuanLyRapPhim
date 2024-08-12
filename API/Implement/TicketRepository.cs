using API.Data;
using API.Entities;
using API.Service;

namespace API.Implement
{
    public class TicketRepository : ITicketRepository
    {
        private readonly CinemaDbContext _context;

        public TicketRepository(CinemaDbContext context)
        {
            _context = context;
        }

        public async Task<Ticket> Create(Ticket ticket)
        {
            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();
            return ticket;
        }

        public async Task<Ticket> Delete(Ticket ticket)
        {
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return ticket;
        }

        public async Task<Ticket?> GetByID(Guid ID)
        {
            return await _context.Tickets.FindAsync(ID);
        }

        public async Task<Ticket> Update(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();
            return ticket;
        }
    }
}
