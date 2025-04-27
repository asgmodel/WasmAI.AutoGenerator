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
    public class InvoiceController : ControllerBase
    {
        private readonly IUseInvoiceService _invoiceService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public InvoiceController(IUseInvoiceService invoiceService, IMapper mapper, ILoggerFactory logger)
        {
            _invoiceService = invoiceService;
            _mapper = mapper;
            _logger = logger.CreateLogger(typeof(InvoiceController).FullName);
        }

        // Get all Invoices.
        [HttpGet(Name = "GetInvoices")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<InvoiceOutputVM>>> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all Invoices...");
                var result = await _invoiceService.GetAllAsync();
                var items = _mapper.Map<List<InvoiceOutputVM>>(result);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all Invoices");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get a Invoice by ID.
        [HttpGet("{id}", Name = "GetInvoice")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<InvoiceInfoVM>> GetById(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid Invoice ID received.");
                return BadRequest("Invalid Invoice ID.");
            }

            try
            {
                _logger.LogInformation("Fetching Invoice with ID: {id}", id);
                var entity = await _invoiceService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("Invoice not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<InvoiceInfoVM>(entity);
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching Invoice with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a Invoice by Lg.
        [HttpGet("GetInvoiceByLanguage", Name = "GetInvoiceByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<InvoiceOutputVM>> GetInvoiceByLg(InvoiceFilterVM model)
        {
            var id = model.Id;
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid Invoice ID received.");
                return BadRequest("Invalid Invoice ID.");
            }

            try
            {
                _logger.LogInformation("Fetching Invoice with ID: {id}", id);
                var entity = await _invoiceService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("Invoice not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<InvoiceOutputVM>(entity, opt => opt.Items.Add(HelperTranslation.KEYLG, model.Lg));
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching Invoice with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a Invoices by Lg.
        [HttpGet("GetInvoicesByLanguage", Name = "GetInvoicesByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<InvoiceOutputVM>>> GetInvoicesByLg(string? lg)
        {
            if (string.IsNullOrWhiteSpace(lg))
            {
                _logger.LogWarning("Invalid Invoice lg received.");
                return BadRequest("Invalid Invoice lg null ");
            }

            try
            {
                var invoices = await _invoiceService.GetAllAsync();
                if (invoices == null)
                {
                    _logger.LogWarning("Invoices not found  by  ");
                    return NotFound();
                }

                var items = _mapper.Map<IEnumerable<InvoiceOutputVM>>(invoices, opt => opt.Items.Add(HelperTranslation.KEYLG, lg));
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching Invoices with Lg: {lg}", lg);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create a new Invoice.
        [HttpPost(Name = "CreateInvoice")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<InvoiceOutputVM>> Create([FromBody] InvoiceCreateVM model)
        {
            if (model == null)
            {
                _logger.LogWarning("Invoice data is null in Create.");
                return BadRequest("Invoice data is required.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state in Create: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Creating new Invoice with data: {@model}", model);
                var item = _mapper.Map<InvoiceRequestDso>(model);
                var createdEntity = await _invoiceService.CreateAsync(item);
                var createdItem = _mapper.Map<InvoiceOutputVM>(createdEntity);
                return Ok(createdItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating a new Invoice");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create multiple Invoices.
        [HttpPost("createRange")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<InvoiceOutputVM>>> CreateRange([FromBody] IEnumerable<InvoiceCreateVM> models)
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
                _logger.LogInformation("Creating multiple Invoices.");
                var items = _mapper.Map<List<InvoiceRequestDso>>(models);
                var createdEntities = await _invoiceService.CreateRangeAsync(items);
                var createdItems = _mapper.Map<List<InvoiceOutputVM>>(createdEntities);
                return Ok(createdItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating multiple Invoices");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Update an existing Invoice.
        [HttpPut(Name = "UpdateInvoice")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<InvoiceOutputVM>> Update([FromBody] InvoiceUpdateVM model)
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
                _logger.LogInformation("Updating Invoice with ID: {id}", model?.Id);
                var item = _mapper.Map<InvoiceRequestDso>(model);
                var updatedEntity = await _invoiceService.UpdateAsync(item);
                if (updatedEntity == null)
                {
                    _logger.LogWarning("Invoice not found for update with ID: {id}", model?.Id);
                    return NotFound();
                }

                var updatedItem = _mapper.Map<InvoiceOutputVM>(updatedEntity);
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating Invoice with ID: {id}", model?.Id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Delete a Invoice.
        [HttpDelete("{id}", Name = "DeleteInvoice")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid Invoice ID received in Delete.");
                return BadRequest("Invalid Invoice ID.");
            }

            try
            {
                _logger.LogInformation("Deleting Invoice with ID: {id}", id);
                await _invoiceService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting Invoice with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get count of Invoices.
        [HttpGet("CountInvoice")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Count()
        {
            try
            {
                _logger.LogInformation("Counting Invoices...");
                var count = await _invoiceService.CountAsync();
                return Ok(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while counting Invoices");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}