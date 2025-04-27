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
    public class ModelAiController : ControllerBase
    {
        private readonly IUseModelAiService _modelaiService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public ModelAiController(IUseModelAiService modelaiService, IMapper mapper, ILoggerFactory logger)
        {
            _modelaiService = modelaiService;
            _mapper = mapper;
            _logger = logger.CreateLogger(typeof(ModelAiController).FullName);
        }

        // Get all ModelAis.
        [HttpGet(Name = "GetModelAis")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ModelAiOutputVM>>> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all ModelAis...");
                var result = await _modelaiService.GetAllAsync();
                var items = _mapper.Map<List<ModelAiOutputVM>>(result);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all ModelAis");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get a ModelAi by ID.
        [HttpGet("{id}", Name = "GetModelAi")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ModelAiInfoVM>> GetById(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid ModelAi ID received.");
                return BadRequest("Invalid ModelAi ID.");
            }

            try
            {
                _logger.LogInformation("Fetching ModelAi with ID: {id}", id);
                var entity = await _modelaiService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("ModelAi not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<ModelAiInfoVM>(entity);
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching ModelAi with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a ModelAi by Lg.
        [HttpGet("GetModelAiByLanguage", Name = "GetModelAiByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ModelAiOutputVM>> GetModelAiByLg(ModelAiFilterVM model)
        {
            var id = model.Id;
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid ModelAi ID received.");
                return BadRequest("Invalid ModelAi ID.");
            }

            try
            {
                _logger.LogInformation("Fetching ModelAi with ID: {id}", id);
                var entity = await _modelaiService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("ModelAi not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<ModelAiOutputVM>(entity, opt => opt.Items.Add(HelperTranslation.KEYLG, model.Lg));
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching ModelAi with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a ModelAis by Lg.
        [HttpGet("GetModelAisByLanguage", Name = "GetModelAisByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ModelAiOutputVM>>> GetModelAisByLg(string? lg)
        {
            if (string.IsNullOrWhiteSpace(lg))
            {
                _logger.LogWarning("Invalid ModelAi lg received.");
                return BadRequest("Invalid ModelAi lg null ");
            }

            try
            {
                var modelais = await _modelaiService.GetAllAsync();
                if (modelais == null)
                {
                    _logger.LogWarning("ModelAis not found  by  ");
                    return NotFound();
                }

                var items = _mapper.Map<IEnumerable<ModelAiOutputVM>>(modelais, opt => opt.Items.Add(HelperTranslation.KEYLG, lg));
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching ModelAis with Lg: {lg}", lg);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create a new ModelAi.
        [HttpPost(Name = "CreateModelAi")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ModelAiOutputVM>> Create([FromBody] ModelAiCreateVM model)
        {
            if (model == null)
            {
                _logger.LogWarning("ModelAi data is null in Create.");
                return BadRequest("ModelAi data is required.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state in Create: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Creating new ModelAi with data: {@model}", model);
                var item = _mapper.Map<ModelAiRequestDso>(model);
                var createdEntity = await _modelaiService.CreateAsync(item);
                var createdItem = _mapper.Map<ModelAiOutputVM>(createdEntity);
                return Ok(createdItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating a new ModelAi");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create multiple ModelAis.
        [HttpPost("createRange")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ModelAiOutputVM>>> CreateRange([FromBody] IEnumerable<ModelAiCreateVM> models)
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
                _logger.LogInformation("Creating multiple ModelAis.");
                var items = _mapper.Map<List<ModelAiRequestDso>>(models);
                var createdEntities = await _modelaiService.CreateRangeAsync(items);
                var createdItems = _mapper.Map<List<ModelAiOutputVM>>(createdEntities);
                return Ok(createdItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating multiple ModelAis");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Update an existing ModelAi.
        [HttpPut(Name = "UpdateModelAi")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ModelAiOutputVM>> Update([FromBody] ModelAiUpdateVM model)
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
                _logger.LogInformation("Updating ModelAi with ID: {id}", model?.Id);
                var item = _mapper.Map<ModelAiRequestDso>(model);
                var updatedEntity = await _modelaiService.UpdateAsync(item);
                if (updatedEntity == null)
                {
                    _logger.LogWarning("ModelAi not found for update with ID: {id}", model?.Id);
                    return NotFound();
                }

                var updatedItem = _mapper.Map<ModelAiOutputVM>(updatedEntity);
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating ModelAi with ID: {id}", model?.Id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Delete a ModelAi.
        [HttpDelete("{id}", Name = "DeleteModelAi")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid ModelAi ID received in Delete.");
                return BadRequest("Invalid ModelAi ID.");
            }

            try
            {
                _logger.LogInformation("Deleting ModelAi with ID: {id}", id);
                await _modelaiService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting ModelAi with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get count of ModelAis.
        [HttpGet("CountModelAi")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Count()
        {
            try
            {
                _logger.LogInformation("Counting ModelAis...");
                var count = await _modelaiService.CountAsync();
                return Ok(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while counting ModelAis");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}