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
    public class ServiceController : ControllerBase
    {
        private readonly IUseServiceService _serviceService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public ServiceController(IUseServiceService serviceService, IMapper mapper, ILoggerFactory logger)
        {
            _serviceService = serviceService;
            _mapper = mapper;
            _logger = logger.CreateLogger(typeof(ServiceController).FullName);
        }

        // Get all Services.
        [HttpGet(Name = "GetServices")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ServiceOutputVM>>> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all Services...");
                var result = await _serviceService.GetAllAsync();
                var items = _mapper.Map<List<ServiceOutputVM>>(result);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all Services");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get a Service by ID.
        [HttpGet("{id}", Name = "GetService")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ServiceInfoVM>> GetById(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid Service ID received.");
                return BadRequest("Invalid Service ID.");
            }

            try
            {
                _logger.LogInformation("Fetching Service with ID: {id}", id);
                var entity = await _serviceService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("Service not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<ServiceInfoVM>(entity);
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching Service with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a Service by Lg.
        [HttpGet("GetServiceByLanguage", Name = "GetServiceByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ServiceOutputVM>> GetServiceByLg(ServiceFilterVM model)
        {
            var id = model.Id;
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid Service ID received.");
                return BadRequest("Invalid Service ID.");
            }

            try
            {
                _logger.LogInformation("Fetching Service with ID: {id}", id);
                var entity = await _serviceService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("Service not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<ServiceOutputVM>(entity, opt => opt.Items.Add(HelperTranslation.KEYLG, model.Lg));
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching Service with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a Services by Lg.
        [HttpGet("GetServicesByLanguage", Name = "GetServicesByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ServiceOutputVM>>> GetServicesByLg(string? lg)
        {
            if (string.IsNullOrWhiteSpace(lg))
            {
                _logger.LogWarning("Invalid Service lg received.");
                return BadRequest("Invalid Service lg null ");
            }

            try
            {
                var services = await _serviceService.GetAllAsync();
                if (services == null)
                {
                    _logger.LogWarning("Services not found  by  ");
                    return NotFound();
                }

                var items = _mapper.Map<IEnumerable<ServiceOutputVM>>(services, opt => opt.Items.Add(HelperTranslation.KEYLG, lg));
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching Services with Lg: {lg}", lg);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create a new Service.
        [HttpPost(Name = "CreateService")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ServiceOutputVM>> Create([FromBody] ServiceCreateVM model)
        {
            if (model == null)
            {
                _logger.LogWarning("Service data is null in Create.");
                return BadRequest("Service data is required.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state in Create: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Creating new Service with data: {@model}", model);
                var item = _mapper.Map<ServiceRequestDso>(model);
                var createdEntity = await _serviceService.CreateAsync(item);
                var createdItem = _mapper.Map<ServiceOutputVM>(createdEntity);
                return Ok(createdItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating a new Service");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create multiple Services.
        [HttpPost("createRange")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ServiceOutputVM>>> CreateRange([FromBody] IEnumerable<ServiceCreateVM> models)
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
                _logger.LogInformation("Creating multiple Services.");
                var items = _mapper.Map<List<ServiceRequestDso>>(models);
                var createdEntities = await _serviceService.CreateRangeAsync(items);
                var createdItems = _mapper.Map<List<ServiceOutputVM>>(createdEntities);
                return Ok(createdItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating multiple Services");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Update an existing Service.
        [HttpPut(Name = "UpdateService")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ServiceOutputVM>> Update([FromBody] ServiceUpdateVM model)
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
                _logger.LogInformation("Updating Service with ID: {id}", model?.Id);
                var item = _mapper.Map<ServiceRequestDso>(model);
                var updatedEntity = await _serviceService.UpdateAsync(item);
                if (updatedEntity == null)
                {
                    _logger.LogWarning("Service not found for update with ID: {id}", model?.Id);
                    return NotFound();
                }

                var updatedItem = _mapper.Map<ServiceOutputVM>(updatedEntity);
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating Service with ID: {id}", model?.Id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Delete a Service.
        [HttpDelete("{id}", Name = "DeleteService")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid Service ID received in Delete.");
                return BadRequest("Invalid Service ID.");
            }

            try
            {
                _logger.LogInformation("Deleting Service with ID: {id}", id);
                await _serviceService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting Service with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get count of Services.
        [HttpGet("CountService")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Count()
        {
            try
            {
                _logger.LogInformation("Counting Services...");
                var count = await _serviceService.CountAsync();
                return Ok(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while counting Services");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}