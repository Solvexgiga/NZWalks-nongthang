using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalksNongthang.API.Model.DTO;
using NZWalksNongthang.API.Repositories;

namespace NZWalksNongthang.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultiesController : Controller
    {
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper mapper;

        public WalkDifficultiesController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalkDifficulties() 
        {
            var walkDifficultiesDomain = await walkDifficultyRepository.GetAllAsync();

            // Convert Domain to DTOs
            var walkDifficultiesDTO = mapper.Map<List<Model.DTO.WalkDifficulty>>(walkDifficultiesDomain);
            return Ok(walkDifficultiesDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDifficultyById")]
        public async Task<IActionResult> GetWalkDifficultyById(Guid id)
        {
            var walkDifficulty = await walkDifficultyRepository.GetAsync(id);
            if (walkDifficulty == null)
            {
                return NotFound();
            }

            // Convert Domain to DTOs
            var walkDifficultyDTO = mapper.Map<Model.DTO.WalkDifficulty>(walkDifficulty);

            return Ok(walkDifficultyDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkDifficultyAsync
            (Model.DTO.AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            // Validate incoming request
            if (!ValidateAddWalkDifficultyAsync(addWalkDifficultyRequest))
            {
                return BadRequest(ModelState);
            }

            // Convert DTO to Domain Model
            var walkDifficultyDomain = new Model.Domain.WalkDifficulty
            {
                Code = addWalkDifficultyRequest.Code
            };

            // Call repository
            walkDifficultyDomain = await walkDifficultyRepository.AddAsync(walkDifficultyDomain);

            // Convert Domain to DTo
            var walkDifficultyDTO = mapper.Map<Model.DTO.WalkDifficulty>(walkDifficultyDomain);

            // Return reponse
            return CreatedAtAction(nameof(GetWalkDifficultyById),
                new { id = walkDifficultyDTO.Id }, walkDifficultyDTO);
            
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkDifficultyAsync
            (Guid id,Model.DTO.UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            // Validate incoming request
            if (!ValidateUpdateWalkDifficultyAsync(updateWalkDifficultyRequest))
            {
                return BadRequest(ModelState);
            }

            // Convert DTO toDomain Model
            var walkDifficultyDomain = new Model.Domain.WalkDifficulty
            {
                Code = updateWalkDifficultyRequest.Code
            };

            // Calll repository to update
            walkDifficultyDomain = await walkDifficultyRepository.UpdateAsync(id, walkDifficultyDomain);
            if(walkDifficultyDomain == null)
            {
                return NotFound();
            }

            // Convert Domain to DTO
            var walkDifficultyDTO = mapper.Map<Model.DTO.WalkDifficulty>(walkDifficultyDomain);

            // Return reponse
            return Ok(walkDifficultyDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkDifficulty(Guid id)
        {
            var walkDifficultyDomain = await walkDifficultyRepository.DeleteAsync(id);
            if(walkDifficultyDomain == null)
            {
                return NotFound();
            }

            // Convert to DTO
            var walkDifficultyDTO = mapper.Map<Model.DTO.WalkDifficulty>(walkDifficultyDomain);
            return Ok(walkDifficultyDTO);
        }
        #region Private methods
        private bool ValidateAddWalkDifficultyAsync(Model.DTO.AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            if (addWalkDifficultyRequest == null)
            {
                ModelState.AddModelError(nameof(addWalkDifficultyRequest),
                   $"{nameof(addWalkDifficultyRequest)}is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(addWalkDifficultyRequest.Code))
            {
                ModelState.AddModelError(nameof(addWalkDifficultyRequest.Code),
                   $"{nameof(addWalkDifficultyRequest.Code)}is required.");
            }
            if(ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }

        private bool ValidateUpdateWalkDifficultyAsync(Model.DTO.UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            if (updateWalkDifficultyRequest == null)
            {
                ModelState.AddModelError(nameof(updateWalkDifficultyRequest),
                   $"{nameof(updateWalkDifficultyRequest)}is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(updateWalkDifficultyRequest.Code))
            {
                ModelState.AddModelError(nameof(updateWalkDifficultyRequest.Code),
                   $"{nameof(updateWalkDifficultyRequest.Code)}is required.");
            }
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }
        #endregion

    }
}
