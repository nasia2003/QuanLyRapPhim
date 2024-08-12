using API.Entities;
using Model.Dto;

namespace API.Service
{
    public interface IFoodRepository
    {
        ICollection<Food> GetAllFood(Pagination query);
        Task<Food?> GetFoodByID(Guid ID);
        Task<Food?> GetFoodByNameAndSize(String name, String size);
        Task<Food> Create(Food food);
        Task<Food> Update(Food food);
        Task<Food> Delete(Food food);
    }
}
