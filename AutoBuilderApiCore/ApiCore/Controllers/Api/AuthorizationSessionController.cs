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
    public class AuthorizationSessionController : ControllerBase
    {
        private readonly IUseAuthorizationSessionService _authorizationsessionService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public AuthorizationSessionController(IUseAuthorizationSessionService authorizationsessionService, IMapper mapper, ILoggerFactory logger)
        {
            _authorizationsessionService = authorizationsessionService;
            _mapper = mapper;
            _logger = logger.CreateLogger(typeof(AuthorizationSessionController).FullName);
        }

        // Get all AuthorizationSessions.
        [HttpGet(Name = "GetAuthorizationSessions")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<AuthorizationSessionOutputVM>>> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all AuthorizationSessions...");
                var result = await _authorizationsessionService.GetAllAsync();
                var items = _mapper.Map<List<AuthorizationSessionOutputVM>>(result);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all AuthorizationSessions");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get a AuthorizationSession by ID.
        [HttpGet("{id}", Name = "GetAuthorizationSession")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AuthorizationSessionInfoVM>> GetById(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid AuthorizationSession ID received.");
                return BadRequest("Invalid AuthorizationSession ID.");
            }

            try
            {
                _logger.LogInformation("Fetching AuthorizationSession with ID: {id}", id);
                var entity = await _authorizationsessionService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("AuthorizationSession not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<AuthorizationSessionInfoVM>(entity);
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching AuthorizationSession with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a AuthorizationSession by Lg.
        [HttpGet("GetAuthorizationSessionByLanguage", Name = "GetAuthorizationSessionByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AuthorizationSessionOutputVM>> GetAuthorizationSessionByLg(AuthorizationSessionFilterVM model)
        {
            var id = model.Id;
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid AuthorizationSession ID received.");
                return BadRequest("Invalid AuthorizationSession ID.");
            }

            try
            {
                _logger.LogInformation("Fetching AuthorizationSession with ID: {id}", id);
                var entity = await _authorizationsessionService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("AuthorizationSession not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<AuthorizationSessionOutputVM>(entity, opt => opt.Items.Add(HelperTranslation.KEYLG, model.Lg));
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching AuthorizationSession with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a AuthorizationSessions by Lg.
        [HttpGet("GetAuthorizationSessionsByLanguage", Name = "GetAuthorizationSessionsByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<AuthorizationSessionOutputVM>>> GetAuthorizationSessionsByLg(string? lg)
        {
            if (string.IsNullOrWhiteSpace(lg))
            {
                _logger.LogWarning("Invalid AuthorizationSession lg received.");
                return BadRequest("Invalid AuthorizationSession lg null ");
            }

            try
            {
                var authorizationsessions = await _authorizationsessionService.GetAllAsync();
                if (authorizationsessions == null)
                {
                    _logger.LogWarning("AuthorizationSessions not found  by  ");
                    return NotFound();
                }

                var items = _mapper.Map<IEnumerable<AuthorizationSessionOutputVM>>(authorizationsessions, opt => opt.Items.Add(HelperTranslation.KEYLG, lg));
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching AuthorizationSessions with Lg: {lg}", lg);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create a new AuthorizationSession.
        [HttpPost(Name = "CreateAuthorizationSession")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AuthorizationSessionOutputVM>> Create([FromBody] AuthorizationSessionCreateVM model)
        {
            if (model == null)
            {
                _logger.LogWarning("AuthorizationSession data is null in Create.");
                return BadRequest("AuthorizationSession data is required.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state in Create: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Creating new AuthorizationSession with data: {@model}", model);
                var item = _mapper.Map<AuthorizationSessionRequestDso>(model);
                var createdEntity = await _authorizationsessionService.CreateAsync(item);
                var createdItem = _mapper.Map<AuthorizationSessionOutputVM>(createdEntity);
                return Ok(createdItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating a new AuthorizationSession");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create multiple AuthorizationSessions.
        [HttpPost("createRange")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<AuthorizationSessionOutputVM>>> CreateRange([FromBody] IEnumerable<AuthorizationSessionCreateVM> models)
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
                _logger.LogInformation("Creating multiple AuthorizationSessions.");
                var items = _mapper.Map<List<AuthorizationSessionRequestDso>>(models);
                var createdEntities = await _authorizationsessionService.CreateRangeAsync(items);
                var createdItems = _mapper.Map<List<AuthorizationSessionOutputVM>>(createdEntities);
                return Ok(createdItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating multiple AuthorizationSessions");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Update an existing AuthorizationSession.
        [HttpPut(Name = "UpdateAuthorizationSession")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AuthorizationSessionOutputVM>> Update([FromBody] AuthorizationSessionUpdateVM model)
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
                _logger.LogInformation("Updating AuthorizationSession with ID: {id}", model?.Id);
                var item = _mapper.Map<AuthorizationSessionRequestDso>(model);
                var updatedEntity = await _authorizationsessionService.UpdateAsync(item);
                if (updatedEntity == null)
                {
                    _logger.LogWarning("AuthorizationSession not found for update with ID: {id}", model?.Id);
                    return NotFound();
                }

                var updatedItem = _mapper.Map<AuthorizationSessionOutputVM>(updatedEntity);
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating AuthorizationSession with ID: {id}", model?.Id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Delete a AuthorizationSession.
        [HttpDelete("{id}", Name = "DeleteAuthorizationSession")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid AuthorizationSession ID received in Delete.");
                return BadRequest("Invalid AuthorizationSession ID.");
            }

            try
            {
                _logger.LogInformation("Deleting AuthorizationSession with ID: {id}", id);
                await _authorizationsessionService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting AuthorizationSession with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get count of AuthorizationSessions.
        [HttpGet("CountAuthorizationSession")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Count()
        {
            try
            {
                _logger.LogInformation("Counting AuthorizationSessions...");
                var count = await _authorizationsessionService.CountAsync();
                return Ok(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while counting AuthorizationSessions");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}