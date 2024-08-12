using API.Implement;
using API.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Request;
using Model.Response;
using API.Entities;
using Model.Dto;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IRoomFilmRepository _roomFilmRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IFilmRepository _filmRepository;

        public TicketController(ITicketRepository ticketRepository,
                                IRoomFilmRepository roomFilmRepository,
                                IRoomRepository roomRepository,
                                IFilmRepository filmRepository)
        {
            _ticketRepository = ticketRepository;
            _roomFilmRepository = roomFilmRepository;
            _roomRepository = roomRepository;
            _filmRepository = filmRepository;
        }

        [HttpPut]
        [Route("CreateTicketList/{ID}")]
        public async Task<IActionResult> CreateListTicket(Guid ID)
        {
            var roomFilmFromDb = await _roomFilmRepository.GetByID(ID);
            if (roomFilmFromDb == null)
            {
                return NotFound();
            }
            var roomFromDb = await _roomRepository.GetRoomByID(roomFilmFromDb.RoomID);

            var filmFromDb = await _filmRepository.GetFilmByID(roomFilmFromDb.FilmID);
            
            if (roomFromDb == null || filmFromDb == null)
            {
                return NotFound();
            }

            roomFilmFromDb.Film = filmFromDb;

            roomFilmFromDb.Tickets = new List<Ticket>();
            for (var i = 0; i < roomFromDb.TotalSlot; i++)
            {
                var ticket = new Ticket(new TicketDto
                {
                    Chair = (i + 1).ToString(),
                    RoomFilmID = roomFilmFromDb.ID,
                });
                roomFilmFromDb.Tickets.Add(ticket);
                await _ticketRepository.Create(ticket);
            }

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Tạo thành công",
                Data = roomFilmFromDb.Tickets.Select(x => new TicketResponse
                {
                    RoomFilmResponse = roomFilmFromDb.ToResponse(),
                    TicketDto = x.ToDto()
                })
            });
        }
    }
}
