using KWalks.API.Data;
using KWalks.API.Models.Domain;
using KWalks.API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly KWalksDbContext dbContext;

        public WalksController(KWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpPost]
        public IActionResult AddWalk(AddWalkRequestDto addWalkRequestDto)
        {
            var walkDomain = new Walk
            {
                Name = addWalkRequestDto.Name,
                Description = addWalkRequestDto.Description,
                LengthInKm = addWalkRequestDto.LengthInKm,
                WalkImgUrl = addWalkRequestDto.WalkImgUrl,
                DifficultyId = addWalkRequestDto.DifficultyId,
                RegionId = addWalkRequestDto.RegionId,
                //Difficulty = addWalkRequestDto.Difficulty,
                //Region = addWalkRequestDto.Region,
            };

            // Use Domain Model to create Region Since only the Domain speaks to the Database
            dbContext.Add(walkDomain);
            dbContext.SaveChanges();

            // Map Domain Model back to DTO
            var walkDto = new WalkDto
            {
                Id = walkDomain.Id,
                Name = walkDomain.Name,
                Description = walkDomain.Description,
                LengthInKm = walkDomain.LengthInKm,
                WalkImgUrl = walkDomain.WalkImgUrl,
                DifficultyId = walkDomain.DifficultyId,
                RegionId = walkDomain.RegionId,
                //Difficulty = walkDomain.Difficulty,
                //Region = walkDomain.Region,
            };

            return CreatedAtAction(nameof(AddWalk), new { id = walkDto.Id }, walkDto);
        }
    }
}
