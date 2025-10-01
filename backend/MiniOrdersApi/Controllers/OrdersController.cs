using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OrdersApi.Dtos;
using OrdersApi.Models;
using OrdersApi.Repositories;



namespace OrdersApi.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _repo;

        public OrdersController(IOrderRepository repo)
        {
            _repo = repo;
        }


        [HttpGet]
        public ActionResult Get([FromQuery] string? search, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var items = _repo.GetAll();

            if (!string.IsNullOrWhiteSpace(search))
            {
                items = items.Where(o => o.Cliente != null && o.Cliente.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            var total = items.Count();
            var paged = items.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return Ok(new { total, page, pageSize, items = paged });
        }

        
        [HttpGet("{id:guid}")]
        public ActionResult GetById(Guid id)
        {
            var order = _repo.Get(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

     
        [HttpPost]
        public ActionResult Create([FromBody] OrderCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var order = new Order
            {
                Id = Guid.NewGuid(),
                Cliente = dto.Cliente,
                Fecha = dto.Fecha ?? DateTime.UtcNow,
                Total = dto.Total
            };

            _repo.Add(order);

            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }

        
        [HttpPut("{id:guid}")]
        public ActionResult Update(Guid id, [FromBody] OrderUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existing = _repo.Get(id);
            if (existing == null) return NotFound();

            existing.Cliente = dto.Cliente;
            existing.Total = dto.Total;
            existing.Fecha = dto.Fecha ?? existing.Fecha;

            var updated = _repo.Update(existing);
            if (!updated) return NotFound();

            return NoContent();
        }

       
        [HttpDelete("{id:guid}")]
        public ActionResult Delete(Guid id)
        {
            var deleted = _repo.Delete(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
