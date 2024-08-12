using API.Entities;

namespace API.Service
{
    public interface ITicketRepository
    {
        Task<Ticket?> GetByID(Guid ID);
        Task<Ticket> Create(Ticket ticket);
        Task<Ticket> Update(Ticket ticket);
        Task<Ticket> Delete(Ticket ticket);
    }
}
