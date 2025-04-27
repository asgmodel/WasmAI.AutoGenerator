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
    public class UserModelAiController : ControllerBase
    {
        private readonly IUseUserModelAiService _usermodelaiService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public UserModelAiController(IUseUserModelAiService usermodelaiService, IMapper mapper, ILoggerFactory logger)
        {
            _usermodelaiService = usermodelaiService;
            _mapper = mapper;
            _logger = logger.CreateLogger(typeof(UserModelAiController).FullName);
        }

        // Get all UserModelAis.
        [HttpGet(Name = "GetUserModelAis")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<UserModelAiOutputVM>>> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all UserModelAis...");
                var result = await _usermodelaiService.GetAllAsync();
                var items = _mapper.Map<List<UserModelAiOutputVM>>(result);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all UserModelAis");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get a UserModelAi by ID.
        [HttpGet("{id}", Name = "GetUserModelAi")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserModelAiInfoVM>> GetById(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid UserModelAi ID received.");
                return BadRequest("Invalid UserModelAi ID.");
            }

            try
            {
                _logger.LogInformation("Fetching UserModelAi with ID: {id}", id);
                var entity = await _usermodelaiService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("UserModelAi not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<UserModelAiInfoVM>(entity);
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching UserModelAi with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a UserModelAi by Lg.
        [HttpGet("GetUserModelAiByLanguage", Name = "GetUserModelAiByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserModelAiOutputVM>> GetUserModelAiByLg(UserModelAiFilterVM model)
        {
            var id = model.Id;
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid UserModelAi ID received.");
                return BadRequest("Invalid UserModelAi ID.");
            }

            try
            {
                _logger.LogInformation("Fetching UserModelAi with ID: {id}", id);
                var entity = await _usermodelaiService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("UserModelAi not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<UserModelAiOutputVM>(entity, opt => opt.Items.Add(HelperTranslation.KEYLG, model.Lg));
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching UserModelAi with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a UserModelAis by Lg.
        [HttpGet("GetUserModelAisByLanguage", Name = "GetUserModelAisByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<UserModelAiOutputVM>>> GetUserModelAisByLg(string? lg)
        {
            if (string.IsNullOrWhiteSpace(lg))
            {
                _logger.LogWarning("Invalid UserModelAi lg received.");
                return BadRequest("Invalid UserModelAi lg null ");
            }

            try
            {
                var usermodelais = await _usermodelaiService.GetAllAsync();
                if (usermodelais == null)
                {
                    _logger.LogWarning("UserModelAis not found  by  ");
                    return NotFound();
                }

                var items = _mapper.Map<IEnumerable<UserModelAiOutputVM>>(usermodelais, opt => opt.Items.Add(HelperTranslation.KEYLG, lg));
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching UserModelAis with Lg: {lg}", lg);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create a new UserModelAi.
        [HttpPost(Name = "CreateUserModelAi")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserModelAiOutputVM>> Create([FromBody] UserModelAiCreateVM model)
        {
            if (model == null)
            {
                _logger.LogWarning("UserModelAi data is null in Create.");
                return BadRequest("UserModelAi data is required.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state in Create: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Creating new UserModelAi with data: {@model}", model);
                var item = _mapper.Map<UserModelAiRequestDso>(model);
                var createdEntity = await _usermodelaiService.CreateAsync(item);
                var createdItem = _mapper.Map<UserModelAiOutputVM>(createdEntity);
                return Ok(createdItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating a new UserModelAi");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create multiple UserModelAis.
        [HttpPost("createRange")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<UserModelAiOutputVM>>> CreateRange([FromBody] IEnumerable<UserModelAiCreateVM> models)
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
                _logger.LogInformation("Creating multiple UserModelAis.");
                var items = _mapper.Map<List<UserModelAiRequestDso>>(models);
                var createdEntities = await _usermodelaiService.CreateRangeAsync(items);
                var createdItems = _mapper.Map<List<UserModelAiOutputVM>>(createdEntities);
                return Ok(createdItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating multiple UserModelAis");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Update an existing UserModelAi.
        [HttpPut(Name = "UpdateUserModelAi")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserModelAiOutputVM>> Update([FromBody] UserModelAiUpdateVM model)
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
                _logger.LogInformation("Updating UserModelAi with ID: {id}", model?.Id);
                var item = _mapper.Map<UserModelAiRequestDso>(model);
                var updatedEntity = await _usermodelaiService.UpdateAsync(item);
                if (updatedEntity == null)
                {
                    _logger.LogWarning("UserModelAi not found for update with ID: {id}", model?.Id);
                    return NotFound();
                }

                var updatedItem = _mapper.Map<UserModelAiOutputVM>(updatedEntity);
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating UserModelAi with ID: {id}", model?.Id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Delete a UserModelAi.
        [HttpDelete("{id}", Name = "DeleteUserModelAi")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid UserModelAi ID received in Delete.");
                return BadRequest("Invalid UserModelAi ID.");
            }

            try
            {
                _logger.LogInformation("Deleting UserModelAi with ID: {id}", id);
                await _usermodelaiService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting UserModelAi with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get count of UserModelAis.
        [HttpGet("CountUserModelAi")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Count()
        {
            try
            {
                _logger.LogInformation("Counting UserModelAis...");
                var count = await _usermodelaiService.CountAsync();
                return Ok(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while counting UserModelAis");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}