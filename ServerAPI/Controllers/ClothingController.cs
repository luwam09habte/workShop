using Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Template;
using ServerAPI.Service;

[ApiController]
[Route("api/[controller]")]

public class ClothingController : ControllerBase
{
    private readonly ClothingService _clothingService;

    public ClothingController(ClothingService clothingService)
    {
        _clothingService = clothingService;

    }

    // til oprettelse af tøjartiklen
    [HttpPost("opret")]
    public async Task<IActionResult> CreateClothing(Clothing item, [FromQuery] string ID)
    {
        if (string.IsNullOrEmpty(item.Type) ||
             string.IsNullOrEmpty(item.Size) ||
             string.IsNullOrEmpty(item.Color))
        {
            return BadRequest("Alle felter skal være udfyldt");
        }

        // Generér nyt Clothe_id på serveren
        item.Clothe_id = Guid.NewGuid().ToString();
        
        if (string.IsNullOrEmpty(item.Status))
            item.Status = "available";
        
        item.Owner_id = ID;

        await _clothingService.CreateClothing(item);
        return Ok(item);
    }
    
    // Henter alle tilgængelige tøjartikler
    [HttpGet("available")]
    public async Task<IActionResult> GetAvailable()
    {
        var list = await _clothingService.GetAvailableClothes();
        return Ok(list);
    }
    
    // Til at låne en tøjartikel
    [HttpPost("loan")]
    public async Task<IActionResult> LoanClothing([FromQuery] string clothingId, [FromQuery] string userId)
    {
        var success = await _clothingService.LoanClothing(clothingId, userId);
        if (!success) return BadRequest("Tøj artiklen er ikke tilgængelig");

        return Ok("Tøj artikel lånt med succes");
    }
    
}