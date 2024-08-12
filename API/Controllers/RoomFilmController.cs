using API.Entities;
using API.Implement;
using API.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Dto;
using Model.Request;
using Model.Response;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomFilmController : ControllerBase
    {
        private readonly IRoomFilmRepository _roomFilmRepository;
        private readonly IFilmRepository _filmRepository;

        public RoomFilmController(IRoomFilmRepository roomFilmRepository,
                                  IFilmRepository filmRepository) 
        {
            _roomFilmRepository = roomFilmRepository;
            _filmRepository = filmRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoomFilmRequest roomFilmRequest)
        {
            var film = await _filmRepository.GetFilmByName(roomFilmRequest.FilmName);

            if (film == null) 
            {
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = $"Tên phim {roomFilmRequest.FilmName} không tìm thấy"
                });
            }

            var roomFilmDto = new RoomFilmDto
            {
                DayPremiered = roomFilmRequest.DayPremiered,
                TimePremiered = roomFilmRequest.TimePremiered,
                Price = roomFilmRequest.Price,
                RoomID = roomFilmRequest.RoomID,
                FilmID = film.ID,
            };

            var result = await _roomFilmRepository.Create(new RoomFilm(roomFilmDto));
            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Thành công",
                Data = result.ToResponse()
            });
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] RoomFilmRequest roomFilmRequest)
        {
            var film = await _filmRepository.GetFilmByName(roomFilmRequest.FilmName);

            if (film == null)
            {
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = $"Tên phim {roomFilmRequest.FilmName} không tìm thấy"
                });
            }

            var roomFilmFromDb = await _roomFilmRepository.GetByRoomAndFilm(roomFilmRequest.RoomID, film.ID);

            if (roomFilmFromDb == null)
            {
                return NotFound();
            }

            var roomFilmDto = new RoomFilmDto
            {
                DayPremiered = roomFilmRequest.DayPremiered,
                TimePremiered = roomFilmRequest.TimePremiered,
                Price = roomFilmRequest.Price,
                RoomID = roomFilmRequest.RoomID,
                FilmID = film.ID,
            };
            roomFilmFromDb.Update(roomFilmDto);
            var result = await _roomFilmRepository.Update(roomFilmFromDb);
            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Thành công",
                Data = result.ToResponse()
            });
        }
    }
}
