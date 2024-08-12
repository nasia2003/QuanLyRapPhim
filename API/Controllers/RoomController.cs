using API.Entities;
using API.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Dto;
using Model.Response;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;

        public RoomController(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        [HttpPost]
        [Route("GetAll")]
        public IActionResult GetAllRoom(Pagination query)
        {
            var list = _roomRepository.GetAllRoom(query);
            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Danh sách phòng chiếu phim",
                Data = list.Select(x => x.ToDto()).ToList()
            });
        }

        [HttpGet]
        [Route("{ID}")]
        public async Task<IActionResult> GetRoomByID(int ID)
        {
            var result = await _roomRepository.GetRoomByID(ID);
            if (result == null)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = $"{ID} is not found",
                });
            }
            return Ok(new ApiResponse
            {
                Success = true,
                Message = "{ID} is found",
                Data = result.ToDto()
            });
        }

        [HttpPost]
        [Authorize(Roles = StateRole.Admin)]
        public async Task<IActionResult> Create(RoomDto RoomDto)
        {
            try
            {
                var result = await _roomRepository.Create(new Room(RoomDto));
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Tạo phòng chiếu thành công",
                    Data = result.ToDto()
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = ex.Message,
                });
            }
        }

        [HttpPost]
        [Route("Update")]
        [Authorize(Roles = StateRole.Admin)]
        public async Task<IActionResult> Update([FromBody] RoomDto RoomDto)
        {
            var roomFromDb = await _roomRepository.GetRoomByID(RoomDto.ID);

            if (roomFromDb == null)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = $"{RoomDto.ID} is not found",
                });
            }
            roomFromDb.Update(RoomDto);
            await _roomRepository.Update(roomFromDb);
            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Cập nhật phòng chiếu thành công",
                Data = roomFromDb.ToDto()
            });
        }

        [HttpDelete]
        [Route("{ID}")]
        [Authorize(Roles = StateRole.Admin)]
        public Task<IActionResult> Delete([FromRoute] int ID)
        {
            // Hoi kho' :(
            throw new NotImplementedException();
        }
    }
}
