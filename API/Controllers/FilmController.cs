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
    public class FilmController : ControllerBase
    {
        private readonly IFilmRepository _filmRepository;

        public FilmController(IFilmRepository filmRepository)
        {
            _filmRepository = filmRepository;
        }

        [HttpPost]
        [Route("GetAll")]
        public IActionResult GetAllFilm(Pagination query)
        {
            var list = _filmRepository.GetAllFilm(query);
            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Danh sách phim",
                Data = list.Select(x => x.ToDto()).ToList()
            });
        }

        [HttpGet]
        [Route("{ID}")]
        public async Task<IActionResult> GetFilmByID(Guid ID)
        {
            var result = await _filmRepository.GetFilmByID(ID);
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
        [Route("ChooseFilm")]
        public async Task<IActionResult> GetFilmByName([FromBody] String name)
        {
            var result = await _filmRepository.GetFilmByName(name);
            if (result == null)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = $"{name}  is not found",
                });
            }
            return Ok(new ApiResponse
            {
                Success = true,
                Message = $"{name} is found",
                Data = result.ToDto()
            });
        }

        [HttpPost]
        [Authorize(Roles = StateRole.Admin)]
        public async Task<IActionResult> Create(FilmDto filmDto)
        {
            try
            {
                var result = await _filmRepository.Create(new Film(filmDto));
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Tao phim thanh cong",
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

        [HttpPut]
        [Route("{ID}")]
        [Authorize(Roles = StateRole.Admin)]
        public async Task<IActionResult> Update([FromRoute] Guid ID, [FromBody] FilmDto filmDto)
        {
            var filmFromDb = await _filmRepository.GetFilmByID(ID);

            if (filmFromDb == null)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = $"{ID} is not found",
                });
            }
            filmFromDb.Update(filmDto);
            await _filmRepository.Update(filmFromDb);
            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Cap nhat phim thanh cong",
                Data = filmFromDb.ToDto()
            });
        }

        [HttpDelete]
        [Route("{ID}")]
        [Authorize(Roles = StateRole.Admin)]
        public Task<IActionResult> Delete([FromRoute] Guid ID, [FromBody] String name)
        {
            // Hoi kho' :(
            throw new NotImplementedException();
        }
    }
}
