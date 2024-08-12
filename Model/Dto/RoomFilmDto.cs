using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto
{
    public class RoomFilmDto
    {
        public int RoomID { get; set; }
        public Guid FilmID { get; set; }
        public DateOnly DayPremiered { get; set; }
        public TimeOnly TimePremiered { get; set; }
        public long Price { get; set; }
    }
}
