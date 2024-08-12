using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto
{
    public class TicketDto
    {
        public String Chair { get; set; } = null!;
        public Guid RoomFilmID { get; set; }
    }
}
