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
    public class ServiceMethodController : ControllerBase
    {
        private readonly IUseServiceMethodService _servicemethodService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public ServiceMethodController(IUseServiceMethodService servicemethodService, IMapper mapper, ILoggerFactory logger)
        {
            _servicemethodService = servicemethodService;
            _mapper = mapper;
            _logger = logger.CreateLogger(typeof(ServiceMethodController).FullName);
        }

        // Get all ServiceMethods.
        [HttpGet(Name = "GetServiceMethods")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ServiceMethodOutputVM>>> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all ServiceMethods...");
                var result = await _servicemethodService.GetAllAsync();
                var items = _mapper.Map<List<ServiceMethodOutputVM>>(result);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all ServiceMethods");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get a ServiceMethod by ID.
        [HttpGet("{id}", Name = "GetServiceMethod")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ServiceMethodInfoVM>> GetById(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid ServiceMethod ID received.");
                return BadRequest("Invalid ServiceMethod ID.");
            }

            try
            {
                _logger.LogInformation("Fetching ServiceMethod with ID: {id}", id);
                var entity = await _servicemethodService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("ServiceMethod not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<ServiceMethodInfoVM>(entity);
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching ServiceMethod with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a ServiceMethod by Lg.
        [HttpGet("GetServiceMethodByLanguage", Name = "GetServiceMethodByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ServiceMethodOutputVM>> GetServiceMethodByLg(ServiceMethodFilterVM model)
        {
            var id = model.Id;
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid ServiceMethod ID received.");
                return BadRequest("Invalid ServiceMethod ID.");
            }

            try
            {
                _logger.LogInformation("Fetching ServiceMethod with ID: {id}", id);
                var entity = await _servicemethodService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("ServiceMethod not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<ServiceMethodOutputVM>(entity, opt => opt.Items.Add(HelperTranslation.KEYLG, model.Lg));
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching ServiceMethod with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a ServiceMethods by Lg.
        [HttpGet("GetServiceMethodsByLanguage", Name = "GetServiceMethodsByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ServiceMethodOutputVM>>> GetServiceMethodsByLg(string? lg)
        {
            if (string.IsNullOrWhiteSpace(lg))
            {
                _logger.LogWarning("Invalid ServiceMethod lg received.");
                return BadRequest("Invalid ServiceMethod lg null ");
            }

            try
            {
                var servicemethods = await _servicemethodService.GetAllAsync();
                if (servicemethods == null)
                {
                    _logger.LogWarning("ServiceMethods not found  by  ");
                    return NotFound();
                }

                var items = _mapper.Map<IEnumerable<ServiceMethodOutputVM>>(servicemethods, opt => opt.Items.Add(HelperTranslation.KEYLG, lg));
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching ServiceMethods with Lg: {lg}", lg);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create a new ServiceMethod.
        [HttpPost(Name = "CreateServiceMethod")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ServiceMethodOutputVM>> Create([FromBody] ServiceMethodCreateVM model)
        {
            if (model == null)
            {
                _logger.LogWarning("ServiceMethod data is null in Create.");
                return BadRequest("ServiceMethod data is required.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state in Create: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Creating new ServiceMethod with data: {@model}", model);
                var item = _mapper.Map<ServiceMethodRequestDso>(model);
                var createdEntity = await _servicemethodService.CreateAsync(item);
                var createdItem = _mapper.Map<ServiceMethodOutputVM>(createdEntity);
                return Ok(createdItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating a new ServiceMethod");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create multiple ServiceMethods.
        [HttpPost("createRange")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ServiceMethodOutputVM>>> CreateRange([FromBody] IEnumerable<ServiceMethodCreateVM> models)
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
                _logger.LogInformation("Creating multiple ServiceMethods.");
                var items = _mapper.Map<List<ServiceMethodRequestDso>>(models);
                var createdEntities = await _servicemethodService.CreateRangeAsync(items);
                var createdItems = _mapper.Map<List<ServiceMethodOutputVM>>(createdEntities);
                return Ok(createdItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating multiple ServiceMethods");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Update an existing ServiceMethod.
        [HttpPut(Name = "UpdateServiceMethod")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ServiceMethodOutputVM>> Update([FromBody] ServiceMethodUpdateVM model)
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
                _logger.LogInformation("Updating ServiceMethod with ID: {id}", model?.Id);
                var item = _mapper.Map<ServiceMethodRequestDso>(model);
                var updatedEntity = await _servicemethodService.UpdateAsync(item);
                if (updatedEntity == null)
                {
                    _logger.LogWarning("ServiceMethod not found for update with ID: {id}", model?.Id);
                    return NotFound();
                }

                var updatedItem = _mapper.Map<ServiceMethodOutputVM>(updatedEntity);
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating ServiceMethod with ID: {id}", model?.Id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Delete a ServiceMethod.
        [HttpDelete("{id}", Name = "DeleteServiceMethod")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid ServiceMethod ID received in Delete.");
                return BadRequest("Invalid ServiceMethod ID.");
            }

            try
            {
                _logger.LogInformation("Deleting ServiceMethod with ID: {id}", id);
                await _servicemethodService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting ServiceMethod with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get count of ServiceMethods.
        [HttpGet("CountServiceMethod")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Count()
        {
            try
            {
                _logger.LogInformation("Counting ServiceMethods...");
                var count = await _servicemethodService.CountAsync();
                return Ok(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while counting ServiceMethods");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}