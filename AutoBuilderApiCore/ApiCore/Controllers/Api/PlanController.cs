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
    public class PlanController : ControllerBase
    {
        private readonly IUsePlanService _planService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public PlanController(IUsePlanService planService, IMapper mapper, ILoggerFactory logger)
        {
            _planService = planService;
            _mapper = mapper;
            _logger = logger.CreateLogger(typeof(PlanController).FullName);
        }

        // Get all Plans.
        [HttpGet(Name = "GetPlans")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<PlanOutputVM>>> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all Plans...");
                var result = await _planService.GetAllAsync();
                var items = _mapper.Map<List<PlanOutputVM>>(result);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all Plans");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get a Plan by ID.
        [HttpGet("{id}", Name = "GetPlan")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PlanInfoVM>> GetById(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid Plan ID received.");
                return BadRequest("Invalid Plan ID.");
            }

            try
            {
                _logger.LogInformation("Fetching Plan with ID: {id}", id);
                var entity = await _planService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("Plan not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<PlanInfoVM>(entity);
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching Plan with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a Plan by Lg.
        [HttpGet("GetPlanByLanguage", Name = "GetPlanByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PlanOutputVM>> GetPlanByLg(PlanFilterVM model)
        {
            var id = model.Id;
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid Plan ID received.");
                return BadRequest("Invalid Plan ID.");
            }

            try
            {
                _logger.LogInformation("Fetching Plan with ID: {id}", id);
                var entity = await _planService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("Plan not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<PlanOutputVM>(entity, opt => opt.Items.Add(HelperTranslation.KEYLG, model.Lg));
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching Plan with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a Plans by Lg.
        [HttpGet("GetPlansByLanguage", Name = "GetPlansByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<PlanOutputVM>>> GetPlansByLg(string? lg)
        {
            if (string.IsNullOrWhiteSpace(lg))
            {
                _logger.LogWarning("Invalid Plan lg received.");
                return BadRequest("Invalid Plan lg null ");
            }

            try
            {
                var plans = await _planService.GetAllAsync();
                if (plans == null)
                {
                    _logger.LogWarning("Plans not found  by  ");
                    return NotFound();
                }

                var items = _mapper.Map<IEnumerable<PlanOutputVM>>(plans, opt => opt.Items.Add(HelperTranslation.KEYLG, lg));
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching Plans with Lg: {lg}", lg);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create a new Plan.
        [HttpPost(Name = "CreatePlan")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PlanOutputVM>> Create([FromBody] PlanCreateVM model)
        {
            if (model == null)
            {
                _logger.LogWarning("Plan data is null in Create.");
                return BadRequest("Plan data is required.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state in Create: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Creating new Plan with data: {@model}", model);
                var item = _mapper.Map<PlanRequestDso>(model);
                var createdEntity = await _planService.CreateAsync(item);
                var createdItem = _mapper.Map<PlanOutputVM>(createdEntity);
                return Ok(createdItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating a new Plan");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create multiple Plans.
        [HttpPost("createRange")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<PlanOutputVM>>> CreateRange([FromBody] IEnumerable<PlanCreateVM> models)
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
                _logger.LogInformation("Creating multiple Plans.");
                var items = _mapper.Map<List<PlanRequestDso>>(models);
                var createdEntities = await _planService.CreateRangeAsync(items);
                var createdItems = _mapper.Map<List<PlanOutputVM>>(createdEntities);
                return Ok(createdItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating multiple Plans");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Update an existing Plan.
        [HttpPut(Name = "UpdatePlan")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PlanOutputVM>> Update([FromBody] PlanUpdateVM model)
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
                _logger.LogInformation("Updating Plan with ID: {id}", model?.Id);
                var item = _mapper.Map<PlanRequestDso>(model);
                var updatedEntity = await _planService.UpdateAsync(item);
                if (updatedEntity == null)
                {
                    _logger.LogWarning("Plan not found for update with ID: {id}", model?.Id);
                    return NotFound();
                }

                var updatedItem = _mapper.Map<PlanOutputVM>(updatedEntity);
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating Plan with ID: {id}", model?.Id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Delete a Plan.
        [HttpDelete("{id}", Name = "DeletePlan")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid Plan ID received in Delete.");
                return BadRequest("Invalid Plan ID.");
            }

            try
            {
                _logger.LogInformation("Deleting Plan with ID: {id}", id);
                await _planService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting Plan with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get count of Plans.
        [HttpGet("CountPlan")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Count()
        {
            try
            {
                _logger.LogInformation("Counting Plans...");
                var count = await _planService.CountAsync();
                return Ok(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while counting Plans");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}