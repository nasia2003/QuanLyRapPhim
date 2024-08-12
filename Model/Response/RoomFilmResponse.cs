using Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Response
{
    public class RoomFilmResponse
    {
        public DateOnly DayPremiered { get; set; }
        public TimeOnly TimePremiered { get; set; }
        public long Price { get; set; }
        public FilmDto FilmDto { get; set; } = null!;
        public int RoomID { get; set; }
    }
}
