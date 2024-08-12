using Model.Dto;

namespace API.Entities
{
    public class Ticket
    {
        public Guid ID { get; set; }
        public string Chair { get; set; } = null!;
        public Guid RoomFilmID { get; set; }
        virtual public TicketBill TicketBill { get; set; } = null!;
        virtual public RoomFilm RoomFilm { get; set; } = null!;

        public Ticket()
        {
            ID = Guid.NewGuid();
        }

        public Ticket(TicketDto ticketDto) : this()
        {
            RoomFilmID = ticketDto.RoomFilmID;
            Chair = ticketDto.Chair;
        }

        public TicketDto ToDto()
        {
            return new TicketDto
            {
                Chair = Chair,
            };
        }
    }
}
