using Football_Manager.Models;
using Football_Manager.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Football_Manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayersService _playerService;

        public PlayersController(IPlayersService playerService)
        {
            _playerService = playerService;
        }

        // GET: api/Players
        [HttpGet]
        public async Task<IEnumerable<Player>> Get()
        {
            return await _playerService.GetPlayersList();
        }

        // GET: api/Players/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> Get(int id)
        {
            var player = await _playerService.GetPlayerById(id);

            if (player == null)
            {
                return NotFound();
            }

            return Ok(player);
        }

        // POST: api/Players
        [HttpPost]
        public async Task<ActionResult<Player>> Post(Player player)
        {
            await _playerService.CreatePlayer(player);

            return CreatedAtAction("Post", new { id = player.PlayerId }, player);
        }

        // PUT: api/Players/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Player player)
        {
            if (id != player.PlayerId)
            {
                return BadRequest("Not a valid player id");
            }

            await _playerService.UpdatePlayer(player);

            return NoContent();
        }

        // DELETE: api/Players/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid player id");

            var player = await _playerService.GetPlayerById(id);
            if (player == null)
            {
                return NotFound();
            }

            await _playerService.DeletePlayer(player);

            return NoContent();
        }
    }
}
