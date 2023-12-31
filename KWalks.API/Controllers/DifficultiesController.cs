using KWalks.API.Data;
using KWalks.API.Models.Domain;
using KWalks.API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DifficultiesController : ControllerBase
    {
        private readonly KWalksDbContext dbContext;

        public DifficultiesController(KWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        public IActionResult GetDifficulties()
        {
            // Get Data from Database - Domain Models
            var difficultiesDomain = dbContext.Difficulties.ToList();

            // Map Domain Models to DTOs (Converting the Domain model into a DTO)
            var difficultiesDto = new List<DifficultyDto>();
            foreach (var difficultyDomain in difficultiesDomain)
            {
                difficultiesDto.Add(new DifficultyDto()
                {
                    Id = difficultyDomain.Id,
                    Name = difficultyDomain.Name,
                });
            }

            // Return DTOs (region DTO is what is returned to the client)
            return Ok(difficultiesDto);
        }


        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetDifficulty(Guid id)
        {
            // Get region Domain model from Database
            var difficultyDomain = dbContext.Difficulties.Find(id);

            if (difficultyDomain == null)
            {
                NotFound();
            }

            // Map/Convert Region Domain Model to Region DTO
            var difficultyDto = new DifficultyDto()
            {
                Id = difficultyDomain.Id,
                Name = difficultyDomain.Name,
            };

            // Return DTO back to Client
            return Ok(difficultyDto);
        }


        [HttpPost]
        public IActionResult AddDifficulty(AddDifficultyRequestDto addDifficultyRequestDto)
        {
            // Convert DTO to Domain Model
            var difficultyDomainModel = new Difficulty
            {
                Name = addDifficultyRequestDto.Name,
            };

            // Use Domain Model to create Region Since only the Domain speaks to the Database
            dbContext.Add(difficultyDomainModel);
            dbContext.SaveChanges();

            // Map Domain Model back to DTO
            var difficultyDto = new DifficultyDto
            {
                Name = difficultyDomainModel.Name,
            };

            return CreatedAtAction(nameof(AddDifficulty), new { id = difficultyDto.Id }, difficultyDto);
        }
    }
}
