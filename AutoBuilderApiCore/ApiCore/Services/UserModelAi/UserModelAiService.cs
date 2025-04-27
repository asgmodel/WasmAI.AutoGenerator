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
    public class UserModelAiService : BaseService<UserModelAiRequestDso, UserModelAiResponseDso>, IUseUserModelAiService
    {
        private readonly IUserModelAiShareRepository _share;
        public UserModelAiService(IUserModelAiShareRepository buildUserModelAiShareRepository, IMapper mapper, ILoggerFactory logger) : base(mapper, logger)
        {
            _share = buildUserModelAiShareRepository;
        }

        public override Task<int> CountAsync()
        {
            try
            {
                _logger.LogInformation("Counting UserModelAi entities...");
                return _share.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CountAsync for UserModelAi entities.");
                return Task.FromResult(0);
            }
        }

        public override async Task<UserModelAiResponseDso> CreateAsync(UserModelAiRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Creating new UserModelAi entity...");
                var result = await _share.CreateAsync(entity);
                var output = GetMapper().Map<UserModelAiResponseDso>(result);
                _logger.LogInformation("Created UserModelAi entity successfully.");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating UserModelAi entity.");
                return null;
            }
        }

        public override Task DeleteAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Deleting UserModelAi entity with ID: {id}...");
                return _share.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting UserModelAi entity with ID: {id}.");
                return Task.CompletedTask;
            }
        }

        public override async Task<IEnumerable<UserModelAiResponseDso>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all UserModelAi entities...");
                var results = await _share.GetAllAsync();
                return GetMapper().Map<IEnumerable<UserModelAiResponseDso>>(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for UserModelAi entities.");
                return null;
            }
        }

        public override async Task<UserModelAiResponseDso?> GetByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Retrieving UserModelAi entity with ID: {id}...");
                var result = await _share.GetByIdAsync(id);
                var item = GetMapper().Map<UserModelAiResponseDso>(result);
                _logger.LogInformation("Retrieved UserModelAi entity successfully.");
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetByIdAsync for UserModelAi entity with ID: {id}.");
                return null;
            }
        }

        public override IQueryable<UserModelAiResponseDso> GetQueryable()
        {
            try
            {
                _logger.LogInformation("Retrieving IQueryable<UserModelAiResponseDso> for UserModelAi entities...");
                var queryable = _share.GetQueryable();
                var result = GetMapper().ProjectTo<UserModelAiResponseDso>(queryable);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetQueryable for UserModelAi entities.");
                return null;
            }
        }

        public override async Task<UserModelAiResponseDso> UpdateAsync(UserModelAiRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Updating UserModelAi entity...");
                var result = await _share.UpdateAsync(entity);
                return GetMapper().Map<UserModelAiResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync for UserModelAi entity.");
                return null;
            }
        }

        public override async Task<bool> ExistsAsync(object value, string name = "Id")
        {
            try
            {
                _logger.LogInformation("Checking if UserModelAi exists with {Key}: {Value}", name, value);
                var exists = await _share.ExistsAsync(value, name);
                if (!exists)
                {
                    _logger.LogWarning("UserModelAi not found with {Key}: {Value}", name, value);
                }

                return exists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking existence of UserModelAi with {Key}: {Value}", name, value);
                return false;
            }
        }

        public override async Task<PagedResponse<UserModelAiResponseDso>> GetAllAsync(string[]? includes = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                _logger.LogInformation("Fetching all UserModelAis with pagination: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
                var results = (await _share.GetAllAsync(includes, pageNumber, pageSize));
                var items = GetMapper().Map<List<UserModelAiResponseDso>>(results.Data);
                return new PagedResponse<UserModelAiResponseDso>(items, results.PageNumber, results.PageSize, results.TotalPages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all UserModelAis.");
                return new PagedResponse<UserModelAiResponseDso>(new List<UserModelAiResponseDso>(), pageNumber, pageSize, 0);
            }
        }

        public override async Task<UserModelAiResponseDso?> GetByIdAsync(object id)
        {
            try
            {
                _logger.LogInformation("Fetching UserModelAi by ID: {Id}", id);
                var result = await _share.GetByIdAsync(id);
                if (result == null)
                {
                    _logger.LogWarning("UserModelAi not found with ID: {Id}", id);
                    return null;
                }

                _logger.LogInformation("Retrieved UserModelAi successfully.");
                return GetMapper().Map<UserModelAiResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving UserModelAi by ID: {Id}", id);
                return null;
            }
        }

        public override async Task DeleteAsync(object value, string key = "Id")
        {
            try
            {
                _logger.LogInformation("Deleting UserModelAi with {Key}: {Value}", key, value);
                await _share.DeleteAsync(value, key);
                _logger.LogInformation("UserModelAi with {Key}: {Value} deleted successfully.", key, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting UserModelAi with {Key}: {Value}", key, value);
            }
        }

        public override async Task DeleteRange(List<UserModelAiRequestDso> entities)
        {
            try
            {
                var builddtos = entities.OfType<UserModelAiRequestShareDto>().ToList();
                _logger.LogInformation("Deleting {Count} UserModelAis...", 201);
                await _share.DeleteRange(builddtos);
                _logger.LogInformation("{Count} UserModelAis deleted successfully.", 202);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting multiple UserModelAis.");
            }
        }

        public override async Task<PagedResponse<UserModelAiResponseDso>> GetAllByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving all UserModelAi entities...");
                var results = await _share.GetAllAsync();
                var response = await _share.GetAllByAsync(conditions, options);
                return response.ToResponse(GetMapper().Map<IEnumerable<UserModelAiResponseDso>>(response.Data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for UserModelAi entities.");
                return null;
            }
        }

        public override async Task<UserModelAiResponseDso?> GetOneByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving UserModelAi entity...");
                return GetMapper().Map<UserModelAiResponseDso>(await _share.GetOneByAsync(conditions, options));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetOneByAsync  for UserModelAi entity.");
                return null;
            }
        }
    }
}