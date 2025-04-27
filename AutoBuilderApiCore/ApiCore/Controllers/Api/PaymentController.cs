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
    public class PaymentController : ControllerBase
    {
        private readonly IUsePaymentService _paymentService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public PaymentController(IUsePaymentService paymentService, IMapper mapper, ILoggerFactory logger)
        {
            _paymentService = paymentService;
            _mapper = mapper;
            _logger = logger.CreateLogger(typeof(PaymentController).FullName);
        }

        // Get all Payments.
        [HttpGet(Name = "GetPayments")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<PaymentOutputVM>>> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all Payments...");
                var result = await _paymentService.GetAllAsync();
                var items = _mapper.Map<List<PaymentOutputVM>>(result);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all Payments");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get a Payment by ID.
        [HttpGet("{id}", Name = "GetPayment")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PaymentInfoVM>> GetById(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid Payment ID received.");
                return BadRequest("Invalid Payment ID.");
            }

            try
            {
                _logger.LogInformation("Fetching Payment with ID: {id}", id);
                var entity = await _paymentService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("Payment not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<PaymentInfoVM>(entity);
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching Payment with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a Payment by Lg.
        [HttpGet("GetPaymentByLanguage", Name = "GetPaymentByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PaymentOutputVM>> GetPaymentByLg(PaymentFilterVM model)
        {
            var id = model.Id;
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid Payment ID received.");
                return BadRequest("Invalid Payment ID.");
            }

            try
            {
                _logger.LogInformation("Fetching Payment with ID: {id}", id);
                var entity = await _paymentService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("Payment not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<PaymentOutputVM>(entity, opt => opt.Items.Add(HelperTranslation.KEYLG, model.Lg));
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching Payment with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a Payments by Lg.
        [HttpGet("GetPaymentsByLanguage", Name = "GetPaymentsByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<PaymentOutputVM>>> GetPaymentsByLg(string? lg)
        {
            if (string.IsNullOrWhiteSpace(lg))
            {
                _logger.LogWarning("Invalid Payment lg received.");
                return BadRequest("Invalid Payment lg null ");
            }

            try
            {
                var payments = await _paymentService.GetAllAsync();
                if (payments == null)
                {
                    _logger.LogWarning("Payments not found  by  ");
                    return NotFound();
                }

                var items = _mapper.Map<IEnumerable<PaymentOutputVM>>(payments, opt => opt.Items.Add(HelperTranslation.KEYLG, lg));
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching Payments with Lg: {lg}", lg);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create a new Payment.
        [HttpPost(Name = "CreatePayment")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PaymentOutputVM>> Create([FromBody] PaymentCreateVM model)
        {
            if (model == null)
            {
                _logger.LogWarning("Payment data is null in Create.");
                return BadRequest("Payment data is required.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state in Create: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Creating new Payment with data: {@model}", model);
                var item = _mapper.Map<PaymentRequestDso>(model);
                var createdEntity = await _paymentService.CreateAsync(item);
                var createdItem = _mapper.Map<PaymentOutputVM>(createdEntity);
                return Ok(createdItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating a new Payment");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create multiple Payments.
        [HttpPost("createRange")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<PaymentOutputVM>>> CreateRange([FromBody] IEnumerable<PaymentCreateVM> models)
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
                _logger.LogInformation("Creating multiple Payments.");
                var items = _mapper.Map<List<PaymentRequestDso>>(models);
                var createdEntities = await _paymentService.CreateRangeAsync(items);
                var createdItems = _mapper.Map<List<PaymentOutputVM>>(createdEntities);
                return Ok(createdItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating multiple Payments");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Update an existing Payment.
        [HttpPut(Name = "UpdatePayment")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PaymentOutputVM>> Update([FromBody] PaymentUpdateVM model)
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
                _logger.LogInformation("Updating Payment with ID: {id}", model?.Id);
                var item = _mapper.Map<PaymentRequestDso>(model);
                var updatedEntity = await _paymentService.UpdateAsync(item);
                if (updatedEntity == null)
                {
                    _logger.LogWarning("Payment not found for update with ID: {id}", model?.Id);
                    return NotFound();
                }

                var updatedItem = _mapper.Map<PaymentOutputVM>(updatedEntity);
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating Payment with ID: {id}", model?.Id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Delete a Payment.
        [HttpDelete("{id}", Name = "DeletePayment")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid Payment ID received in Delete.");
                return BadRequest("Invalid Payment ID.");
            }

            try
            {
                _logger.LogInformation("Deleting Payment with ID: {id}", id);
                await _paymentService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting Payment with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get count of Payments.
        [HttpGet("CountPayment")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Count()
        {
            try
            {
                _logger.LogInformation("Counting Payments...");
                var count = await _paymentService.CountAsync();
                return Ok(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while counting Payments");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}