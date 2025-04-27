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
    public class TypeModelController : ControllerBase
    {
        private readonly IUseTypeModelService _typemodelService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public TypeModelController(IUseTypeModelService typemodelService, IMapper mapper, ILoggerFactory logger)
        {
            _typemodelService = typemodelService;
            _mapper = mapper;
            _logger = logger.CreateLogger(typeof(TypeModelController).FullName);
        }

        // Get all TypeModels.
        [HttpGet(Name = "GetTypeModels")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<TypeModelOutputVM>>> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all TypeModels...");
                var result = await _typemodelService.GetAllAsync();
                var items = _mapper.Map<List<TypeModelOutputVM>>(result);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all TypeModels");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get a TypeModel by ID.
        [HttpGet("{id}", Name = "GetTypeModel")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TypeModelInfoVM>> GetById(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid TypeModel ID received.");
                return BadRequest("Invalid TypeModel ID.");
            }

            try
            {
                _logger.LogInformation("Fetching TypeModel with ID: {id}", id);
                var entity = await _typemodelService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("TypeModel not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<TypeModelInfoVM>(entity);
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching TypeModel with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a TypeModel by Lg.
        [HttpGet("GetTypeModelByLanguage", Name = "GetTypeModelByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TypeModelOutputVM>> GetTypeModelByLg(TypeModelFilterVM model)
        {
            var id = model.Id;
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid TypeModel ID received.");
                return BadRequest("Invalid TypeModel ID.");
            }

            try
            {
                _logger.LogInformation("Fetching TypeModel with ID: {id}", id);
                var entity = await _typemodelService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("TypeModel not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<TypeModelOutputVM>(entity, opt => opt.Items.Add(HelperTranslation.KEYLG, model.Lg));
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching TypeModel with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a TypeModels by Lg.
        [HttpGet("GetTypeModelsByLanguage", Name = "GetTypeModelsByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<TypeModelOutputVM>>> GetTypeModelsByLg(string? lg)
        {
            if (string.IsNullOrWhiteSpace(lg))
            {
                _logger.LogWarning("Invalid TypeModel lg received.");
                return BadRequest("Invalid TypeModel lg null ");
            }

            try
            {
                var typemodels = await _typemodelService.GetAllAsync();
                if (typemodels == null)
                {
                    _logger.LogWarning("TypeModels not found  by  ");
                    return NotFound();
                }

                var items = _mapper.Map<IEnumerable<TypeModelOutputVM>>(typemodels, opt => opt.Items.Add(HelperTranslation.KEYLG, lg));
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching TypeModels with Lg: {lg}", lg);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create a new TypeModel.
        [HttpPost(Name = "CreateTypeModel")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TypeModelOutputVM>> Create([FromBody] TypeModelCreateVM model)
        {
            if (model == null)
            {
                _logger.LogWarning("TypeModel data is null in Create.");
                return BadRequest("TypeModel data is required.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state in Create: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Creating new TypeModel with data: {@model}", model);
                var item = _mapper.Map<TypeModelRequestDso>(model);
                var createdEntity = await _typemodelService.CreateAsync(item);
                var createdItem = _mapper.Map<TypeModelOutputVM>(createdEntity);
                return Ok(createdItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating a new TypeModel");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create multiple TypeModels.
        [HttpPost("createRange")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<TypeModelOutputVM>>> CreateRange([FromBody] IEnumerable<TypeModelCreateVM> models)
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
                _logger.LogInformation("Creating multiple TypeModels.");
                var items = _mapper.Map<List<TypeModelRequestDso>>(models);
                var createdEntities = await _typemodelService.CreateRangeAsync(items);
                var createdItems = _mapper.Map<List<TypeModelOutputVM>>(createdEntities);
                return Ok(createdItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating multiple TypeModels");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Update an existing TypeModel.
        [HttpPut(Name = "UpdateTypeModel")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TypeModelOutputVM>> Update([FromBody] TypeModelUpdateVM model)
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
                _logger.LogInformation("Updating TypeModel with ID: {id}", model?.Id);
                var item = _mapper.Map<TypeModelRequestDso>(model);
                var updatedEntity = await _typemodelService.UpdateAsync(item);
                if (updatedEntity == null)
                {
                    _logger.LogWarning("TypeModel not found for update with ID: {id}", model?.Id);
                    return NotFound();
                }

                var updatedItem = _mapper.Map<TypeModelOutputVM>(updatedEntity);
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating TypeModel with ID: {id}", model?.Id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Delete a TypeModel.
        [HttpDelete("{id}", Name = "DeleteTypeModel")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid TypeModel ID received in Delete.");
                return BadRequest("Invalid TypeModel ID.");
            }

            try
            {
                _logger.LogInformation("Deleting TypeModel with ID: {id}", id);
                await _typemodelService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting TypeModel with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get count of TypeModels.
        [HttpGet("CountTypeModel")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Count()
        {
            try
            {
                _logger.LogInformation("Counting TypeModels...");
                var count = await _typemodelService.CountAsync();
                return Ok(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while counting TypeModels");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}