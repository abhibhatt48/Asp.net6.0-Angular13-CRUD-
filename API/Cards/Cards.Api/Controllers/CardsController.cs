using Cards.Api.Data;
using Cards.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cards.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class CardsController : Controller
    {

        private readonly CardsDbContext _cardsDbContext;
        public CardsController(CardsDbContext cardsDbContext)
        {
            _cardsDbContext = cardsDbContext;
        }
        //Get all cards...
        [HttpGet]
        public async Task<IActionResult> GetAllCards()
        {
            var cards = await _cardsDbContext.Cards.ToListAsync();
            return Ok(cards);
        }

        //Get single cards...
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetCard")]
        public async Task<IActionResult> GetCard([FromRoute] Guid id)
        {
            var card = await _cardsDbContext.Cards.FirstOrDefaultAsync(x => x.ID == id);
            if (card != null)
            {
                return Ok(card);
            }
            return NotFound("card is not available...");
            
        }

        //Get single cards...
        [HttpPost]
        public async Task<IActionResult> AddCard([FromBody] Card card)
        {
            card.ID = Guid.NewGuid();
            await _cardsDbContext.Cards.AddAsync(card);
            await _cardsDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCard), card.ID, card);
        }

        //updating card
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateCard([FromRoute] Guid id , [FromBody] Card card)
        {
            var extcard = await _cardsDbContext.Cards.FirstOrDefaultAsync(x => x.ID == id);
            if (extcard != null)
            {
                extcard.CardholderName = card.CardholderName;
                extcard.CardNumber = card.CardNumber;
                extcard.ExpiryMonth = card.ExpiryMonth;
                extcard.ExpiryYear = card.ExpiryYear;
                extcard.CVC = card.CVC;
                await _cardsDbContext.SaveChangesAsync();
                return Ok(extcard);
            }
            return NotFound("card not found for update...");
        }

        //removing card
        [HttpDelete]
        [Route("{id:guid}")]
        
        public async Task<IActionResult> DeleteCard([FromRoute] Guid id)
        {
            var extcard = await _cardsDbContext.Cards.FirstOrDefaultAsync(x => x.ID == id);
            if (extcard != null)
            {
                _cardsDbContext.Remove(extcard);
                await _cardsDbContext.SaveChangesAsync();
                return Ok(extcard);
            }
            return NotFound("card not found for update...");

        }

    }
}
