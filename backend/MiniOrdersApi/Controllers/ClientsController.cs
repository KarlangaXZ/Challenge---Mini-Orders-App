using Microsoft.AspNetCore.Mvc;
using MiniOrdersApi.Dtos;
using MiniOrdersApi.Models;
using MiniOrdersApi.Repositories;
using System;

namespace MiniOrdersApi.Controllers
{
    [ApiController]
    [Route("clients")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientRepository _repo;

        public ClientsController(IClientRepository repo)
        {
            _repo = repo;
        }

        // GET /clients
        [HttpGet]
        public ActionResult Get()
        {
            var clients = _repo.GetAll();
            return Ok(clients);
        }

        // GET /clients/{id}
        [HttpGet("{id:guid}")]
        public ActionResult GetById(Guid id)
        {
            var client = _repo.Get(id);
            if (client == null) return NotFound();
            return Ok(client);
        }

     
        [HttpPost]
        public ActionResult Create([FromBody] ClientCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var client = new Client
            {
                Id = Guid.NewGuid(),
                Nombre = dto.Nombre
            };

            _repo.Add(client);

            return CreatedAtAction(nameof(GetById), new { id = client.Id }, client);
        }

        [HttpPut("{id:guid}")]
        public ActionResult Update(Guid id, [FromBody] ClientCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existing = _repo.Get(id);
            if (existing == null) return NotFound();

            existing.Nombre = dto.Nombre;

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
