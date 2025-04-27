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
    public class CategoryTabController : ControllerBase
    {
        private readonly IUseCategoryTabService _categorytabService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public CategoryTabController(IUseCategoryTabService categorytabService, IMapper mapper, ILoggerFactory logger)
        {
            _categorytabService = categorytabService;
            _mapper = mapper;
            _logger = logger.CreateLogger(typeof(CategoryTabController).FullName);
        }

        // Get all CategoryTabs.
        [HttpGet(Name = "GetCategoryTabs")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CategoryTabOutputVM>>> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all CategoryTabs...");
                var result = await _categorytabService.GetAllAsync();
                var items = _mapper.Map<List<CategoryTabOutputVM>>(result);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all CategoryTabs");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get a CategoryTab by ID.
        [HttpGet("{id}", Name = "GetCategoryTab")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryTabInfoVM>> GetById(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid CategoryTab ID received.");
                return BadRequest("Invalid CategoryTab ID.");
            }

            try
            {
                _logger.LogInformation("Fetching CategoryTab with ID: {id}", id);
                var entity = await _categorytabService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("CategoryTab not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<CategoryTabInfoVM>(entity);
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching CategoryTab with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a CategoryTab by Lg.
        [HttpGet("GetCategoryTabByLanguage", Name = "GetCategoryTabByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryTabOutputVM>> GetCategoryTabByLg(CategoryTabFilterVM model)
        {
            var id = model.Id;
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid CategoryTab ID received.");
                return BadRequest("Invalid CategoryTab ID.");
            }

            try
            {
                _logger.LogInformation("Fetching CategoryTab with ID: {id}", id);
                var entity = await _categorytabService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("CategoryTab not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<CategoryTabOutputVM>(entity, opt => opt.Items.Add(HelperTranslation.KEYLG, model.Lg));
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching CategoryTab with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a CategoryTabs by Lg.
        [HttpGet("GetCategoryTabsByLanguage", Name = "GetCategoryTabsByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CategoryTabOutputVM>>> GetCategoryTabsByLg(string? lg)
        {
            if (string.IsNullOrWhiteSpace(lg))
            {
                _logger.LogWarning("Invalid CategoryTab lg received.");
                return BadRequest("Invalid CategoryTab lg null ");
            }

            try
            {
                var categorytabs = await _categorytabService.GetAllAsync();
                if (categorytabs == null)
                {
                    _logger.LogWarning("CategoryTabs not found  by  ");
                    return NotFound();
                }

                var items = _mapper.Map<IEnumerable<CategoryTabOutputVM>>(categorytabs, opt => opt.Items.Add(HelperTranslation.KEYLG, lg));
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching CategoryTabs with Lg: {lg}", lg);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create a new CategoryTab.
        [HttpPost(Name = "CreateCategoryTab")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryTabOutputVM>> Create([FromBody] CategoryTabCreateVM model)
        {
            if (model == null)
            {
                _logger.LogWarning("CategoryTab data is null in Create.");
                return BadRequest("CategoryTab data is required.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state in Create: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Creating new CategoryTab with data: {@model}", model);
                var item = _mapper.Map<CategoryTabRequestDso>(model);
                var createdEntity = await _categorytabService.CreateAsync(item);
                var createdItem = _mapper.Map<CategoryTabOutputVM>(createdEntity);
                return Ok(createdItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating a new CategoryTab");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create multiple CategoryTabs.
        [HttpPost("createRange")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CategoryTabOutputVM>>> CreateRange([FromBody] IEnumerable<CategoryTabCreateVM> models)
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
                _logger.LogInformation("Creating multiple CategoryTabs.");
                var items = _mapper.Map<List<CategoryTabRequestDso>>(models);
                var createdEntities = await _categorytabService.CreateRangeAsync(items);
                var createdItems = _mapper.Map<List<CategoryTabOutputVM>>(createdEntities);
                return Ok(createdItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating multiple CategoryTabs");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Update an existing CategoryTab.
        [HttpPut(Name = "UpdateCategoryTab")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryTabOutputVM>> Update([FromBody] CategoryTabUpdateVM model)
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
                _logger.LogInformation("Updating CategoryTab with ID: {id}", model?.Id);
                var item = _mapper.Map<CategoryTabRequestDso>(model);
                var updatedEntity = await _categorytabService.UpdateAsync(item);
                if (updatedEntity == null)
                {
                    _logger.LogWarning("CategoryTab not found for update with ID: {id}", model?.Id);
                    return NotFound();
                }

                var updatedItem = _mapper.Map<CategoryTabOutputVM>(updatedEntity);
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating CategoryTab with ID: {id}", model?.Id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Delete a CategoryTab.
        [HttpDelete("{id}", Name = "DeleteCategoryTab")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid CategoryTab ID received in Delete.");
                return BadRequest("Invalid CategoryTab ID.");
            }

            try
            {
                _logger.LogInformation("Deleting CategoryTab with ID: {id}", id);
                await _categorytabService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting CategoryTab with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get count of CategoryTabs.
        [HttpGet("CountCategoryTab")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Count()
        {
            try
            {
                _logger.LogInformation("Counting CategoryTabs...");
                var count = await _categorytabService.CountAsync();
                return Ok(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while counting CategoryTabs");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}