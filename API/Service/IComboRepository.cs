using API.Entities;
using Model.Dto;

namespace API.Service
{
    public interface IComboRepository
    {
        ICollection<Combo> GetAllCombo(Pagination query);
        Task<Combo?> GetComboByID(Guid ID);
        Task<Combo?> GetComboByName(String name);
        Task<Combo> Create(Combo combo);
        Task<Combo> Update(Combo combo);
        Task<Combo> Delete(Combo combo);
    }
}
