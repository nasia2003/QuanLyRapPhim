using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto
{
    public class FoodDto
    {
        public string Name { get; set; } = null!;
        public long Price { get; set; }
        public String Size { get; set; } = null!;
    }
}
