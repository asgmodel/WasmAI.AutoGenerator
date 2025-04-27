using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using ApiCore.Services.Services;
using Microsoft.AspNetCore.Mvc;
using ApiCore.DyModels.VMs;
using System.Linq.Expressions;
using ApiCore.DyModels.Dso.Requests;
using AutoGenerator.Helper.Translation;
using System;

namespace ApiCore.Controllers.Api
{
    //[ApiExplorerSettings(GroupName = "ApiCore")]
    [Route("api/ApiCore/Api/[controller]")]
    [ApiController]
    public class AdvertisementController : ControllerBase
    {
        private readonly IUseAdvertisementService _advertisementService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public AdvertisementController(IUseAdvertisementService advertisementService, IMapper mapper, ILoggerFactory logger)
        {
            _advertisementService = advertisementService;
            _mapper = mapper;
            _logger = logger.CreateLogger(typeof(AdvertisementController).FullName);
        }

        // Get all Advertisements.
        [HttpGet(Name = "GetAdvertisements")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<AdvertisementOutputVM>>> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all Advertisements...");
                var result = await _advertisementService.GetAllAsync();
                var items = _mapper.Map<List<AdvertisementOutputVM>>(result);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all Advertisements");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get a Advertisement by ID.
        [HttpGet("{id}", Name = "GetAdvertisement")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AdvertisementInfoVM>> GetById(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid Advertisement ID received.");
                return BadRequest("Invalid Advertisement ID.");
            }

            try
            {
                _logger.LogInformation("Fetching Advertisement with ID: {id}", id);
                var entity = await _advertisementService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("Advertisement not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<AdvertisementInfoVM>(entity);
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching Advertisement with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a Advertisement by Lg.
        [HttpGet("GetAdvertisementByLanguage", Name = "GetAdvertisementByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AdvertisementOutputVM>> GetAdvertisementByLg(AdvertisementFilterVM model)
        {
            var id = model.Id;
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid Advertisement ID received.");
                return BadRequest("Invalid Advertisement ID.");
            }

            try
            {
                _logger.LogInformation("Fetching Advertisement with ID: {id}", id);
                var entity = await _advertisementService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("Advertisement not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<AdvertisementOutputVM>(entity, opt => opt.Items.Add(HelperTranslation.KEYLG, model.Lg));
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching Advertisement with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a Advertisements by Lg.
        [HttpGet("GetAdvertisementsByLanguage", Name = "GetAdvertisementsByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<AdvertisementOutputVM>>> GetAdvertisementsByLg(string? lg)
        {
            if (string.IsNullOrWhiteSpace(lg))
            {
                _logger.LogWarning("Invalid Advertisement lg received.");
                return BadRequest("Invalid Advertisement lg null ");
            }

            try
            {
                var advertisements = await _advertisementService.GetAllAsync();
                if (advertisements == null)
                {
                    _logger.LogWarning("Advertisements not found  by  ");
                    return NotFound();
                }

                var items = _mapper.Map<IEnumerable<AdvertisementOutputVM>>(advertisements, opt => opt.Items.Add(HelperTranslation.KEYLG, lg));
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching Advertisements with Lg: {lg}", lg);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create a new Advertisement.
        [HttpPost(Name = "CreateAdvertisement")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AdvertisementOutputVM>> Create([FromBody] AdvertisementCreateVM model)
        {
            if (model == null)
            {
                _logger.LogWarning("Advertisement data is null in Create.");
                return BadRequest("Advertisement data is required.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state in Create: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Creating new Advertisement with data: {@model}", model);
                var item = _mapper.Map<AdvertisementRequestDso>(model);
                var createdEntity = await _advertisementService.CreateAsync(item);
                var createdItem = _mapper.Map<AdvertisementOutputVM>(createdEntity);
                return Ok(createdItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating a new Advertisement");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create multiple Advertisements.
        [HttpPost("createRange")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<AdvertisementOutputVM>>> CreateRange([FromBody] IEnumerable<AdvertisementCreateVM> models)
        {
            if (models == null)
            {
                _logger.LogWarning("Data is null in CreateRange.");
                return BadRequest("Data is required.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state in CreateRange: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Creating multiple Advertisements.");
                var items = _mapper.Map<List<AdvertisementRequestDso>>(models);
                var createdEntities = await _advertisementService.CreateRangeAsync(items);
                var createdItems = _mapper.Map<List<AdvertisementOutputVM>>(createdEntities);
                return Ok(createdItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating multiple Advertisements");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Update an existing Advertisement.
        [HttpPut(Name = "UpdateAdvertisement")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AdvertisementOutputVM>> Update([FromBody] AdvertisementUpdateVM model)
        {
            if (model == null)
            {
                _logger.LogWarning("Invalid data in Update.");
                return BadRequest("Invalid data.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state in Update: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Updating Advertisement with ID: {id}", model?.Id);
                var item = _mapper.Map<AdvertisementRequestDso>(model);
                var updatedEntity = await _advertisementService.UpdateAsync(item);
                if (updatedEntity == null)
                {
                    _logger.LogWarning("Advertisement not found for update with ID: {id}", model?.Id);
                    return NotFound();
                }

                var updatedItem = _mapper.Map<AdvertisementOutputVM>(updatedEntity);
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating Advertisement with ID: {id}", model?.Id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Delete a Advertisement.
        [HttpDelete("{id}", Name = "DeleteAdvertisement")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid Advertisement ID received in Delete.");
                return BadRequest("Invalid Advertisement ID.");
            }

            try
            {
                _logger.LogInformation("Deleting Advertisement with ID: {id}", id);
                await _advertisementService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting Advertisement with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get count of Advertisements.
        [HttpGet("CountAdvertisement")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Count()
        {
            try
            {
                _logger.LogInformation("Counting Advertisements...");
                var count = await _advertisementService.CountAsync();
                return Ok(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while counting Advertisements");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}