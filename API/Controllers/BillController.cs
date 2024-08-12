using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Request;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        [HttpPost]
        public Task<IActionResult> Create([FromBody] BillRequest billRequest)
        {

        }
    }
}
