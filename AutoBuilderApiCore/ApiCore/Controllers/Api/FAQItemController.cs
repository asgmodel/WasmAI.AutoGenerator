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
    public class FAQItemController : ControllerBase
    {
        private readonly IUseFAQItemService _faqitemService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public FAQItemController(IUseFAQItemService faqitemService, IMapper mapper, ILoggerFactory logger)
        {
            _faqitemService = faqitemService;
            _mapper = mapper;
            _logger = logger.CreateLogger(typeof(FAQItemController).FullName);
        }

        // Get all FAQItems.
        [HttpGet(Name = "GetFAQItems")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<FAQItemOutputVM>>> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all FAQItems...");
                var result = await _faqitemService.GetAllAsync();
                var items = _mapper.Map<List<FAQItemOutputVM>>(result);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all FAQItems");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get a FAQItem by ID.
        [HttpGet("{id}", Name = "GetFAQItem")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FAQItemInfoVM>> GetById(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid FAQItem ID received.");
                return BadRequest("Invalid FAQItem ID.");
            }

            try
            {
                _logger.LogInformation("Fetching FAQItem with ID: {id}", id);
                var entity = await _faqitemService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("FAQItem not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<FAQItemInfoVM>(entity);
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching FAQItem with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a FAQItem by Lg.
        [HttpGet("GetFAQItemByLanguage", Name = "GetFAQItemByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FAQItemOutputVM>> GetFAQItemByLg(FAQItemFilterVM model)
        {
            var id = model.Id;
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid FAQItem ID received.");
                return BadRequest("Invalid FAQItem ID.");
            }

            try
            {
                _logger.LogInformation("Fetching FAQItem with ID: {id}", id);
                var entity = await _faqitemService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("FAQItem not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<FAQItemOutputVM>(entity, opt => opt.Items.Add(HelperTranslation.KEYLG, model.Lg));
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching FAQItem with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a FAQItems by Lg.
        [HttpGet("GetFAQItemsByLanguage", Name = "GetFAQItemsByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<FAQItemOutputVM>>> GetFAQItemsByLg(string? lg)
        {
            if (string.IsNullOrWhiteSpace(lg))
            {
                _logger.LogWarning("Invalid FAQItem lg received.");
                return BadRequest("Invalid FAQItem lg null ");
            }

            try
            {
                var faqitems = await _faqitemService.GetAllAsync();
                if (faqitems == null)
                {
                    _logger.LogWarning("FAQItems not found  by  ");
                    return NotFound();
                }

                var items = _mapper.Map<IEnumerable<FAQItemOutputVM>>(faqitems, opt => opt.Items.Add(HelperTranslation.KEYLG, lg));
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching FAQItems with Lg: {lg}", lg);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create a new FAQItem.
        [HttpPost(Name = "CreateFAQItem")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FAQItemOutputVM>> Create([FromBody] FAQItemCreateVM model)
        {
            if (model == null)
            {
                _logger.LogWarning("FAQItem data is null in Create.");
                return BadRequest("FAQItem data is required.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state in Create: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Creating new FAQItem with data: {@model}", model);
                var item = _mapper.Map<FAQItemRequestDso>(model);
                var createdEntity = await _faqitemService.CreateAsync(item);
                var createdItem = _mapper.Map<FAQItemOutputVM>(createdEntity);
                return Ok(createdItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating a new FAQItem");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create multiple FAQItems.
        [HttpPost("createRange")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<FAQItemOutputVM>>> CreateRange([FromBody] IEnumerable<FAQItemCreateVM> models)
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
                _logger.LogInformation("Creating multiple FAQItems.");
                var items = _mapper.Map<List<FAQItemRequestDso>>(models);
                var createdEntities = await _faqitemService.CreateRangeAsync(items);
                var createdItems = _mapper.Map<List<FAQItemOutputVM>>(createdEntities);
                return Ok(createdItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating multiple FAQItems");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Update an existing FAQItem.
        [HttpPut(Name = "UpdateFAQItem")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FAQItemOutputVM>> Update([FromBody] FAQItemUpdateVM model)
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
                _logger.LogInformation("Updating FAQItem with ID: {id}", model?.Id);
                var item = _mapper.Map<FAQItemRequestDso>(model);
                var updatedEntity = await _faqitemService.UpdateAsync(item);
                if (updatedEntity == null)
                {
                    _logger.LogWarning("FAQItem not found for update with ID: {id}", model?.Id);
                    return NotFound();
                }

                var updatedItem = _mapper.Map<FAQItemOutputVM>(updatedEntity);
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating FAQItem with ID: {id}", model?.Id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Delete a FAQItem.
        [HttpDelete("{id}", Name = "DeleteFAQItem")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid FAQItem ID received in Delete.");
                return BadRequest("Invalid FAQItem ID.");
            }

            try
            {
                _logger.LogInformation("Deleting FAQItem with ID: {id}", id);
                await _faqitemService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting FAQItem with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get count of FAQItems.
        [HttpGet("CountFAQItem")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Count()
        {
            try
            {
                _logger.LogInformation("Counting FAQItems...");
                var count = await _faqitemService.CountAsync();
                return Ok(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while counting FAQItems");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}