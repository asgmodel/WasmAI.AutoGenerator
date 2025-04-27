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
    public class SettingController : ControllerBase
    {
        private readonly IUseSettingService _settingService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public SettingController(IUseSettingService settingService, IMapper mapper, ILoggerFactory logger)
        {
            _settingService = settingService;
            _mapper = mapper;
            _logger = logger.CreateLogger(typeof(SettingController).FullName);
        }

        // Get all Settings.
        [HttpGet(Name = "GetSettings")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<SettingOutputVM>>> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all Settings...");
                var result = await _settingService.GetAllAsync();
                var items = _mapper.Map<List<SettingOutputVM>>(result);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all Settings");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get a Setting by ID.
        [HttpGet("{id}", Name = "GetSetting")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SettingInfoVM>> GetById(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid Setting ID received.");
                return BadRequest("Invalid Setting ID.");
            }

            try
            {
                _logger.LogInformation("Fetching Setting with ID: {id}", id);
                var entity = await _settingService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("Setting not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<SettingInfoVM>(entity);
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching Setting with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a Setting by Lg.
        [HttpGet("GetSettingByLanguage", Name = "GetSettingByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SettingOutputVM>> GetSettingByLg(SettingFilterVM model)
        {
            var id = model.Id;
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid Setting ID received.");
                return BadRequest("Invalid Setting ID.");
            }

            try
            {
                _logger.LogInformation("Fetching Setting with ID: {id}", id);
                var entity = await _settingService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("Setting not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<SettingOutputVM>(entity, opt => opt.Items.Add(HelperTranslation.KEYLG, model.Lg));
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching Setting with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a Settings by Lg.
        [HttpGet("GetSettingsByLanguage", Name = "GetSettingsByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<SettingOutputVM>>> GetSettingsByLg(string? lg)
        {
            if (string.IsNullOrWhiteSpace(lg))
            {
                _logger.LogWarning("Invalid Setting lg received.");
                return BadRequest("Invalid Setting lg null ");
            }

            try
            {
                var settings = await _settingService.GetAllAsync();
                if (settings == null)
                {
                    _logger.LogWarning("Settings not found  by  ");
                    return NotFound();
                }

                var items = _mapper.Map<IEnumerable<SettingOutputVM>>(settings, opt => opt.Items.Add(HelperTranslation.KEYLG, lg));
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching Settings with Lg: {lg}", lg);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create a new Setting.
        [HttpPost(Name = "CreateSetting")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SettingOutputVM>> Create([FromBody] SettingCreateVM model)
        {
            if (model == null)
            {
                _logger.LogWarning("Setting data is null in Create.");
                return BadRequest("Setting data is required.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state in Create: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Creating new Setting with data: {@model}", model);
                var item = _mapper.Map<SettingRequestDso>(model);
                var createdEntity = await _settingService.CreateAsync(item);
                var createdItem = _mapper.Map<SettingOutputVM>(createdEntity);
                return Ok(createdItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating a new Setting");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create multiple Settings.
        [HttpPost("createRange")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<SettingOutputVM>>> CreateRange([FromBody] IEnumerable<SettingCreateVM> models)
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
                _logger.LogInformation("Creating multiple Settings.");
                var items = _mapper.Map<List<SettingRequestDso>>(models);
                var createdEntities = await _settingService.CreateRangeAsync(items);
                var createdItems = _mapper.Map<List<SettingOutputVM>>(createdEntities);
                return Ok(createdItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating multiple Settings");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Update an existing Setting.
        [HttpPut(Name = "UpdateSetting")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SettingOutputVM>> Update([FromBody] SettingUpdateVM model)
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
                _logger.LogInformation("Updating Setting with ID: {id}", model?.Id);
                var item = _mapper.Map<SettingRequestDso>(model);
                var updatedEntity = await _settingService.UpdateAsync(item);
                if (updatedEntity == null)
                {
                    _logger.LogWarning("Setting not found for update with ID: {id}", model?.Id);
                    return NotFound();
                }

                var updatedItem = _mapper.Map<SettingOutputVM>(updatedEntity);
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating Setting with ID: {id}", model?.Id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Delete a Setting.
        [HttpDelete("{id}", Name = "DeleteSetting")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid Setting ID received in Delete.");
                return BadRequest("Invalid Setting ID.");
            }

            try
            {
                _logger.LogInformation("Deleting Setting with ID: {id}", id);
                await _settingService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting Setting with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get count of Settings.
        [HttpGet("CountSetting")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Count()
        {
            try
            {
                _logger.LogInformation("Counting Settings...");
                var count = await _settingService.CountAsync();
                return Ok(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while counting Settings");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}