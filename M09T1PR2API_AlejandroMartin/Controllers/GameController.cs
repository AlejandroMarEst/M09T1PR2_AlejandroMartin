using M09T1PR2API_AlejandroMartin.Data;
using M09T1PR2API_AlejandroMartin.DTOs;
using M09T1PR2API_AlejandroMartin.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace M09T1PR2API_AlejandroMartin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly AppDbContext _context;
        public GameController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetAllGames()
        {
            var gameList = await _context.Games.ToListAsync();
            if (gameList.Count == 0)
            {
                return NotFound("There are no games");
            }
            return Ok(gameList);
        }
        [HttpPost]
        public async Task<ActionResult<Game>> CreateGame(GameDTO gameDTO)
        {
            Game game = new Game
            {
                Title = gameDTO.Title,
                Description = gameDTO.Description,
                Dev = gameDTO.Dev,
                Img = gameDTO.Img
            };
            _context.Games.Add(game);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAllGames), game);
        }
        [HttpPut]
        public async Task<ActionResult<Game>> UpdateGame(Game game)
        {
            Game gameToUpdate = await _context.Games.FirstOrDefaultAsync(x => x.Id == game.Id);
            gameToUpdate.Title = game.Title;
            gameToUpdate.Description = game.Description;
            gameToUpdate.Dev = game.Dev;
            gameToUpdate.Img = game.Img;
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAllGames), game);
        }
        [HttpDelete]
        public async Task<ActionResult<Game>> RemoveGame(Game game)
        {
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAllGames), game);
        }
    }
}
