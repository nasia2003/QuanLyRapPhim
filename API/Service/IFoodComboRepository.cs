using API.Entities;

namespace API.Service
{
    public interface IFoodComboRepository
    {
        Task<FoodCombo?> GetByKey(FoodCombo foodCombo);
        Task<FoodCombo> Create(FoodCombo foodCombo);
        Task<FoodCombo> Delete(FoodCombo foodCombo);
    }
}
