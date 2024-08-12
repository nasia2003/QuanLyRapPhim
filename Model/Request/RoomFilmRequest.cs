using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Request
{
    public class RoomFilmRequest
    {
        public int RoomID { get; set; }
        public String FilmName { get; set; } = null!;
        public long Price { get; set; }
        public DateOnly DayPremiered { get; set; }
        public TimeOnly TimePremiered { get; set; }
    }
}
