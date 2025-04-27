using AutoGenerator;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using AutoGenerator.Services.Base;
using ApiCore.DyModels.Dso.Requests;
using ApiCore.DyModels.Dso.Responses;
using LAHJAAPI.Models;
using ApiCore.DyModels.Dto.Share.Requests;
using ApiCore.DyModels.Dto.Share.Responses;
using ApiCore.Repositories.Share;
using System.Linq.Expressions;
using ApiCore.Repositories.Builder;
using AutoGenerator.Repositories.Base;
using AutoGenerator.Helper;
using System;

namespace ApiCore.Services.Services
{
    public class InvoiceService : BaseService<InvoiceRequestDso, InvoiceResponseDso>, IUseInvoiceService
    {
        private readonly IInvoiceShareRepository _share;
        public InvoiceService(IInvoiceShareRepository buildInvoiceShareRepository, IMapper mapper, ILoggerFactory logger) : base(mapper, logger)
        {
            _share = buildInvoiceShareRepository;
        }

        public override Task<int> CountAsync()
        {
            try
            {
                _logger.LogInformation("Counting Invoice entities...");
                return _share.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CountAsync for Invoice entities.");
                return Task.FromResult(0);
            }
        }

        public override async Task<InvoiceResponseDso> CreateAsync(InvoiceRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Creating new Invoice entity...");
                var result = await _share.CreateAsync(entity);
                var output = GetMapper().Map<InvoiceResponseDso>(result);
                _logger.LogInformation("Created Invoice entity successfully.");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating Invoice entity.");
                return null;
            }
        }

        public override Task DeleteAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Deleting Invoice entity with ID: {id}...");
                return _share.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting Invoice entity with ID: {id}.");
                return Task.CompletedTask;
            }
        }

        public override async Task<IEnumerable<InvoiceResponseDso>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all Invoice entities...");
                var results = await _share.GetAllAsync();
                return GetMapper().Map<IEnumerable<InvoiceResponseDso>>(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for Invoice entities.");
                return null;
            }
        }

        public override async Task<InvoiceResponseDso?> GetByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Retrieving Invoice entity with ID: {id}...");
                var result = await _share.GetByIdAsync(id);
                var item = GetMapper().Map<InvoiceResponseDso>(result);
                _logger.LogInformation("Retrieved Invoice entity successfully.");
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetByIdAsync for Invoice entity with ID: {id}.");
                return null;
            }
        }

        public override IQueryable<InvoiceResponseDso> GetQueryable()
        {
            try
            {
                _logger.LogInformation("Retrieving IQueryable<InvoiceResponseDso> for Invoice entities...");
                var queryable = _share.GetQueryable();
                var result = GetMapper().ProjectTo<InvoiceResponseDso>(queryable);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetQueryable for Invoice entities.");
                return null;
            }
        }

        public override async Task<InvoiceResponseDso> UpdateAsync(InvoiceRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Updating Invoice entity...");
                var result = await _share.UpdateAsync(entity);
                return GetMapper().Map<InvoiceResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync for Invoice entity.");
                return null;
            }
        }

        public override async Task<bool> ExistsAsync(object value, string name = "Id")
        {
            try
            {
                _logger.LogInformation("Checking if Invoice exists with {Key}: {Value}", name, value);
                var exists = await _share.ExistsAsync(value, name);
                if (!exists)
                {
                    _logger.LogWarning("Invoice not found with {Key}: {Value}", name, value);
                }

                return exists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking existence of Invoice with {Key}: {Value}", name, value);
                return false;
            }
        }

        public override async Task<PagedResponse<InvoiceResponseDso>> GetAllAsync(string[]? includes = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                _logger.LogInformation("Fetching all Invoices with pagination: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
                var results = (await _share.GetAllAsync(includes, pageNumber, pageSize));
                var items = GetMapper().Map<List<InvoiceResponseDso>>(results.Data);
                return new PagedResponse<InvoiceResponseDso>(items, results.PageNumber, results.PageSize, results.TotalPages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all Invoices.");
                return new PagedResponse<InvoiceResponseDso>(new List<InvoiceResponseDso>(), pageNumber, pageSize, 0);
            }
        }

        public override async Task<InvoiceResponseDso?> GetByIdAsync(object id)
        {
            try
            {
                _logger.LogInformation("Fetching Invoice by ID: {Id}", id);
                var result = await _share.GetByIdAsync(id);
                if (result == null)
                {
                    _logger.LogWarning("Invoice not found with ID: {Id}", id);
                    return null;
                }

                _logger.LogInformation("Retrieved Invoice successfully.");
                return GetMapper().Map<InvoiceResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving Invoice by ID: {Id}", id);
                return null;
            }
        }

        public override async Task DeleteAsync(object value, string key = "Id")
        {
            try
            {
                _logger.LogInformation("Deleting Invoice with {Key}: {Value}", key, value);
                await _share.DeleteAsync(value, key);
                _logger.LogInformation("Invoice with {Key}: {Value} deleted successfully.", key, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting Invoice with {Key}: {Value}", key, value);
            }
        }

        public override async Task DeleteRange(List<InvoiceRequestDso> entities)
        {
            try
            {
                var builddtos = entities.OfType<InvoiceRequestShareDto>().ToList();
                _logger.LogInformation("Deleting {Count} Invoices...", 201);
                await _share.DeleteRange(builddtos);
                _logger.LogInformation("{Count} Invoices deleted successfully.", 202);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting multiple Invoices.");
            }
        }

        public override async Task<PagedResponse<InvoiceResponseDso>> GetAllByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving all Invoice entities...");
                var results = await _share.GetAllAsync();
                var response = await _share.GetAllByAsync(conditions, options);
                return response.ToResponse(GetMapper().Map<IEnumerable<InvoiceResponseDso>>(response.Data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for Invoice entities.");
                return null;
            }
        }

        public override async Task<InvoiceResponseDso?> GetOneByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving Invoice entity...");
                return GetMapper().Map<InvoiceResponseDso>(await _share.GetOneByAsync(conditions, options));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetOneByAsync  for Invoice entity.");
                return null;
            }
        }
    }
}