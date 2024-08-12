using API.Implement;
using API.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Dto;
using Model.Response;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Drawing;
using Microsoft.AspNetCore.Http.HttpResults;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly IFoodRepository _foodRepository;

        public FoodController(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }

        [HttpPost]
        [Route("GetAll")]
        public IActionResult GetAllFood(Pagination query)
        {
            var list = _foodRepository.GetAllFood(query);
            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Danh sách món ăn",
                Data = list.Select(x => x.ToDto()).ToList()
            });
        }

        [HttpGet]
        [Route("{ID}")]
        public async Task<IActionResult> GetFoodByID(Guid ID)
        {
            var result = await _foodRepository.GetFoodByID(ID);
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
        [Route("ChooseFood")]
        public async Task<IActionResult> GetFoodByNameAndSize([FromBody] ChooseFood chooseFoodRequest)
        {
            var result = await _foodRepository.GetFoodByNameAndSize(chooseFoodRequest.Name, chooseFoodRequest.Size);
            if (result == null)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = $"{chooseFoodRequest.Name} with {chooseFoodRequest.Size} is not found",
                });
            }
            return Ok(new ApiResponse
            {
                Success = true,
                Message = $"{chooseFoodRequest.Name} with {chooseFoodRequest.Size} is found",
                Data = result.ToDto()
            });
        }

        [HttpPost]
        [Authorize(Roles = StateRole.Admin)]
        public async Task<IActionResult> Create(FoodDto foodDto)
        {
            try
            {
                var result = await _foodRepository.Create(new Food(foodDto));
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Tao food thanh cong",
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
        public async Task<IActionResult> Update([FromRoute] Guid ID, [FromBody] FoodDto foodDto)
        {
            var foodFromDb = await _foodRepository.GetFoodByID(ID);

            if (foodFromDb == null)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = $"{ID} is not found",
                });
            }
            foodFromDb.Update(foodDto);
            await _foodRepository.Update(foodFromDb);
            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Cap nhat food thanh cong",
                Data = foodFromDb.ToDto()
            });
        }

        [HttpDelete]
        [Route("{ID}")]
        [Authorize(Roles = StateRole.Admin)]
        public Task<IActionResult> Delete([FromRoute] Guid ID, [FromBody] FoodDto foodDto)
        {
            // Hoi kho' :(
            throw new NotImplementedException();
        }
    }
}
