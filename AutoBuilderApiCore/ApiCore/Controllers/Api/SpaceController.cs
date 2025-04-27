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
    public class SpaceController : ControllerBase
    {
        private readonly IUseSpaceService _spaceService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public SpaceController(IUseSpaceService spaceService, IMapper mapper, ILoggerFactory logger)
        {
            _spaceService = spaceService;
            _mapper = mapper;
            _logger = logger.CreateLogger(typeof(SpaceController).FullName);
        }

        // Get all Spaces.
        [HttpGet(Name = "GetSpaces")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<SpaceOutputVM>>> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all Spaces...");
                var result = await _spaceService.GetAllAsync();
                var items = _mapper.Map<List<SpaceOutputVM>>(result);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all Spaces");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get a Space by ID.
        [HttpGet("{id}", Name = "GetSpace")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SpaceInfoVM>> GetById(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid Space ID received.");
                return BadRequest("Invalid Space ID.");
            }

            try
            {
                _logger.LogInformation("Fetching Space with ID: {id}", id);
                var entity = await _spaceService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("Space not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<SpaceInfoVM>(entity);
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching Space with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a Space by Lg.
        [HttpGet("GetSpaceByLanguage", Name = "GetSpaceByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SpaceOutputVM>> GetSpaceByLg(SpaceFilterVM model)
        {
            var id = model.Id;
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid Space ID received.");
                return BadRequest("Invalid Space ID.");
            }

            try
            {
                _logger.LogInformation("Fetching Space with ID: {id}", id);
                var entity = await _spaceService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("Space not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<SpaceOutputVM>(entity, opt => opt.Items.Add(HelperTranslation.KEYLG, model.Lg));
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching Space with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a Spaces by Lg.
        [HttpGet("GetSpacesByLanguage", Name = "GetSpacesByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<SpaceOutputVM>>> GetSpacesByLg(string? lg)
        {
            if (string.IsNullOrWhiteSpace(lg))
            {
                _logger.LogWarning("Invalid Space lg received.");
                return BadRequest("Invalid Space lg null ");
            }

            try
            {
                var spaces = await _spaceService.GetAllAsync();
                if (spaces == null)
                {
                    _logger.LogWarning("Spaces not found  by  ");
                    return NotFound();
                }

                var items = _mapper.Map<IEnumerable<SpaceOutputVM>>(spaces, opt => opt.Items.Add(HelperTranslation.KEYLG, lg));
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching Spaces with Lg: {lg}", lg);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create a new Space.
        [HttpPost(Name = "CreateSpace")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SpaceOutputVM>> Create([FromBody] SpaceCreateVM model)
        {
            if (model == null)
            {
                _logger.LogWarning("Space data is null in Create.");
                return BadRequest("Space data is required.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state in Create: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Creating new Space with data: {@model}", model);
                var item = _mapper.Map<SpaceRequestDso>(model);
                var createdEntity = await _spaceService.CreateAsync(item);
                var createdItem = _mapper.Map<SpaceOutputVM>(createdEntity);
                return Ok(createdItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating a new Space");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create multiple Spaces.
        [HttpPost("createRange")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<SpaceOutputVM>>> CreateRange([FromBody] IEnumerable<SpaceCreateVM> models)
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
                _logger.LogInformation("Creating multiple Spaces.");
                var items = _mapper.Map<List<SpaceRequestDso>>(models);
                var createdEntities = await _spaceService.CreateRangeAsync(items);
                var createdItems = _mapper.Map<List<SpaceOutputVM>>(createdEntities);
                return Ok(createdItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating multiple Spaces");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Update an existing Space.
        [HttpPut(Name = "UpdateSpace")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SpaceOutputVM>> Update([FromBody] SpaceUpdateVM model)
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
                _logger.LogInformation("Updating Space with ID: {id}", model?.Id);
                var item = _mapper.Map<SpaceRequestDso>(model);
                var updatedEntity = await _spaceService.UpdateAsync(item);
                if (updatedEntity == null)
                {
                    _logger.LogWarning("Space not found for update with ID: {id}", model?.Id);
                    return NotFound();
                }

                var updatedItem = _mapper.Map<SpaceOutputVM>(updatedEntity);
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating Space with ID: {id}", model?.Id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Delete a Space.
        [HttpDelete("{id}", Name = "DeleteSpace")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid Space ID received in Delete.");
                return BadRequest("Invalid Space ID.");
            }

            try
            {
                _logger.LogInformation("Deleting Space with ID: {id}", id);
                await _spaceService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting Space with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get count of Spaces.
        [HttpGet("CountSpace")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Count()
        {
            try
            {
                _logger.LogInformation("Counting Spaces...");
                var count = await _spaceService.CountAsync();
                return Ok(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while counting Spaces");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}