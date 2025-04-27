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
    public class ModelGatewayController : ControllerBase
    {
        private readonly IUseModelGatewayService _modelgatewayService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public ModelGatewayController(IUseModelGatewayService modelgatewayService, IMapper mapper, ILoggerFactory logger)
        {
            _modelgatewayService = modelgatewayService;
            _mapper = mapper;
            _logger = logger.CreateLogger(typeof(ModelGatewayController).FullName);
        }

        // Get all ModelGateways.
        [HttpGet(Name = "GetModelGateways")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ModelGatewayOutputVM>>> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all ModelGateways...");
                var result = await _modelgatewayService.GetAllAsync();
                var items = _mapper.Map<List<ModelGatewayOutputVM>>(result);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all ModelGateways");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get a ModelGateway by ID.
        [HttpGet("{id}", Name = "GetModelGateway")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ModelGatewayInfoVM>> GetById(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid ModelGateway ID received.");
                return BadRequest("Invalid ModelGateway ID.");
            }

            try
            {
                _logger.LogInformation("Fetching ModelGateway with ID: {id}", id);
                var entity = await _modelgatewayService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("ModelGateway not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<ModelGatewayInfoVM>(entity);
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching ModelGateway with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a ModelGateway by Lg.
        [HttpGet("GetModelGatewayByLanguage", Name = "GetModelGatewayByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ModelGatewayOutputVM>> GetModelGatewayByLg(ModelGatewayFilterVM model)
        {
            var id = model.Id;
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid ModelGateway ID received.");
                return BadRequest("Invalid ModelGateway ID.");
            }

            try
            {
                _logger.LogInformation("Fetching ModelGateway with ID: {id}", id);
                var entity = await _modelgatewayService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("ModelGateway not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<ModelGatewayOutputVM>(entity, opt => opt.Items.Add(HelperTranslation.KEYLG, model.Lg));
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching ModelGateway with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a ModelGateways by Lg.
        [HttpGet("GetModelGatewaysByLanguage", Name = "GetModelGatewaysByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ModelGatewayOutputVM>>> GetModelGatewaysByLg(string? lg)
        {
            if (string.IsNullOrWhiteSpace(lg))
            {
                _logger.LogWarning("Invalid ModelGateway lg received.");
                return BadRequest("Invalid ModelGateway lg null ");
            }

            try
            {
                var modelgateways = await _modelgatewayService.GetAllAsync();
                if (modelgateways == null)
                {
                    _logger.LogWarning("ModelGateways not found  by  ");
                    return NotFound();
                }

                var items = _mapper.Map<IEnumerable<ModelGatewayOutputVM>>(modelgateways, opt => opt.Items.Add(HelperTranslation.KEYLG, lg));
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching ModelGateways with Lg: {lg}", lg);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create a new ModelGateway.
        [HttpPost(Name = "CreateModelGateway")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ModelGatewayOutputVM>> Create([FromBody] ModelGatewayCreateVM model)
        {
            if (model == null)
            {
                _logger.LogWarning("ModelGateway data is null in Create.");
                return BadRequest("ModelGateway data is required.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state in Create: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Creating new ModelGateway with data: {@model}", model);
                var item = _mapper.Map<ModelGatewayRequestDso>(model);
                var createdEntity = await _modelgatewayService.CreateAsync(item);
                var createdItem = _mapper.Map<ModelGatewayOutputVM>(createdEntity);
                return Ok(createdItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating a new ModelGateway");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create multiple ModelGateways.
        [HttpPost("createRange")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ModelGatewayOutputVM>>> CreateRange([FromBody] IEnumerable<ModelGatewayCreateVM> models)
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
                _logger.LogInformation("Creating multiple ModelGateways.");
                var items = _mapper.Map<List<ModelGatewayRequestDso>>(models);
                var createdEntities = await _modelgatewayService.CreateRangeAsync(items);
                var createdItems = _mapper.Map<List<ModelGatewayOutputVM>>(createdEntities);
                return Ok(createdItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating multiple ModelGateways");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Update an existing ModelGateway.
        [HttpPut(Name = "UpdateModelGateway")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ModelGatewayOutputVM>> Update([FromBody] ModelGatewayUpdateVM model)
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
                _logger.LogInformation("Updating ModelGateway with ID: {id}", model?.Id);
                var item = _mapper.Map<ModelGatewayRequestDso>(model);
                var updatedEntity = await _modelgatewayService.UpdateAsync(item);
                if (updatedEntity == null)
                {
                    _logger.LogWarning("ModelGateway not found for update with ID: {id}", model?.Id);
                    return NotFound();
                }

                var updatedItem = _mapper.Map<ModelGatewayOutputVM>(updatedEntity);
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating ModelGateway with ID: {id}", model?.Id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Delete a ModelGateway.
        [HttpDelete("{id}", Name = "DeleteModelGateway")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid ModelGateway ID received in Delete.");
                return BadRequest("Invalid ModelGateway ID.");
            }

            try
            {
                _logger.LogInformation("Deleting ModelGateway with ID: {id}", id);
                await _modelgatewayService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting ModelGateway with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get count of ModelGateways.
        [HttpGet("CountModelGateway")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Count()
        {
            try
            {
                _logger.LogInformation("Counting ModelGateways...");
                var count = await _modelgatewayService.CountAsync();
                return Ok(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while counting ModelGateways");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}