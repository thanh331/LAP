using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.Application.Services;

namespace Todo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _service;

        public TodoController(ITodoService service)
        {
            _service = service;
        }

        // Lấy toàn bộ danh sách
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var results = await _service.GetAll();
            return Ok(results);
        }

        // Lấy chi tiết một công việc theo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // Thêm mới công việc
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Infrastructure.Todo entity)
        {
            if (entity == null) return BadRequest();
            await _service.Add(entity);
            return Ok(entity);
        }

        // Cập nhật công việc
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Infrastructure.Todo entity)
        {
            if (id != entity.Id) return BadRequest("ID không khớp");

            await _service.Update(entity);
            return Ok();
        }

        // Xóa công việc
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok();
        }
    }
}