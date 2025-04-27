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
    public class PaymentService : BaseService<PaymentRequestDso, PaymentResponseDso>, IUsePaymentService
    {
        private readonly IPaymentShareRepository _share;
        public PaymentService(IPaymentShareRepository buildPaymentShareRepository, IMapper mapper, ILoggerFactory logger) : base(mapper, logger)
        {
            _share = buildPaymentShareRepository;
        }

        public override Task<int> CountAsync()
        {
            try
            {
                _logger.LogInformation("Counting Payment entities...");
                return _share.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CountAsync for Payment entities.");
                return Task.FromResult(0);
            }
        }

        public override async Task<PaymentResponseDso> CreateAsync(PaymentRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Creating new Payment entity...");
                var result = await _share.CreateAsync(entity);
                var output = GetMapper().Map<PaymentResponseDso>(result);
                _logger.LogInformation("Created Payment entity successfully.");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating Payment entity.");
                return null;
            }
        }

        public override Task DeleteAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Deleting Payment entity with ID: {id}...");
                return _share.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting Payment entity with ID: {id}.");
                return Task.CompletedTask;
            }
        }

        public override async Task<IEnumerable<PaymentResponseDso>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all Payment entities...");
                var results = await _share.GetAllAsync();
                return GetMapper().Map<IEnumerable<PaymentResponseDso>>(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for Payment entities.");
                return null;
            }
        }

        public override async Task<PaymentResponseDso?> GetByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Retrieving Payment entity with ID: {id}...");
                var result = await _share.GetByIdAsync(id);
                var item = GetMapper().Map<PaymentResponseDso>(result);
                _logger.LogInformation("Retrieved Payment entity successfully.");
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetByIdAsync for Payment entity with ID: {id}.");
                return null;
            }
        }

        public override IQueryable<PaymentResponseDso> GetQueryable()
        {
            try
            {
                _logger.LogInformation("Retrieving IQueryable<PaymentResponseDso> for Payment entities...");
                var queryable = _share.GetQueryable();
                var result = GetMapper().ProjectTo<PaymentResponseDso>(queryable);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetQueryable for Payment entities.");
                return null;
            }
        }

        public override async Task<PaymentResponseDso> UpdateAsync(PaymentRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Updating Payment entity...");
                var result = await _share.UpdateAsync(entity);
                return GetMapper().Map<PaymentResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync for Payment entity.");
                return null;
            }
        }

        public override async Task<bool> ExistsAsync(object value, string name = "Id")
        {
            try
            {
                _logger.LogInformation("Checking if Payment exists with {Key}: {Value}", name, value);
                var exists = await _share.ExistsAsync(value, name);
                if (!exists)
                {
                    _logger.LogWarning("Payment not found with {Key}: {Value}", name, value);
                }

                return exists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking existence of Payment with {Key}: {Value}", name, value);
                return false;
            }
        }

        public override async Task<PagedResponse<PaymentResponseDso>> GetAllAsync(string[]? includes = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                _logger.LogInformation("Fetching all Payments with pagination: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
                var results = (await _share.GetAllAsync(includes, pageNumber, pageSize));
                var items = GetMapper().Map<List<PaymentResponseDso>>(results.Data);
                return new PagedResponse<PaymentResponseDso>(items, results.PageNumber, results.PageSize, results.TotalPages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all Payments.");
                return new PagedResponse<PaymentResponseDso>(new List<PaymentResponseDso>(), pageNumber, pageSize, 0);
            }
        }

        public override async Task<PaymentResponseDso?> GetByIdAsync(object id)
        {
            try
            {
                _logger.LogInformation("Fetching Payment by ID: {Id}", id);
                var result = await _share.GetByIdAsync(id);
                if (result == null)
                {
                    _logger.LogWarning("Payment not found with ID: {Id}", id);
                    return null;
                }

                _logger.LogInformation("Retrieved Payment successfully.");
                return GetMapper().Map<PaymentResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving Payment by ID: {Id}", id);
                return null;
            }
        }

        public override async Task DeleteAsync(object value, string key = "Id")
        {
            try
            {
                _logger.LogInformation("Deleting Payment with {Key}: {Value}", key, value);
                await _share.DeleteAsync(value, key);
                _logger.LogInformation("Payment with {Key}: {Value} deleted successfully.", key, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting Payment with {Key}: {Value}", key, value);
            }
        }

        public override async Task DeleteRange(List<PaymentRequestDso> entities)
        {
            try
            {
                var builddtos = entities.OfType<PaymentRequestShareDto>().ToList();
                _logger.LogInformation("Deleting {Count} Payments...", 201);
                await _share.DeleteRange(builddtos);
                _logger.LogInformation("{Count} Payments deleted successfully.", 202);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting multiple Payments.");
            }
        }

        public override async Task<PagedResponse<PaymentResponseDso>> GetAllByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving all Payment entities...");
                var results = await _share.GetAllAsync();
                var response = await _share.GetAllByAsync(conditions, options);
                return response.ToResponse(GetMapper().Map<IEnumerable<PaymentResponseDso>>(response.Data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for Payment entities.");
                return null;
            }
        }

        public override async Task<PaymentResponseDso?> GetOneByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving Payment entity...");
                return GetMapper().Map<PaymentResponseDso>(await _share.GetOneByAsync(conditions, options));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetOneByAsync  for Payment entity.");
                return null;
            }
        }
    }
}