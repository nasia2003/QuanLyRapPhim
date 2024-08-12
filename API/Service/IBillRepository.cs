using API.Entities;
using Model.Dto;

namespace API.Service
{
    public interface IBillRepository
    {
        ICollection<Bill> GetAllBill(Pagination query);
        Task<Bill?> GetBillByID(String ID);
        Task<Bill> Create(Bill bill);
    }
}
