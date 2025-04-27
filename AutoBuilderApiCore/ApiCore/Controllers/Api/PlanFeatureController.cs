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
    public class PlanFeatureController : ControllerBase
    {
        private readonly IUsePlanFeatureService _planfeatureService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public PlanFeatureController(IUsePlanFeatureService planfeatureService, IMapper mapper, ILoggerFactory logger)
        {
            _planfeatureService = planfeatureService;
            _mapper = mapper;
            _logger = logger.CreateLogger(typeof(PlanFeatureController).FullName);
        }

        // Get all PlanFeatures.
        [HttpGet(Name = "GetPlanFeatures")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<PlanFeatureOutputVM>>> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all PlanFeatures...");
                var result = await _planfeatureService.GetAllAsync();
                var items = _mapper.Map<List<PlanFeatureOutputVM>>(result);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all PlanFeatures");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get a PlanFeature by ID.
        [HttpGet("{id}", Name = "GetPlanFeature")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PlanFeatureInfoVM>> GetById(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid PlanFeature ID received.");
                return BadRequest("Invalid PlanFeature ID.");
            }

            try
            {
                _logger.LogInformation("Fetching PlanFeature with ID: {id}", id);
                var entity = await _planfeatureService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("PlanFeature not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<PlanFeatureInfoVM>(entity);
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching PlanFeature with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a PlanFeature by Lg.
        [HttpGet("GetPlanFeatureByLanguage", Name = "GetPlanFeatureByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PlanFeatureOutputVM>> GetPlanFeatureByLg(PlanFeatureFilterVM model)
        {
            var id = model.Id;
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid PlanFeature ID received.");
                return BadRequest("Invalid PlanFeature ID.");
            }

            try
            {
                _logger.LogInformation("Fetching PlanFeature with ID: {id}", id);
                var entity = await _planfeatureService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("PlanFeature not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<PlanFeatureOutputVM>(entity, opt => opt.Items.Add(HelperTranslation.KEYLG, model.Lg));
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching PlanFeature with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a PlanFeatures by Lg.
        [HttpGet("GetPlanFeaturesByLanguage", Name = "GetPlanFeaturesByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<PlanFeatureOutputVM>>> GetPlanFeaturesByLg(string? lg)
        {
            if (string.IsNullOrWhiteSpace(lg))
            {
                _logger.LogWarning("Invalid PlanFeature lg received.");
                return BadRequest("Invalid PlanFeature lg null ");
            }

            try
            {
                var planfeatures = await _planfeatureService.GetAllAsync();
                if (planfeatures == null)
                {
                    _logger.LogWarning("PlanFeatures not found  by  ");
                    return NotFound();
                }

                var items = _mapper.Map<IEnumerable<PlanFeatureOutputVM>>(planfeatures, opt => opt.Items.Add(HelperTranslation.KEYLG, lg));
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching PlanFeatures with Lg: {lg}", lg);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create a new PlanFeature.
        [HttpPost(Name = "CreatePlanFeature")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PlanFeatureOutputVM>> Create([FromBody] PlanFeatureCreateVM model)
        {
            if (model == null)
            {
                _logger.LogWarning("PlanFeature data is null in Create.");
                return BadRequest("PlanFeature data is required.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state in Create: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Creating new PlanFeature with data: {@model}", model);
                var item = _mapper.Map<PlanFeatureRequestDso>(model);
                var createdEntity = await _planfeatureService.CreateAsync(item);
                var createdItem = _mapper.Map<PlanFeatureOutputVM>(createdEntity);
                return Ok(createdItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating a new PlanFeature");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create multiple PlanFeatures.
        [HttpPost("createRange")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<PlanFeatureOutputVM>>> CreateRange([FromBody] IEnumerable<PlanFeatureCreateVM> models)
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
                _logger.LogInformation("Creating multiple PlanFeatures.");
                var items = _mapper.Map<List<PlanFeatureRequestDso>>(models);
                var createdEntities = await _planfeatureService.CreateRangeAsync(items);
                var createdItems = _mapper.Map<List<PlanFeatureOutputVM>>(createdEntities);
                return Ok(createdItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating multiple PlanFeatures");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Update an existing PlanFeature.
        [HttpPut(Name = "UpdatePlanFeature")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PlanFeatureOutputVM>> Update([FromBody] PlanFeatureUpdateVM model)
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
                _logger.LogInformation("Updating PlanFeature with ID: {id}", model?.Id);
                var item = _mapper.Map<PlanFeatureRequestDso>(model);
                var updatedEntity = await _planfeatureService.UpdateAsync(item);
                if (updatedEntity == null)
                {
                    _logger.LogWarning("PlanFeature not found for update with ID: {id}", model?.Id);
                    return NotFound();
                }

                var updatedItem = _mapper.Map<PlanFeatureOutputVM>(updatedEntity);
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating PlanFeature with ID: {id}", model?.Id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Delete a PlanFeature.
        [HttpDelete("{id}", Name = "DeletePlanFeature")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid PlanFeature ID received in Delete.");
                return BadRequest("Invalid PlanFeature ID.");
            }

            try
            {
                _logger.LogInformation("Deleting PlanFeature with ID: {id}", id);
                await _planfeatureService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting PlanFeature with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get count of PlanFeatures.
        [HttpGet("CountPlanFeature")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Count()
        {
            try
            {
                _logger.LogInformation("Counting PlanFeatures...");
                var count = await _planfeatureService.CountAsync();
                return Ok(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while counting PlanFeatures");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}