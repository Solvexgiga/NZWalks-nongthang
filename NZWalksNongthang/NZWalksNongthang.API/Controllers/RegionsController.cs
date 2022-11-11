using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NZWalksNongthang.API.Model.Domain;
using NZWalksNongthang.API.Model.DTO;
using NZWalksNongthang.API.Repositories;

namespace NZWalksNongthang.API.Controllers
{
    [ApiController]
    [Route("Regions")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            var regions = await regionRepository.GetAllAsync();
            // return DTO regions
            //var regionsDTO = new List<Model.DTO.Region>();
            //regions.ToList().ForEach(region =>
            //{
            //    var regionDTO = new Model.DTO.Region()
            //    {
            //        Id = region.Id,
            //        Code = region.Code,
            //        Name = region.Name,
            //        Area = region.Area,
            //        Lat = region.Lat,
            //        Long = region.Long,
            //        Population = region.Population,
            //    };
            //    regionsDTO.Add(regionDTO);
            //});

            var regionsDTO = mapper.Map<List<Model.DTO.Region>>(regions);

            return Ok(regionsDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await regionRepository.GetAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            var regionDTO = mapper.Map<Model.DTO.Region>(region);
            return Ok(regionDTO);
        }
        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(Model.DTO.AddRegionRequest addRegionRequest)
        { 
        //Request(DTO) to domain model
        var region = new Model.Domain.Region()
        {
            Code = addRegionRequest.Code,
            Area = addRegionRequest.Area,
            Lat = addRegionRequest.Lat,
            Long = addRegionRequest.Long,
            Name = addRegionRequest.Name,
            Population = addRegionRequest.Population,
        };
        //pass details to Repository
            region = await regionRepository.AddAsync(region);
            //Convert back to DTO
            var regionDTO = new Model.DTO.Region
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population,
            };
            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDTO.Id }, regionDTO);
        
    }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            //Get region from database
            var region = await regionRepository.DeleteAsync(id);

            //if null NotFound
            if (region == null)
            {
                return NotFound();
            }

            //Convert response back to DTO
            var regionDTO = new Model.DTO.Region
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population,
            };

            //return Ok response
            return Ok(regionDTO);

        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsyn([FromRoute] Guid id, [FromBody] Model.DTO.UpdateRegionRequest updateRegionRequest)
        {
            // Convert DTO to Domain Model
            var region = new Model.Domain.Region()
            {
                Code = updateRegionRequest.Code,
                Area = updateRegionRequest.Area,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Name = updateRegionRequest.Name,
                Population = updateRegionRequest.Population,
            };

            // Update Region using repository
            region = await regionRepository.UpdateAsync(id, region);

            // If Null then NotFound
            if (region == null)
            {
                return NotFound();
            }

            // Convert Domain back to DTO
            var regionDTO = new Model.DTO.Region
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population,
            };

            // Return Ok response
            return Ok(regionDTO);
        }
    }
}
