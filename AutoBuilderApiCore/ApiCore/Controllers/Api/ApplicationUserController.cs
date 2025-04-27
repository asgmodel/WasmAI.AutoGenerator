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
    public class ApplicationUserController : ControllerBase
    {
        private readonly IUseApplicationUserService _applicationuserService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public ApplicationUserController(IUseApplicationUserService applicationuserService, IMapper mapper, ILoggerFactory logger)
        {
            _applicationuserService = applicationuserService;
            _mapper = mapper;
            _logger = logger.CreateLogger(typeof(ApplicationUserController).FullName);
        }

        // Get all ApplicationUsers.
        [HttpGet(Name = "GetApplicationUsers")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ApplicationUserOutputVM>>> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all ApplicationUsers...");
                var result = await _applicationuserService.GetAllAsync();
                var items = _mapper.Map<List<ApplicationUserOutputVM>>(result);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all ApplicationUsers");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get a ApplicationUser by ID.
        [HttpGet("{id}", Name = "GetApplicationUser")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApplicationUserInfoVM>> GetById(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid ApplicationUser ID received.");
                return BadRequest("Invalid ApplicationUser ID.");
            }

            try
            {
                _logger.LogInformation("Fetching ApplicationUser with ID: {id}", id);
                var entity = await _applicationuserService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("ApplicationUser not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<ApplicationUserInfoVM>(entity);
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching ApplicationUser with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a ApplicationUser by Lg.
        [HttpGet("GetApplicationUserByLanguage", Name = "GetApplicationUserByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApplicationUserOutputVM>> GetApplicationUserByLg(ApplicationUserFilterVM model)
        {
            var id = model.Id;
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid ApplicationUser ID received.");
                return BadRequest("Invalid ApplicationUser ID.");
            }

            try
            {
                _logger.LogInformation("Fetching ApplicationUser with ID: {id}", id);
                var entity = await _applicationuserService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("ApplicationUser not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<ApplicationUserOutputVM>(entity, opt => opt.Items.Add(HelperTranslation.KEYLG, model.Lg));
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching ApplicationUser with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a ApplicationUsers by Lg.
        [HttpGet("GetApplicationUsersByLanguage", Name = "GetApplicationUsersByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ApplicationUserOutputVM>>> GetApplicationUsersByLg(string? lg)
        {
            if (string.IsNullOrWhiteSpace(lg))
            {
                _logger.LogWarning("Invalid ApplicationUser lg received.");
                return BadRequest("Invalid ApplicationUser lg null ");
            }

            try
            {
                var applicationusers = await _applicationuserService.GetAllAsync();
                if (applicationusers == null)
                {
                    _logger.LogWarning("ApplicationUsers not found  by  ");
                    return NotFound();
                }

                var items = _mapper.Map<IEnumerable<ApplicationUserOutputVM>>(applicationusers, opt => opt.Items.Add(HelperTranslation.KEYLG, lg));
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching ApplicationUsers with Lg: {lg}", lg);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create a new ApplicationUser.
        [HttpPost(Name = "CreateApplicationUser")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApplicationUserOutputVM>> Create([FromBody] ApplicationUserCreateVM model)
        {
            if (model == null)
            {
                _logger.LogWarning("ApplicationUser data is null in Create.");
                return BadRequest("ApplicationUser data is required.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state in Create: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Creating new ApplicationUser with data: {@model}", model);
                var item = _mapper.Map<ApplicationUserRequestDso>(model);
                var createdEntity = await _applicationuserService.CreateAsync(item);
                var createdItem = _mapper.Map<ApplicationUserOutputVM>(createdEntity);
                return Ok(createdItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating a new ApplicationUser");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create multiple ApplicationUsers.
        [HttpPost("createRange")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ApplicationUserOutputVM>>> CreateRange([FromBody] IEnumerable<ApplicationUserCreateVM> models)
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
                _logger.LogInformation("Creating multiple ApplicationUsers.");
                var items = _mapper.Map<List<ApplicationUserRequestDso>>(models);
                var createdEntities = await _applicationuserService.CreateRangeAsync(items);
                var createdItems = _mapper.Map<List<ApplicationUserOutputVM>>(createdEntities);
                return Ok(createdItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating multiple ApplicationUsers");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Update an existing ApplicationUser.
        [HttpPut(Name = "UpdateApplicationUser")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApplicationUserOutputVM>> Update([FromBody] ApplicationUserUpdateVM model)
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
                _logger.LogInformation("Updating ApplicationUser with ID: {id}", model?.Id);
                var item = _mapper.Map<ApplicationUserRequestDso>(model);
                var updatedEntity = await _applicationuserService.UpdateAsync(item);
                if (updatedEntity == null)
                {
                    _logger.LogWarning("ApplicationUser not found for update with ID: {id}", model?.Id);
                    return NotFound();
                }

                var updatedItem = _mapper.Map<ApplicationUserOutputVM>(updatedEntity);
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating ApplicationUser with ID: {id}", model?.Id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Delete a ApplicationUser.
        [HttpDelete("{id}", Name = "DeleteApplicationUser")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid ApplicationUser ID received in Delete.");
                return BadRequest("Invalid ApplicationUser ID.");
            }

            try
            {
                _logger.LogInformation("Deleting ApplicationUser with ID: {id}", id);
                await _applicationuserService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting ApplicationUser with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get count of ApplicationUsers.
        [HttpGet("CountApplicationUser")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Count()
        {
            try
            {
                _logger.LogInformation("Counting ApplicationUsers...");
                var count = await _applicationuserService.CountAsync();
                return Ok(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while counting ApplicationUsers");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}