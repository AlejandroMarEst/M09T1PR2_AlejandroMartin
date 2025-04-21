

using M09T1PR2API_AlejandroMartin.Data;
using M09T1PR2API_AlejandroMartin.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace M09T1PR2API_AlejandroMartin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotingController : ControllerBase
    {
        private readonly AppDbContext _context;
        public VotingController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpPost("Vote")]
        public async Task<ActionResult> PostVote(int gameId)
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var Vote = new GameVoting
            {
                GameId = gameId,
                UserId = user
            };
            _context.GameVotes.Add(Vote);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(PostVote), Vote);
        }

        [Authorize]
        [HttpPost("Unvote")]
        public async Task<ActionResult> PostUnvote(int gameId)
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var Vote = await _context.GameVotes.FirstOrDefaultAsync(x => x.GameId == gameId && x.UserId == user);
            if (Vote == null)
            {
                return NotFound("Vote not found");
            }
            _context.GameVotes.Remove(Vote);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [Authorize]
        [HttpGet("GetVotes")]
        public async Task<ActionResult<IEnumerable<GameVoting>>> GetVotes()
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var Votes = await _context.GameVotes.Where(x => x.UserId == user).ToListAsync();
            if (Votes.Count == 0)
            {
                return NotFound("No votes found");
            }
            return Ok(Votes);
        }

        [HttpGet("GetVotes/{gameId}")]
        public async Task<ActionResult<IEnumerable<GameVoting>>> GetVotesByGameId(int gameId)
        {
            var Votes = await _context.GameVotes.Where(x => x.GameId == gameId).ToListAsync();
            return Ok(Votes.Count);
        }
    }
}
