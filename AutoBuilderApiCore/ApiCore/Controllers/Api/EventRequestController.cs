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
    public class EventRequestController : ControllerBase
    {
        private readonly IUseEventRequestService _eventrequestService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public EventRequestController(IUseEventRequestService eventrequestService, IMapper mapper, ILoggerFactory logger)
        {
            _eventrequestService = eventrequestService;
            _mapper = mapper;
            _logger = logger.CreateLogger(typeof(EventRequestController).FullName);
        }

        // Get all EventRequests.
        [HttpGet(Name = "GetEventRequests")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<EventRequestOutputVM>>> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all EventRequests...");
                var result = await _eventrequestService.GetAllAsync();
                var items = _mapper.Map<List<EventRequestOutputVM>>(result);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all EventRequests");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get a EventRequest by ID.
        [HttpGet("{id}", Name = "GetEventRequest")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EventRequestInfoVM>> GetById(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid EventRequest ID received.");
                return BadRequest("Invalid EventRequest ID.");
            }

            try
            {
                _logger.LogInformation("Fetching EventRequest with ID: {id}", id);
                var entity = await _eventrequestService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("EventRequest not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<EventRequestInfoVM>(entity);
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching EventRequest with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a EventRequest by Lg.
        [HttpGet("GetEventRequestByLanguage", Name = "GetEventRequestByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EventRequestOutputVM>> GetEventRequestByLg(EventRequestFilterVM model)
        {
            var id = model.Id;
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid EventRequest ID received.");
                return BadRequest("Invalid EventRequest ID.");
            }

            try
            {
                _logger.LogInformation("Fetching EventRequest with ID: {id}", id);
                var entity = await _eventrequestService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("EventRequest not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<EventRequestOutputVM>(entity, opt => opt.Items.Add(HelperTranslation.KEYLG, model.Lg));
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching EventRequest with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a EventRequests by Lg.
        [HttpGet("GetEventRequestsByLanguage", Name = "GetEventRequestsByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<EventRequestOutputVM>>> GetEventRequestsByLg(string? lg)
        {
            if (string.IsNullOrWhiteSpace(lg))
            {
                _logger.LogWarning("Invalid EventRequest lg received.");
                return BadRequest("Invalid EventRequest lg null ");
            }

            try
            {
                var eventrequests = await _eventrequestService.GetAllAsync();
                if (eventrequests == null)
                {
                    _logger.LogWarning("EventRequests not found  by  ");
                    return NotFound();
                }

                var items = _mapper.Map<IEnumerable<EventRequestOutputVM>>(eventrequests, opt => opt.Items.Add(HelperTranslation.KEYLG, lg));
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching EventRequests with Lg: {lg}", lg);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create a new EventRequest.
        [HttpPost(Name = "CreateEventRequest")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EventRequestOutputVM>> Create([FromBody] EventRequestCreateVM model)
        {
            if (model == null)
            {
                _logger.LogWarning("EventRequest data is null in Create.");
                return BadRequest("EventRequest data is required.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state in Create: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Creating new EventRequest with data: {@model}", model);
                var item = _mapper.Map<EventRequestRequestDso>(model);
                var createdEntity = await _eventrequestService.CreateAsync(item);
                var createdItem = _mapper.Map<EventRequestOutputVM>(createdEntity);
                return Ok(createdItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating a new EventRequest");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create multiple EventRequests.
        [HttpPost("createRange")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<EventRequestOutputVM>>> CreateRange([FromBody] IEnumerable<EventRequestCreateVM> models)
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
                _logger.LogInformation("Creating multiple EventRequests.");
                var items = _mapper.Map<List<EventRequestRequestDso>>(models);
                var createdEntities = await _eventrequestService.CreateRangeAsync(items);
                var createdItems = _mapper.Map<List<EventRequestOutputVM>>(createdEntities);
                return Ok(createdItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating multiple EventRequests");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Update an existing EventRequest.
        [HttpPut(Name = "UpdateEventRequest")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EventRequestOutputVM>> Update([FromBody] EventRequestUpdateVM model)
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
                _logger.LogInformation("Updating EventRequest with ID: {id}", model?.Id);
                var item = _mapper.Map<EventRequestRequestDso>(model);
                var updatedEntity = await _eventrequestService.UpdateAsync(item);
                if (updatedEntity == null)
                {
                    _logger.LogWarning("EventRequest not found for update with ID: {id}", model?.Id);
                    return NotFound();
                }

                var updatedItem = _mapper.Map<EventRequestOutputVM>(updatedEntity);
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating EventRequest with ID: {id}", model?.Id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Delete a EventRequest.
        [HttpDelete("{id}", Name = "DeleteEventRequest")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid EventRequest ID received in Delete.");
                return BadRequest("Invalid EventRequest ID.");
            }

            try
            {
                _logger.LogInformation("Deleting EventRequest with ID: {id}", id);
                await _eventrequestService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting EventRequest with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get count of EventRequests.
        [HttpGet("CountEventRequest")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Count()
        {
            try
            {
                _logger.LogInformation("Counting EventRequests...");
                var count = await _eventrequestService.CountAsync();
                return Ok(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while counting EventRequests");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}