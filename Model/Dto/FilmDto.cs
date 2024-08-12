using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto
{
    public class FilmDto
    {
        public string Name { get; set; } = null!;
        public string Summary { get; set; } = null!;
        public string Type { get; set; } = null!;
        public double Rate { get; set; }
        public DateOnly DayStart { get; set; }
        public int Duration { get; set; }
    }
}
