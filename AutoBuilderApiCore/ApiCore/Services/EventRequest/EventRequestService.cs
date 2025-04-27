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
    public class EventRequestService : BaseService<EventRequestRequestDso, EventRequestResponseDso>, IUseEventRequestService
    {
        private readonly IEventRequestShareRepository _share;
        public EventRequestService(IEventRequestShareRepository buildEventRequestShareRepository, IMapper mapper, ILoggerFactory logger) : base(mapper, logger)
        {
            _share = buildEventRequestShareRepository;
        }

        public override Task<int> CountAsync()
        {
            try
            {
                _logger.LogInformation("Counting EventRequest entities...");
                return _share.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CountAsync for EventRequest entities.");
                return Task.FromResult(0);
            }
        }

        public override async Task<EventRequestResponseDso> CreateAsync(EventRequestRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Creating new EventRequest entity...");
                var result = await _share.CreateAsync(entity);
                var output = GetMapper().Map<EventRequestResponseDso>(result);
                _logger.LogInformation("Created EventRequest entity successfully.");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating EventRequest entity.");
                return null;
            }
        }

        public override Task DeleteAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Deleting EventRequest entity with ID: {id}...");
                return _share.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting EventRequest entity with ID: {id}.");
                return Task.CompletedTask;
            }
        }

        public override async Task<IEnumerable<EventRequestResponseDso>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all EventRequest entities...");
                var results = await _share.GetAllAsync();
                return GetMapper().Map<IEnumerable<EventRequestResponseDso>>(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for EventRequest entities.");
                return null;
            }
        }

        public override async Task<EventRequestResponseDso?> GetByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Retrieving EventRequest entity with ID: {id}...");
                var result = await _share.GetByIdAsync(id);
                var item = GetMapper().Map<EventRequestResponseDso>(result);
                _logger.LogInformation("Retrieved EventRequest entity successfully.");
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetByIdAsync for EventRequest entity with ID: {id}.");
                return null;
            }
        }

        public override IQueryable<EventRequestResponseDso> GetQueryable()
        {
            try
            {
                _logger.LogInformation("Retrieving IQueryable<EventRequestResponseDso> for EventRequest entities...");
                var queryable = _share.GetQueryable();
                var result = GetMapper().ProjectTo<EventRequestResponseDso>(queryable);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetQueryable for EventRequest entities.");
                return null;
            }
        }

        public override async Task<EventRequestResponseDso> UpdateAsync(EventRequestRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Updating EventRequest entity...");
                var result = await _share.UpdateAsync(entity);
                return GetMapper().Map<EventRequestResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync for EventRequest entity.");
                return null;
            }
        }

        public override async Task<bool> ExistsAsync(object value, string name = "Id")
        {
            try
            {
                _logger.LogInformation("Checking if EventRequest exists with {Key}: {Value}", name, value);
                var exists = await _share.ExistsAsync(value, name);
                if (!exists)
                {
                    _logger.LogWarning("EventRequest not found with {Key}: {Value}", name, value);
                }

                return exists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking existence of EventRequest with {Key}: {Value}", name, value);
                return false;
            }
        }

        public override async Task<PagedResponse<EventRequestResponseDso>> GetAllAsync(string[]? includes = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                _logger.LogInformation("Fetching all EventRequests with pagination: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
                var results = (await _share.GetAllAsync(includes, pageNumber, pageSize));
                var items = GetMapper().Map<List<EventRequestResponseDso>>(results.Data);
                return new PagedResponse<EventRequestResponseDso>(items, results.PageNumber, results.PageSize, results.TotalPages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all EventRequests.");
                return new PagedResponse<EventRequestResponseDso>(new List<EventRequestResponseDso>(), pageNumber, pageSize, 0);
            }
        }

        public override async Task<EventRequestResponseDso?> GetByIdAsync(object id)
        {
            try
            {
                _logger.LogInformation("Fetching EventRequest by ID: {Id}", id);
                var result = await _share.GetByIdAsync(id);
                if (result == null)
                {
                    _logger.LogWarning("EventRequest not found with ID: {Id}", id);
                    return null;
                }

                _logger.LogInformation("Retrieved EventRequest successfully.");
                return GetMapper().Map<EventRequestResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving EventRequest by ID: {Id}", id);
                return null;
            }
        }

        public override async Task DeleteAsync(object value, string key = "Id")
        {
            try
            {
                _logger.LogInformation("Deleting EventRequest with {Key}: {Value}", key, value);
                await _share.DeleteAsync(value, key);
                _logger.LogInformation("EventRequest with {Key}: {Value} deleted successfully.", key, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting EventRequest with {Key}: {Value}", key, value);
            }
        }

        public override async Task DeleteRange(List<EventRequestRequestDso> entities)
        {
            try
            {
                var builddtos = entities.OfType<EventRequestRequestShareDto>().ToList();
                _logger.LogInformation("Deleting {Count} EventRequests...", 201);
                await _share.DeleteRange(builddtos);
                _logger.LogInformation("{Count} EventRequests deleted successfully.", 202);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting multiple EventRequests.");
            }
        }

        public override async Task<PagedResponse<EventRequestResponseDso>> GetAllByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving all EventRequest entities...");
                var results = await _share.GetAllAsync();
                var response = await _share.GetAllByAsync(conditions, options);
                return response.ToResponse(GetMapper().Map<IEnumerable<EventRequestResponseDso>>(response.Data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for EventRequest entities.");
                return null;
            }
        }

        public override async Task<EventRequestResponseDso?> GetOneByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving EventRequest entity...");
                return GetMapper().Map<EventRequestResponseDso>(await _share.GetOneByAsync(conditions, options));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetOneByAsync  for EventRequest entity.");
                return null;
            }
        }
    }
}