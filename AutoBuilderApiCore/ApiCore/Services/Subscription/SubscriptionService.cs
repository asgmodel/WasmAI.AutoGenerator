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
    public class SubscriptionService : BaseService<SubscriptionRequestDso, SubscriptionResponseDso>, IUseSubscriptionService
    {
        private readonly ISubscriptionShareRepository _share;
        public SubscriptionService(ISubscriptionShareRepository buildSubscriptionShareRepository, IMapper mapper, ILoggerFactory logger) : base(mapper, logger)
        {
            _share = buildSubscriptionShareRepository;
        }

        public override Task<int> CountAsync()
        {
            try
            {
                _logger.LogInformation("Counting Subscription entities...");
                return _share.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CountAsync for Subscription entities.");
                return Task.FromResult(0);
            }
        }

        public override async Task<SubscriptionResponseDso> CreateAsync(SubscriptionRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Creating new Subscription entity...");
                var result = await _share.CreateAsync(entity);
                var output = GetMapper().Map<SubscriptionResponseDso>(result);
                _logger.LogInformation("Created Subscription entity successfully.");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating Subscription entity.");
                return null;
            }
        }

        public override Task DeleteAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Deleting Subscription entity with ID: {id}...");
                return _share.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting Subscription entity with ID: {id}.");
                return Task.CompletedTask;
            }
        }

        public override async Task<IEnumerable<SubscriptionResponseDso>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all Subscription entities...");
                var results = await _share.GetAllAsync();
                return GetMapper().Map<IEnumerable<SubscriptionResponseDso>>(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for Subscription entities.");
                return null;
            }
        }

        public override async Task<SubscriptionResponseDso?> GetByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Retrieving Subscription entity with ID: {id}...");
                var result = await _share.GetByIdAsync(id);
                var item = GetMapper().Map<SubscriptionResponseDso>(result);
                _logger.LogInformation("Retrieved Subscription entity successfully.");
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetByIdAsync for Subscription entity with ID: {id}.");
                return null;
            }
        }

        public override IQueryable<SubscriptionResponseDso> GetQueryable()
        {
            try
            {
                _logger.LogInformation("Retrieving IQueryable<SubscriptionResponseDso> for Subscription entities...");
                var queryable = _share.GetQueryable();
                var result = GetMapper().ProjectTo<SubscriptionResponseDso>(queryable);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetQueryable for Subscription entities.");
                return null;
            }
        }

        public override async Task<SubscriptionResponseDso> UpdateAsync(SubscriptionRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Updating Subscription entity...");
                var result = await _share.UpdateAsync(entity);
                return GetMapper().Map<SubscriptionResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync for Subscription entity.");
                return null;
            }
        }

        public override async Task<bool> ExistsAsync(object value, string name = "Id")
        {
            try
            {
                _logger.LogInformation("Checking if Subscription exists with {Key}: {Value}", name, value);
                var exists = await _share.ExistsAsync(value, name);
                if (!exists)
                {
                    _logger.LogWarning("Subscription not found with {Key}: {Value}", name, value);
                }

                return exists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking existence of Subscription with {Key}: {Value}", name, value);
                return false;
            }
        }

        public override async Task<PagedResponse<SubscriptionResponseDso>> GetAllAsync(string[]? includes = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                _logger.LogInformation("Fetching all Subscriptions with pagination: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
                var results = (await _share.GetAllAsync(includes, pageNumber, pageSize));
                var items = GetMapper().Map<List<SubscriptionResponseDso>>(results.Data);
                return new PagedResponse<SubscriptionResponseDso>(items, results.PageNumber, results.PageSize, results.TotalPages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all Subscriptions.");
                return new PagedResponse<SubscriptionResponseDso>(new List<SubscriptionResponseDso>(), pageNumber, pageSize, 0);
            }
        }

        public override async Task<SubscriptionResponseDso?> GetByIdAsync(object id)
        {
            try
            {
                _logger.LogInformation("Fetching Subscription by ID: {Id}", id);
                var result = await _share.GetByIdAsync(id);
                if (result == null)
                {
                    _logger.LogWarning("Subscription not found with ID: {Id}", id);
                    return null;
                }

                _logger.LogInformation("Retrieved Subscription successfully.");
                return GetMapper().Map<SubscriptionResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving Subscription by ID: {Id}", id);
                return null;
            }
        }

        public override async Task DeleteAsync(object value, string key = "Id")
        {
            try
            {
                _logger.LogInformation("Deleting Subscription with {Key}: {Value}", key, value);
                await _share.DeleteAsync(value, key);
                _logger.LogInformation("Subscription with {Key}: {Value} deleted successfully.", key, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting Subscription with {Key}: {Value}", key, value);
            }
        }

        public override async Task DeleteRange(List<SubscriptionRequestDso> entities)
        {
            try
            {
                var builddtos = entities.OfType<SubscriptionRequestShareDto>().ToList();
                _logger.LogInformation("Deleting {Count} Subscriptions...", 201);
                await _share.DeleteRange(builddtos);
                _logger.LogInformation("{Count} Subscriptions deleted successfully.", 202);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting multiple Subscriptions.");
            }
        }

        public override async Task<PagedResponse<SubscriptionResponseDso>> GetAllByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving all Subscription entities...");
                var results = await _share.GetAllAsync();
                var response = await _share.GetAllByAsync(conditions, options);
                return response.ToResponse(GetMapper().Map<IEnumerable<SubscriptionResponseDso>>(response.Data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for Subscription entities.");
                return null;
            }
        }

        public override async Task<SubscriptionResponseDso?> GetOneByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving Subscription entity...");
                return GetMapper().Map<SubscriptionResponseDso>(await _share.GetOneByAsync(conditions, options));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetOneByAsync  for Subscription entity.");
                return null;
            }
        }
    }
}