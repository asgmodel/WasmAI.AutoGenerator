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
    public class UserServiceController : ControllerBase
    {
        private readonly IUseUserServiceService _userserviceService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public UserServiceController(IUseUserServiceService userserviceService, IMapper mapper, ILoggerFactory logger)
        {
            _userserviceService = userserviceService;
            _mapper = mapper;
            _logger = logger.CreateLogger(typeof(UserServiceController).FullName);
        }

        // Get all UserServices.
        [HttpGet(Name = "GetUserServices")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<UserServiceOutputVM>>> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all UserServices...");
                var result = await _userserviceService.GetAllAsync();
                var items = _mapper.Map<List<UserServiceOutputVM>>(result);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all UserServices");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get a UserService by ID.
        [HttpGet("{id}", Name = "GetUserService")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserServiceInfoVM>> GetById(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid UserService ID received.");
                return BadRequest("Invalid UserService ID.");
            }

            try
            {
                _logger.LogInformation("Fetching UserService with ID: {id}", id);
                var entity = await _userserviceService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("UserService not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<UserServiceInfoVM>(entity);
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching UserService with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a UserService by Lg.
        [HttpGet("GetUserServiceByLanguage", Name = "GetUserServiceByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserServiceOutputVM>> GetUserServiceByLg(UserServiceFilterVM model)
        {
            var id = model.Id;
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid UserService ID received.");
                return BadRequest("Invalid UserService ID.");
            }

            try
            {
                _logger.LogInformation("Fetching UserService with ID: {id}", id);
                var entity = await _userserviceService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("UserService not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<UserServiceOutputVM>(entity, opt => opt.Items.Add(HelperTranslation.KEYLG, model.Lg));
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching UserService with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a UserServices by Lg.
        [HttpGet("GetUserServicesByLanguage", Name = "GetUserServicesByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<UserServiceOutputVM>>> GetUserServicesByLg(string? lg)
        {
            if (string.IsNullOrWhiteSpace(lg))
            {
                _logger.LogWarning("Invalid UserService lg received.");
                return BadRequest("Invalid UserService lg null ");
            }

            try
            {
                var userservices = await _userserviceService.GetAllAsync();
                if (userservices == null)
                {
                    _logger.LogWarning("UserServices not found  by  ");
                    return NotFound();
                }

                var items = _mapper.Map<IEnumerable<UserServiceOutputVM>>(userservices, opt => opt.Items.Add(HelperTranslation.KEYLG, lg));
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching UserServices with Lg: {lg}", lg);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create a new UserService.
        [HttpPost(Name = "CreateUserService")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserServiceOutputVM>> Create([FromBody] UserServiceCreateVM model)
        {
            if (model == null)
            {
                _logger.LogWarning("UserService data is null in Create.");
                return BadRequest("UserService data is required.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state in Create: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Creating new UserService with data: {@model}", model);
                var item = _mapper.Map<UserServiceRequestDso>(model);
                var createdEntity = await _userserviceService.CreateAsync(item);
                var createdItem = _mapper.Map<UserServiceOutputVM>(createdEntity);
                return Ok(createdItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating a new UserService");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create multiple UserServices.
        [HttpPost("createRange")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<UserServiceOutputVM>>> CreateRange([FromBody] IEnumerable<UserServiceCreateVM> models)
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
                _logger.LogInformation("Creating multiple UserServices.");
                var items = _mapper.Map<List<UserServiceRequestDso>>(models);
                var createdEntities = await _userserviceService.CreateRangeAsync(items);
                var createdItems = _mapper.Map<List<UserServiceOutputVM>>(createdEntities);
                return Ok(createdItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating multiple UserServices");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Update an existing UserService.
        [HttpPut(Name = "UpdateUserService")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserServiceOutputVM>> Update([FromBody] UserServiceUpdateVM model)
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
                _logger.LogInformation("Updating UserService with ID: {id}", model?.Id);
                var item = _mapper.Map<UserServiceRequestDso>(model);
                var updatedEntity = await _userserviceService.UpdateAsync(item);
                if (updatedEntity == null)
                {
                    _logger.LogWarning("UserService not found for update with ID: {id}", model?.Id);
                    return NotFound();
                }

                var updatedItem = _mapper.Map<UserServiceOutputVM>(updatedEntity);
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating UserService with ID: {id}", model?.Id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Delete a UserService.
        [HttpDelete("{id}", Name = "DeleteUserService")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid UserService ID received in Delete.");
                return BadRequest("Invalid UserService ID.");
            }

            try
            {
                _logger.LogInformation("Deleting UserService with ID: {id}", id);
                await _userserviceService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting UserService with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get count of UserServices.
        [HttpGet("CountUserService")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Count()
        {
            try
            {
                _logger.LogInformation("Counting UserServices...");
                var count = await _userserviceService.CountAsync();
                return Ok(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while counting UserServices");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}