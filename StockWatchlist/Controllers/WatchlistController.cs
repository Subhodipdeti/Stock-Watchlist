using Microsoft.AspNetCore.Mvc;
using StockWatchlist.Models;
using StockWatchlist.Services;

namespace StockWatchlist.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WatchlistController : ControllerBase
    {
        private readonly MongoDBService mongoDBService;

        public WatchlistController(MongoDBService mongoDBService)
        {
            this.mongoDBService = mongoDBService;
        }

        [HttpGet]
        public async Task<List<Watchlist>> GetWatchList() {
            return await mongoDBService.GetAsync();
        }

        [HttpPost]
        public async Task<IActionResult> CreateWatchlist([FromBody] AddWatchlist watchlist) {
           await mongoDBService.CreateAsync(watchlist);
           return CreatedAtAction(nameof(CreateWatchlist), watchlist);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AddToWatchlist(string id, [FromBody] string name) {
            await mongoDBService.AddToWatchlistAsync(id, name);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWatchlist(string id) {
            await mongoDBService.DeleteAsync(id);
            return NoContent();
        }
    }
}