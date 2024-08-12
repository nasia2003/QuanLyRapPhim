using Model.Dto;

namespace API.Entities
{
    public class Food
    {
        public Guid ID { get; set; }
        public string Name { get; set; } = null!;
        public long Price { get; set; }
        public String Size { get; set; } = null!;
        virtual public ICollection<FoodCombo> FoodCombos { get; set; } = null!;
        virtual public ICollection<FoodBill> FoodBills { get; set; } = null!;

        public Food() { }

        public Food(FoodDto foodDto)
        {
            Update(foodDto);
        }

        public void Update(FoodDto foodDto)
        {
            Name = foodDto.Name;
            Price = foodDto.Price;
            Size = foodDto.Size;
        }

        public FoodDto ToDto()
        {
            var foodDto = new FoodDto
            {
                Name = this.Name,
                Price = this.Price,
                Size = this.Size,
            };
            return foodDto;
        }

        public ChooseFood ToChooseFood()
        {
            return new ChooseFood
            {
                Name = this.Name,
                Size = this.Size,
            };
        }
    }
}
