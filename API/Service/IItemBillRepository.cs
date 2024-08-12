using API.Entities;

namespace API.Service
{
    public interface IItemBillRepository<T>
    {
        Task<T> Create(T ItemBill);
        Task<T?> GetByKey(String BillID, Guid ItemID);
        Task<T> Delete(T ItemBill);
    }
}
