using KWalks.API.Data;
using KWalks.API.Models.Domain;
using KWalks.API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly KWalksDbContext dbContext;

        public RegionsController(KWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // CLIENT -> DTO -> API -> DOMAIN MODEL(Only one talking to the dbcontext) -> DATABASE (DTO is what is sent back to the client)
        [HttpGet]
        public IActionResult GetRegions()
        {
            // Get Data from Database - Domain Models
            var regionsDomain = dbContext.Regions.ToList();

            // Map Domain Models to DTOs (Converting the Domain model into a DTO)
            var regionsDto = new List<RegionDto>();
            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionUrl = regionDomain.RegionUrl
                });
            }

            // Return DTOs (region DTO is what is returned to the client)
            return Ok(regionsDto);
        }


        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetRegion(Guid id)
        {
            // Get region Domain model from Database
            var regionDomain = dbContext.Regions.Find(id);

            if(regionDomain == null)
            {
                return NotFound();
            }

            // Map/Convert Region Domain Model to Region DTO
            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionUrl = regionDomain.RegionUrl,
            };

            // Return DTO back to Client
            return Ok(regionDto);
        }


        [HttpPost]
        public IActionResult AddRegion(AddRegionRequestDto addRegionRequestDto)
        {
            // Convert DTO to Domain Model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionUrl = addRegionRequestDto.RegionUrl,
            };

            // Use Domain Model to create Region Since only the Domain speaks to the Database
            dbContext.Add(regionDomainModel);
            dbContext.SaveChanges();

            // Map Domain Model back to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionUrl = regionDomainModel.RegionUrl,
            };

            return CreatedAtAction(nameof(AddRegion), new { id = regionDto.Id }, regionDto);
        }


        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult EditRegion( [FromRoute] Guid id, EditRegionRequestDto editRegionRequestDto)
        {
            // Get region Domain model from Database
            var regionDomain = dbContext.Regions.Find(id);

            if (regionDomain != null)
            {
                regionDomain.Code = editRegionRequestDto.Code;
                regionDomain.Name = editRegionRequestDto.Name;
                regionDomain.RegionUrl = editRegionRequestDto.RegionUrl;

                dbContext.SaveChanges();

                // Map Domain back to DTO
                var regionDto = new RegionDto
                {
                    Code = editRegionRequestDto.Code,
                    Name = editRegionRequestDto.Name,
                    RegionUrl = editRegionRequestDto.RegionUrl,
                };
                return Ok(regionDto);
            }
            return NotFound();
        }


        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult DeleteRegion([FromRoute] Guid id)
        {
            // Get region Domain model from Database
            var regionDomain = dbContext.Regions.Find(id);

            if ( regionDomain != null )
            {
                dbContext.Remove(regionDomain);
                dbContext.SaveChanges();
                return Ok(regionDomain);
            }
            return NotFound();
        }
    }
}
