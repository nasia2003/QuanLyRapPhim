using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto
{
    public class ComboDto
    {
        public String Name { get; set; } = null!;
        public long Price { get; set; }
        virtual public ICollection<ChooseFood> Foods { get; set; } = null!;
    }
}
