using API.Entities;
using API.Implement;
using API.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Model.Dto;
using Model.Response;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComboController : ControllerBase
    {
        private readonly IComboRepository _comboRepository;
        private readonly IFoodRepository _foodRepository;
        private readonly IFoodComboRepository _foodComboRepository;

        public ComboController(IComboRepository comboRepository,
                               IFoodRepository foodRepository,
                               IFoodComboRepository foodComboRepository) 
        {
            _comboRepository = comboRepository;
            _foodRepository = foodRepository;
            _foodComboRepository = foodComboRepository;
        }

        [HttpPost]
        [Route("GetAll")]
        public IActionResult GetAllCombo(Pagination query)
        {
            var list = _comboRepository.GetAllCombo(query);
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
            var result = await _comboRepository.GetComboByID(ID);
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
                Message = $"{ID} is found",
                Data = result.ToDto()
            });
        }

        [HttpPost]
        [Route("ChooseCombo")]
        public async Task<IActionResult> GetComboByName([FromBody] String Name)
        {
            var result = await _comboRepository.GetComboByName(Name);
            if (result == null)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = $"{Name} is not found",
                });
            }
            return Ok(new ApiResponse
            {
                Success = true,
                Message = $"{Name} is found",
                Data = result.ToDto()
            });
        }

        [HttpPost]
        [Authorize(Roles = StateRole.Admin)]
        public async Task<IActionResult> Create(ComboDto comboDto)
        {
            try
            {
                var combo = new Combo(comboDto);
                var result = await _comboRepository.Create(combo);

                var list = await ConvertFoodComboList(result, comboDto.Foods);

                if(list == null)
                {
                    return BadRequest(new ApiResponse
                    {
                        Success = false,
                        Message = "Something's wrong :(",
                    });
                }
                result.FoodCombos = new List<FoodCombo>();
                foreach (var item in list)
                {
                    result.FoodCombos.Add(item);
                    await _foodComboRepository.Create(item);
                }

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
        public async Task<IActionResult> Update([FromRoute] Guid ID, [FromBody] ComboDto comboDto)
        {
            var comboFromDb = await _comboRepository.GetComboByID(ID);

            if (comboFromDb == null)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = $"{ID} is not found",
                });
            }
            comboFromDb.Update(comboDto);
            await _comboRepository.Update(comboFromDb);

            var list = await ConvertFoodComboList(comboFromDb, comboDto.Foods);

            if (list == null)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Something's wrong :(",
                });
            }

            foreach(var item in comboFromDb.FoodCombos)
            {
                if(!list.Contains(item))
                {
                    comboFromDb.FoodCombos.Remove(item);
                    await _foodComboRepository.Delete(item);
                }    
            }

            foreach (var item in list)
            {
                if (!comboFromDb.FoodCombos.Contains(item))
                {
                    comboFromDb.FoodCombos.Add(item);
                    await _foodComboRepository.Create(item);
                }
            }

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Cap nhat food thanh cong",
                Data = comboFromDb.ToDto()
            });
        }

        [HttpDelete]
        [Route("{ID}")]
        [Authorize(Roles = StateRole.Admin)]
        public Task<IActionResult> Delete([FromRoute] Guid ID)
        {
            //Hoi kho' :(
            throw new NotImplementedException();
        }


        private async Task<ICollection<FoodCombo>> ConvertFoodComboList(Combo combo, ICollection<ChooseFood> Foods)
        {
            var Now = new List<FoodCombo>();
            foreach (var chooseFood in Foods)
            {
                var food = await _foodRepository.GetFoodByNameAndSize(chooseFood.Name, chooseFood.Size);
                if (food == null)
                {
                    return new List<FoodCombo>();
                }
                Now.Add(new FoodCombo()
                {
                    FoodID = food.ID,
                    Food = food,
                    ComboID = combo.ID,
                    Combo = combo,
                });
            }
            return Now;
        }
    }
}
