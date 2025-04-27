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
    public class LanguageController : ControllerBase
    {
        private readonly IUseLanguageService _languageService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public LanguageController(IUseLanguageService languageService, IMapper mapper, ILoggerFactory logger)
        {
            _languageService = languageService;
            _mapper = mapper;
            _logger = logger.CreateLogger(typeof(LanguageController).FullName);
        }

        // Get all Languages.
        [HttpGet(Name = "GetLanguages")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<LanguageOutputVM>>> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all Languages...");
                var result = await _languageService.GetAllAsync();
                var items = _mapper.Map<List<LanguageOutputVM>>(result);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all Languages");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get a Language by ID.
        [HttpGet("{id}", Name = "GetLanguage")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LanguageInfoVM>> GetById(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid Language ID received.");
                return BadRequest("Invalid Language ID.");
            }

            try
            {
                _logger.LogInformation("Fetching Language with ID: {id}", id);
                var entity = await _languageService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("Language not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<LanguageInfoVM>(entity);
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching Language with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a Language by Lg.
        [HttpGet("GetLanguageByLanguage", Name = "GetLanguageByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LanguageOutputVM>> GetLanguageByLg(LanguageFilterVM model)
        {
            var id = model.Id;
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid Language ID received.");
                return BadRequest("Invalid Language ID.");
            }

            try
            {
                _logger.LogInformation("Fetching Language with ID: {id}", id);
                var entity = await _languageService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("Language not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<LanguageOutputVM>(entity, opt => opt.Items.Add(HelperTranslation.KEYLG, model.Lg));
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching Language with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a Languages by Lg.
        [HttpGet("GetLanguagesByLanguage", Name = "GetLanguagesByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<LanguageOutputVM>>> GetLanguagesByLg(string? lg)
        {
            if (string.IsNullOrWhiteSpace(lg))
            {
                _logger.LogWarning("Invalid Language lg received.");
                return BadRequest("Invalid Language lg null ");
            }

            try
            {
                var languages = await _languageService.GetAllAsync();
                if (languages == null)
                {
                    _logger.LogWarning("Languages not found  by  ");
                    return NotFound();
                }

                var items = _mapper.Map<IEnumerable<LanguageOutputVM>>(languages, opt => opt.Items.Add(HelperTranslation.KEYLG, lg));
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching Languages with Lg: {lg}", lg);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create a new Language.
        [HttpPost(Name = "CreateLanguage")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LanguageOutputVM>> Create([FromBody] LanguageCreateVM model)
        {
            if (model == null)
            {
                _logger.LogWarning("Language data is null in Create.");
                return BadRequest("Language data is required.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state in Create: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Creating new Language with data: {@model}", model);
                var item = _mapper.Map<LanguageRequestDso>(model);
                var createdEntity = await _languageService.CreateAsync(item);
                var createdItem = _mapper.Map<LanguageOutputVM>(createdEntity);
                return Ok(createdItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating a new Language");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create multiple Languages.
        [HttpPost("createRange")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<LanguageOutputVM>>> CreateRange([FromBody] IEnumerable<LanguageCreateVM> models)
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
                _logger.LogInformation("Creating multiple Languages.");
                var items = _mapper.Map<List<LanguageRequestDso>>(models);
                var createdEntities = await _languageService.CreateRangeAsync(items);
                var createdItems = _mapper.Map<List<LanguageOutputVM>>(createdEntities);
                return Ok(createdItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating multiple Languages");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Update an existing Language.
        [HttpPut(Name = "UpdateLanguage")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LanguageOutputVM>> Update([FromBody] LanguageUpdateVM model)
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
                _logger.LogInformation("Updating Language with ID: {id}", model?.Id);
                var item = _mapper.Map<LanguageRequestDso>(model);
                var updatedEntity = await _languageService.UpdateAsync(item);
                if (updatedEntity == null)
                {
                    _logger.LogWarning("Language not found for update with ID: {id}", model?.Id);
                    return NotFound();
                }

                var updatedItem = _mapper.Map<LanguageOutputVM>(updatedEntity);
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating Language with ID: {id}", model?.Id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Delete a Language.
        [HttpDelete("{id}", Name = "DeleteLanguage")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid Language ID received in Delete.");
                return BadRequest("Invalid Language ID.");
            }

            try
            {
                _logger.LogInformation("Deleting Language with ID: {id}", id);
                await _languageService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting Language with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get count of Languages.
        [HttpGet("CountLanguage")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Count()
        {
            try
            {
                _logger.LogInformation("Counting Languages...");
                var count = await _languageService.CountAsync();
                return Ok(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while counting Languages");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}