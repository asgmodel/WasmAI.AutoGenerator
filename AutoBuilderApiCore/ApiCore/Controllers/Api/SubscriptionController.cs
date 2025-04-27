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
    public class SubscriptionController : ControllerBase
    {
        private readonly IUseSubscriptionService _subscriptionService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public SubscriptionController(IUseSubscriptionService subscriptionService, IMapper mapper, ILoggerFactory logger)
        {
            _subscriptionService = subscriptionService;
            _mapper = mapper;
            _logger = logger.CreateLogger(typeof(SubscriptionController).FullName);
        }

        // Get all Subscriptions.
        [HttpGet(Name = "GetSubscriptions")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<SubscriptionOutputVM>>> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all Subscriptions...");
                var result = await _subscriptionService.GetAllAsync();
                var items = _mapper.Map<List<SubscriptionOutputVM>>(result);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all Subscriptions");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get a Subscription by ID.
        [HttpGet("{id}", Name = "GetSubscription")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SubscriptionInfoVM>> GetById(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid Subscription ID received.");
                return BadRequest("Invalid Subscription ID.");
            }

            try
            {
                _logger.LogInformation("Fetching Subscription with ID: {id}", id);
                var entity = await _subscriptionService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("Subscription not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<SubscriptionInfoVM>(entity);
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching Subscription with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a Subscription by Lg.
        [HttpGet("GetSubscriptionByLanguage", Name = "GetSubscriptionByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SubscriptionOutputVM>> GetSubscriptionByLg(SubscriptionFilterVM model)
        {
            var id = model.Id;
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid Subscription ID received.");
                return BadRequest("Invalid Subscription ID.");
            }

            try
            {
                _logger.LogInformation("Fetching Subscription with ID: {id}", id);
                var entity = await _subscriptionService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("Subscription not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<SubscriptionOutputVM>(entity, opt => opt.Items.Add(HelperTranslation.KEYLG, model.Lg));
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching Subscription with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a Subscriptions by Lg.
        [HttpGet("GetSubscriptionsByLanguage", Name = "GetSubscriptionsByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<SubscriptionOutputVM>>> GetSubscriptionsByLg(string? lg)
        {
            if (string.IsNullOrWhiteSpace(lg))
            {
                _logger.LogWarning("Invalid Subscription lg received.");
                return BadRequest("Invalid Subscription lg null ");
            }

            try
            {
                var subscriptions = await _subscriptionService.GetAllAsync();
                if (subscriptions == null)
                {
                    _logger.LogWarning("Subscriptions not found  by  ");
                    return NotFound();
                }

                var items = _mapper.Map<IEnumerable<SubscriptionOutputVM>>(subscriptions, opt => opt.Items.Add(HelperTranslation.KEYLG, lg));
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching Subscriptions with Lg: {lg}", lg);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create a new Subscription.
        [HttpPost(Name = "CreateSubscription")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SubscriptionOutputVM>> Create([FromBody] SubscriptionCreateVM model)
        {
            if (model == null)
            {
                _logger.LogWarning("Subscription data is null in Create.");
                return BadRequest("Subscription data is required.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state in Create: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Creating new Subscription with data: {@model}", model);
                var item = _mapper.Map<SubscriptionRequestDso>(model);
                var createdEntity = await _subscriptionService.CreateAsync(item);
                var createdItem = _mapper.Map<SubscriptionOutputVM>(createdEntity);
                return Ok(createdItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating a new Subscription");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create multiple Subscriptions.
        [HttpPost("createRange")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<SubscriptionOutputVM>>> CreateRange([FromBody] IEnumerable<SubscriptionCreateVM> models)
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
                _logger.LogInformation("Creating multiple Subscriptions.");
                var items = _mapper.Map<List<SubscriptionRequestDso>>(models);
                var createdEntities = await _subscriptionService.CreateRangeAsync(items);
                var createdItems = _mapper.Map<List<SubscriptionOutputVM>>(createdEntities);
                return Ok(createdItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating multiple Subscriptions");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Update an existing Subscription.
        [HttpPut(Name = "UpdateSubscription")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SubscriptionOutputVM>> Update([FromBody] SubscriptionUpdateVM model)
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
                _logger.LogInformation("Updating Subscription with ID: {id}", model?.Id);
                var item = _mapper.Map<SubscriptionRequestDso>(model);
                var updatedEntity = await _subscriptionService.UpdateAsync(item);
                if (updatedEntity == null)
                {
                    _logger.LogWarning("Subscription not found for update with ID: {id}", model?.Id);
                    return NotFound();
                }

                var updatedItem = _mapper.Map<SubscriptionOutputVM>(updatedEntity);
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating Subscription with ID: {id}", model?.Id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Delete a Subscription.
        [HttpDelete("{id}", Name = "DeleteSubscription")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid Subscription ID received in Delete.");
                return BadRequest("Invalid Subscription ID.");
            }

            try
            {
                _logger.LogInformation("Deleting Subscription with ID: {id}", id);
                await _subscriptionService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting Subscription with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get count of Subscriptions.
        [HttpGet("CountSubscription")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Count()
        {
            try
            {
                _logger.LogInformation("Counting Subscriptions...");
                var count = await _subscriptionService.CountAsync();
                return Ok(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while counting Subscriptions");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}