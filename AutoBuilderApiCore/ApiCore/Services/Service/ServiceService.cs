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
    public class ServiceService : BaseService<ServiceRequestDso, ServiceResponseDso>, IUseServiceService
    {
        private readonly IServiceShareRepository _share;
        public ServiceService(IServiceShareRepository buildServiceShareRepository, IMapper mapper, ILoggerFactory logger) : base(mapper, logger)
        {
            _share = buildServiceShareRepository;
        }

        public override Task<int> CountAsync()
        {
            try
            {
                _logger.LogInformation("Counting Service entities...");
                return _share.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CountAsync for Service entities.");
                return Task.FromResult(0);
            }
        }

        public override async Task<ServiceResponseDso> CreateAsync(ServiceRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Creating new Service entity...");
                var result = await _share.CreateAsync(entity);
                var output = GetMapper().Map<ServiceResponseDso>(result);
                _logger.LogInformation("Created Service entity successfully.");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating Service entity.");
                return null;
            }
        }

        public override Task DeleteAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Deleting Service entity with ID: {id}...");
                return _share.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting Service entity with ID: {id}.");
                return Task.CompletedTask;
            }
        }

        public override async Task<IEnumerable<ServiceResponseDso>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all Service entities...");
                var results = await _share.GetAllAsync();
                return GetMapper().Map<IEnumerable<ServiceResponseDso>>(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for Service entities.");
                return null;
            }
        }

        public override async Task<ServiceResponseDso?> GetByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Retrieving Service entity with ID: {id}...");
                var result = await _share.GetByIdAsync(id);
                var item = GetMapper().Map<ServiceResponseDso>(result);
                _logger.LogInformation("Retrieved Service entity successfully.");
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetByIdAsync for Service entity with ID: {id}.");
                return null;
            }
        }

        public override IQueryable<ServiceResponseDso> GetQueryable()
        {
            try
            {
                _logger.LogInformation("Retrieving IQueryable<ServiceResponseDso> for Service entities...");
                var queryable = _share.GetQueryable();
                var result = GetMapper().ProjectTo<ServiceResponseDso>(queryable);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetQueryable for Service entities.");
                return null;
            }
        }

        public override async Task<ServiceResponseDso> UpdateAsync(ServiceRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Updating Service entity...");
                var result = await _share.UpdateAsync(entity);
                return GetMapper().Map<ServiceResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync for Service entity.");
                return null;
            }
        }

        public override async Task<bool> ExistsAsync(object value, string name = "Id")
        {
            try
            {
                _logger.LogInformation("Checking if Service exists with {Key}: {Value}", name, value);
                var exists = await _share.ExistsAsync(value, name);
                if (!exists)
                {
                    _logger.LogWarning("Service not found with {Key}: {Value}", name, value);
                }

                return exists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking existence of Service with {Key}: {Value}", name, value);
                return false;
            }
        }

        public override async Task<PagedResponse<ServiceResponseDso>> GetAllAsync(string[]? includes = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                _logger.LogInformation("Fetching all Services with pagination: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
                var results = (await _share.GetAllAsync(includes, pageNumber, pageSize));
                var items = GetMapper().Map<List<ServiceResponseDso>>(results.Data);
                return new PagedResponse<ServiceResponseDso>(items, results.PageNumber, results.PageSize, results.TotalPages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all Services.");
                return new PagedResponse<ServiceResponseDso>(new List<ServiceResponseDso>(), pageNumber, pageSize, 0);
            }
        }

        public override async Task<ServiceResponseDso?> GetByIdAsync(object id)
        {
            try
            {
                _logger.LogInformation("Fetching Service by ID: {Id}", id);
                var result = await _share.GetByIdAsync(id);
                if (result == null)
                {
                    _logger.LogWarning("Service not found with ID: {Id}", id);
                    return null;
                }

                _logger.LogInformation("Retrieved Service successfully.");
                return GetMapper().Map<ServiceResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving Service by ID: {Id}", id);
                return null;
            }
        }

        public override async Task DeleteAsync(object value, string key = "Id")
        {
            try
            {
                _logger.LogInformation("Deleting Service with {Key}: {Value}", key, value);
                await _share.DeleteAsync(value, key);
                _logger.LogInformation("Service with {Key}: {Value} deleted successfully.", key, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting Service with {Key}: {Value}", key, value);
            }
        }

        public override async Task DeleteRange(List<ServiceRequestDso> entities)
        {
            try
            {
                var builddtos = entities.OfType<ServiceRequestShareDto>().ToList();
                _logger.LogInformation("Deleting {Count} Services...", 201);
                await _share.DeleteRange(builddtos);
                _logger.LogInformation("{Count} Services deleted successfully.", 202);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting multiple Services.");
            }
        }

        public override async Task<PagedResponse<ServiceResponseDso>> GetAllByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving all Service entities...");
                var results = await _share.GetAllAsync();
                var response = await _share.GetAllByAsync(conditions, options);
                return response.ToResponse(GetMapper().Map<IEnumerable<ServiceResponseDso>>(response.Data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for Service entities.");
                return null;
            }
        }

        public override async Task<ServiceResponseDso?> GetOneByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving Service entity...");
                return GetMapper().Map<ServiceResponseDso>(await _share.GetOneByAsync(conditions, options));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetOneByAsync  for Service entity.");
                return null;
            }
        }
    }
}