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
    public class RequestService : BaseService<RequestRequestDso, RequestResponseDso>, IUseRequestService
    {
        private readonly IRequestShareRepository _share;
        public RequestService(IRequestShareRepository buildRequestShareRepository, IMapper mapper, ILoggerFactory logger) : base(mapper, logger)
        {
            _share = buildRequestShareRepository;
        }

        public override Task<int> CountAsync()
        {
            try
            {
                _logger.LogInformation("Counting Request entities...");
                return _share.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CountAsync for Request entities.");
                return Task.FromResult(0);
            }
        }

        public override async Task<RequestResponseDso> CreateAsync(RequestRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Creating new Request entity...");
                var result = await _share.CreateAsync(entity);
                var output = GetMapper().Map<RequestResponseDso>(result);
                _logger.LogInformation("Created Request entity successfully.");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating Request entity.");
                return null;
            }
        }

        public override Task DeleteAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Deleting Request entity with ID: {id}...");
                return _share.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting Request entity with ID: {id}.");
                return Task.CompletedTask;
            }
        }

        public override async Task<IEnumerable<RequestResponseDso>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all Request entities...");
                var results = await _share.GetAllAsync();
                return GetMapper().Map<IEnumerable<RequestResponseDso>>(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for Request entities.");
                return null;
            }
        }

        public override async Task<RequestResponseDso?> GetByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Retrieving Request entity with ID: {id}...");
                var result = await _share.GetByIdAsync(id);
                var item = GetMapper().Map<RequestResponseDso>(result);
                _logger.LogInformation("Retrieved Request entity successfully.");
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetByIdAsync for Request entity with ID: {id}.");
                return null;
            }
        }

        public override IQueryable<RequestResponseDso> GetQueryable()
        {
            try
            {
                _logger.LogInformation("Retrieving IQueryable<RequestResponseDso> for Request entities...");
                var queryable = _share.GetQueryable();
                var result = GetMapper().ProjectTo<RequestResponseDso>(queryable);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetQueryable for Request entities.");
                return null;
            }
        }

        public override async Task<RequestResponseDso> UpdateAsync(RequestRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Updating Request entity...");
                var result = await _share.UpdateAsync(entity);
                return GetMapper().Map<RequestResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync for Request entity.");
                return null;
            }
        }

        public override async Task<bool> ExistsAsync(object value, string name = "Id")
        {
            try
            {
                _logger.LogInformation("Checking if Request exists with {Key}: {Value}", name, value);
                var exists = await _share.ExistsAsync(value, name);
                if (!exists)
                {
                    _logger.LogWarning("Request not found with {Key}: {Value}", name, value);
                }

                return exists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking existence of Request with {Key}: {Value}", name, value);
                return false;
            }
        }

        public override async Task<PagedResponse<RequestResponseDso>> GetAllAsync(string[]? includes = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                _logger.LogInformation("Fetching all Requests with pagination: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
                var results = (await _share.GetAllAsync(includes, pageNumber, pageSize));
                var items = GetMapper().Map<List<RequestResponseDso>>(results.Data);
                return new PagedResponse<RequestResponseDso>(items, results.PageNumber, results.PageSize, results.TotalPages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all Requests.");
                return new PagedResponse<RequestResponseDso>(new List<RequestResponseDso>(), pageNumber, pageSize, 0);
            }
        }

        public override async Task<RequestResponseDso?> GetByIdAsync(object id)
        {
            try
            {
                _logger.LogInformation("Fetching Request by ID: {Id}", id);
                var result = await _share.GetByIdAsync(id);
                if (result == null)
                {
                    _logger.LogWarning("Request not found with ID: {Id}", id);
                    return null;
                }

                _logger.LogInformation("Retrieved Request successfully.");
                return GetMapper().Map<RequestResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving Request by ID: {Id}", id);
                return null;
            }
        }

        public override async Task DeleteAsync(object value, string key = "Id")
        {
            try
            {
                _logger.LogInformation("Deleting Request with {Key}: {Value}", key, value);
                await _share.DeleteAsync(value, key);
                _logger.LogInformation("Request with {Key}: {Value} deleted successfully.", key, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting Request with {Key}: {Value}", key, value);
            }
        }

        public override async Task DeleteRange(List<RequestRequestDso> entities)
        {
            try
            {
                var builddtos = entities.OfType<RequestRequestShareDto>().ToList();
                _logger.LogInformation("Deleting {Count} Requests...", 201);
                await _share.DeleteRange(builddtos);
                _logger.LogInformation("{Count} Requests deleted successfully.", 202);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting multiple Requests.");
            }
        }

        public override async Task<PagedResponse<RequestResponseDso>> GetAllByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving all Request entities...");
                var results = await _share.GetAllAsync();
                var response = await _share.GetAllByAsync(conditions, options);
                return response.ToResponse(GetMapper().Map<IEnumerable<RequestResponseDso>>(response.Data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for Request entities.");
                return null;
            }
        }

        public override async Task<RequestResponseDso?> GetOneByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving Request entity...");
                return GetMapper().Map<RequestResponseDso>(await _share.GetOneByAsync(conditions, options));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetOneByAsync  for Request entity.");
                return null;
            }
        }
    }
}