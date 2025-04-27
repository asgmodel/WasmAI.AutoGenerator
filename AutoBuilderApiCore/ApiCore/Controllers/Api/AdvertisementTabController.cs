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
    public class AdvertisementTabController : ControllerBase
    {
        private readonly IUseAdvertisementTabService _advertisementtabService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public AdvertisementTabController(IUseAdvertisementTabService advertisementtabService, IMapper mapper, ILoggerFactory logger)
        {
            _advertisementtabService = advertisementtabService;
            _mapper = mapper;
            _logger = logger.CreateLogger(typeof(AdvertisementTabController).FullName);
        }

        // Get all AdvertisementTabs.
        [HttpGet(Name = "GetAdvertisementTabs")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<AdvertisementTabOutputVM>>> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all AdvertisementTabs...");
                var result = await _advertisementtabService.GetAllAsync();
                var items = _mapper.Map<List<AdvertisementTabOutputVM>>(result);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all AdvertisementTabs");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get a AdvertisementTab by ID.
        [HttpGet("{id}", Name = "GetAdvertisementTab")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AdvertisementTabInfoVM>> GetById(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid AdvertisementTab ID received.");
                return BadRequest("Invalid AdvertisementTab ID.");
            }

            try
            {
                _logger.LogInformation("Fetching AdvertisementTab with ID: {id}", id);
                var entity = await _advertisementtabService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("AdvertisementTab not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<AdvertisementTabInfoVM>(entity);
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching AdvertisementTab with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a AdvertisementTab by Lg.
        [HttpGet("GetAdvertisementTabByLanguage", Name = "GetAdvertisementTabByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AdvertisementTabOutputVM>> GetAdvertisementTabByLg(AdvertisementTabFilterVM model)
        {
            var id = model.Id;
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid AdvertisementTab ID received.");
                return BadRequest("Invalid AdvertisementTab ID.");
            }

            try
            {
                _logger.LogInformation("Fetching AdvertisementTab with ID: {id}", id);
                var entity = await _advertisementtabService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("AdvertisementTab not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<AdvertisementTabOutputVM>(entity, opt => opt.Items.Add(HelperTranslation.KEYLG, model.Lg));
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching AdvertisementTab with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a AdvertisementTabs by Lg.
        [HttpGet("GetAdvertisementTabsByLanguage", Name = "GetAdvertisementTabsByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<AdvertisementTabOutputVM>>> GetAdvertisementTabsByLg(string? lg)
        {
            if (string.IsNullOrWhiteSpace(lg))
            {
                _logger.LogWarning("Invalid AdvertisementTab lg received.");
                return BadRequest("Invalid AdvertisementTab lg null ");
            }

            try
            {
                var advertisementtabs = await _advertisementtabService.GetAllAsync();
                if (advertisementtabs == null)
                {
                    _logger.LogWarning("AdvertisementTabs not found  by  ");
                    return NotFound();
                }

                var items = _mapper.Map<IEnumerable<AdvertisementTabOutputVM>>(advertisementtabs, opt => opt.Items.Add(HelperTranslation.KEYLG, lg));
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching AdvertisementTabs with Lg: {lg}", lg);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create a new AdvertisementTab.
        [HttpPost(Name = "CreateAdvertisementTab")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AdvertisementTabOutputVM>> Create([FromBody] AdvertisementTabCreateVM model)
        {
            if (model == null)
            {
                _logger.LogWarning("AdvertisementTab data is null in Create.");
                return BadRequest("AdvertisementTab data is required.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state in Create: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Creating new AdvertisementTab with data: {@model}", model);
                var item = _mapper.Map<AdvertisementTabRequestDso>(model);
                var createdEntity = await _advertisementtabService.CreateAsync(item);
                var createdItem = _mapper.Map<AdvertisementTabOutputVM>(createdEntity);
                return Ok(createdItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating a new AdvertisementTab");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create multiple AdvertisementTabs.
        [HttpPost("createRange")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<AdvertisementTabOutputVM>>> CreateRange([FromBody] IEnumerable<AdvertisementTabCreateVM> models)
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
                _logger.LogInformation("Creating multiple AdvertisementTabs.");
                var items = _mapper.Map<List<AdvertisementTabRequestDso>>(models);
                var createdEntities = await _advertisementtabService.CreateRangeAsync(items);
                var createdItems = _mapper.Map<List<AdvertisementTabOutputVM>>(createdEntities);
                return Ok(createdItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating multiple AdvertisementTabs");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Update an existing AdvertisementTab.
        [HttpPut(Name = "UpdateAdvertisementTab")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AdvertisementTabOutputVM>> Update([FromBody] AdvertisementTabUpdateVM model)
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
                _logger.LogInformation("Updating AdvertisementTab with ID: {id}", model?.Id);
                var item = _mapper.Map<AdvertisementTabRequestDso>(model);
                var updatedEntity = await _advertisementtabService.UpdateAsync(item);
                if (updatedEntity == null)
                {
                    _logger.LogWarning("AdvertisementTab not found for update with ID: {id}", model?.Id);
                    return NotFound();
                }

                var updatedItem = _mapper.Map<AdvertisementTabOutputVM>(updatedEntity);
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating AdvertisementTab with ID: {id}", model?.Id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Delete a AdvertisementTab.
        [HttpDelete("{id}", Name = "DeleteAdvertisementTab")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid AdvertisementTab ID received in Delete.");
                return BadRequest("Invalid AdvertisementTab ID.");
            }

            try
            {
                _logger.LogInformation("Deleting AdvertisementTab with ID: {id}", id);
                await _advertisementtabService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting AdvertisementTab with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get count of AdvertisementTabs.
        [HttpGet("CountAdvertisementTab")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Count()
        {
            try
            {
                _logger.LogInformation("Counting AdvertisementTabs...");
                var count = await _advertisementtabService.CountAsync();
                return Ok(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while counting AdvertisementTabs");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}