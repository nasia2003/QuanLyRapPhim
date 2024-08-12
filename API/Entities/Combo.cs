using API.Implement;
using API.Service;
using Microsoft.EntityFrameworkCore;
using Model.Dto;
using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class Combo
    {
        public Guid ID { get; set; }
        public String Name { get; set; } = null!;
        public long Price { get; set; }
        virtual public ICollection<FoodCombo> FoodCombos { get; set; } = null!;
        virtual public ICollection<ComboBill> ComboBills { get; set; } = null!;

        public Combo() 
        {
        }

        public Combo(ComboDto comboDto)
        {
            Update(comboDto);
        }

        public void Update(ComboDto comboDto)
        {
            Name = comboDto.Name;
            Price = comboDto.Price;
        }

        public ComboDto ToDto()
        {
            var comboDto = new ComboDto()
            {
                Name = this.Name,
                Price = this.Price,
                Foods = this.FoodCombos.Select(x => x.Food.ToChooseFood()).ToList()
            };
            return comboDto;
        }
    }
}
