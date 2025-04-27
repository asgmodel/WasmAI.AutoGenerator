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
    public class RequestController : ControllerBase
    {
        private readonly IUseRequestService _requestService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public RequestController(IUseRequestService requestService, IMapper mapper, ILoggerFactory logger)
        {
            _requestService = requestService;
            _mapper = mapper;
            _logger = logger.CreateLogger(typeof(RequestController).FullName);
        }

        // Get all Requests.
        [HttpGet(Name = "GetRequests")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<RequestOutputVM>>> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all Requests...");
                var result = await _requestService.GetAllAsync();
                var items = _mapper.Map<List<RequestOutputVM>>(result);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all Requests");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get a Request by ID.
        [HttpGet("{id}", Name = "GetRequest")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RequestInfoVM>> GetById(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid Request ID received.");
                return BadRequest("Invalid Request ID.");
            }

            try
            {
                _logger.LogInformation("Fetching Request with ID: {id}", id);
                var entity = await _requestService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("Request not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<RequestInfoVM>(entity);
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching Request with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a Request by Lg.
        [HttpGet("GetRequestByLanguage", Name = "GetRequestByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RequestOutputVM>> GetRequestByLg(RequestFilterVM model)
        {
            var id = model.Id;
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid Request ID received.");
                return BadRequest("Invalid Request ID.");
            }

            try
            {
                _logger.LogInformation("Fetching Request with ID: {id}", id);
                var entity = await _requestService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("Request not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<RequestOutputVM>(entity, opt => opt.Items.Add(HelperTranslation.KEYLG, model.Lg));
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching Request with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a Requests by Lg.
        [HttpGet("GetRequestsByLanguage", Name = "GetRequestsByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<RequestOutputVM>>> GetRequestsByLg(string? lg)
        {
            if (string.IsNullOrWhiteSpace(lg))
            {
                _logger.LogWarning("Invalid Request lg received.");
                return BadRequest("Invalid Request lg null ");
            }

            try
            {
                var requests = await _requestService.GetAllAsync();
                if (requests == null)
                {
                    _logger.LogWarning("Requests not found  by  ");
                    return NotFound();
                }

                var items = _mapper.Map<IEnumerable<RequestOutputVM>>(requests, opt => opt.Items.Add(HelperTranslation.KEYLG, lg));
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching Requests with Lg: {lg}", lg);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create a new Request.
        [HttpPost(Name = "CreateRequest")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RequestOutputVM>> Create([FromBody] RequestCreateVM model)
        {
            if (model == null)
            {
                _logger.LogWarning("Request data is null in Create.");
                return BadRequest("Request data is required.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state in Create: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Creating new Request with data: {@model}", model);
                var item = _mapper.Map<RequestRequestDso>(model);
                var createdEntity = await _requestService.CreateAsync(item);
                var createdItem = _mapper.Map<RequestOutputVM>(createdEntity);
                return Ok(createdItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating a new Request");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create multiple Requests.
        [HttpPost("createRange")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<RequestOutputVM>>> CreateRange([FromBody] IEnumerable<RequestCreateVM> models)
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
                _logger.LogInformation("Creating multiple Requests.");
                var items = _mapper.Map<List<RequestRequestDso>>(models);
                var createdEntities = await _requestService.CreateRangeAsync(items);
                var createdItems = _mapper.Map<List<RequestOutputVM>>(createdEntities);
                return Ok(createdItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating multiple Requests");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Update an existing Request.
        [HttpPut(Name = "UpdateRequest")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RequestOutputVM>> Update([FromBody] RequestUpdateVM model)
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
                _logger.LogInformation("Updating Request with ID: {id}", model?.Id);
                var item = _mapper.Map<RequestRequestDso>(model);
                var updatedEntity = await _requestService.UpdateAsync(item);
                if (updatedEntity == null)
                {
                    _logger.LogWarning("Request not found for update with ID: {id}", model?.Id);
                    return NotFound();
                }

                var updatedItem = _mapper.Map<RequestOutputVM>(updatedEntity);
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating Request with ID: {id}", model?.Id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Delete a Request.
        [HttpDelete("{id}", Name = "DeleteRequest")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid Request ID received in Delete.");
                return BadRequest("Invalid Request ID.");
            }

            try
            {
                _logger.LogInformation("Deleting Request with ID: {id}", id);
                await _requestService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting Request with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get count of Requests.
        [HttpGet("CountRequest")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Count()
        {
            try
            {
                _logger.LogInformation("Counting Requests...");
                var count = await _requestService.CountAsync();
                return Ok(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while counting Requests");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}