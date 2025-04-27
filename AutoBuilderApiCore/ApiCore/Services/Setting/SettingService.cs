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
    public class SettingService : BaseService<SettingRequestDso, SettingResponseDso>, IUseSettingService
    {
        private readonly ISettingShareRepository _share;
        public SettingService(ISettingShareRepository buildSettingShareRepository, IMapper mapper, ILoggerFactory logger) : base(mapper, logger)
        {
            _share = buildSettingShareRepository;
        }

        public override Task<int> CountAsync()
        {
            try
            {
                _logger.LogInformation("Counting Setting entities...");
                return _share.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CountAsync for Setting entities.");
                return Task.FromResult(0);
            }
        }

        public override async Task<SettingResponseDso> CreateAsync(SettingRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Creating new Setting entity...");
                var result = await _share.CreateAsync(entity);
                var output = GetMapper().Map<SettingResponseDso>(result);
                _logger.LogInformation("Created Setting entity successfully.");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating Setting entity.");
                return null;
            }
        }

        public override Task DeleteAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Deleting Setting entity with ID: {id}...");
                return _share.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting Setting entity with ID: {id}.");
                return Task.CompletedTask;
            }
        }

        public override async Task<IEnumerable<SettingResponseDso>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all Setting entities...");
                var results = await _share.GetAllAsync();
                return GetMapper().Map<IEnumerable<SettingResponseDso>>(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for Setting entities.");
                return null;
            }
        }

        public override async Task<SettingResponseDso?> GetByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Retrieving Setting entity with ID: {id}...");
                var result = await _share.GetByIdAsync(id);
                var item = GetMapper().Map<SettingResponseDso>(result);
                _logger.LogInformation("Retrieved Setting entity successfully.");
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetByIdAsync for Setting entity with ID: {id}.");
                return null;
            }
        }

        public override IQueryable<SettingResponseDso> GetQueryable()
        {
            try
            {
                _logger.LogInformation("Retrieving IQueryable<SettingResponseDso> for Setting entities...");
                var queryable = _share.GetQueryable();
                var result = GetMapper().ProjectTo<SettingResponseDso>(queryable);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetQueryable for Setting entities.");
                return null;
            }
        }

        public override async Task<SettingResponseDso> UpdateAsync(SettingRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Updating Setting entity...");
                var result = await _share.UpdateAsync(entity);
                return GetMapper().Map<SettingResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync for Setting entity.");
                return null;
            }
        }

        public override async Task<bool> ExistsAsync(object value, string name = "Id")
        {
            try
            {
                _logger.LogInformation("Checking if Setting exists with {Key}: {Value}", name, value);
                var exists = await _share.ExistsAsync(value, name);
                if (!exists)
                {
                    _logger.LogWarning("Setting not found with {Key}: {Value}", name, value);
                }

                return exists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking existence of Setting with {Key}: {Value}", name, value);
                return false;
            }
        }

        public override async Task<PagedResponse<SettingResponseDso>> GetAllAsync(string[]? includes = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                _logger.LogInformation("Fetching all Settings with pagination: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
                var results = (await _share.GetAllAsync(includes, pageNumber, pageSize));
                var items = GetMapper().Map<List<SettingResponseDso>>(results.Data);
                return new PagedResponse<SettingResponseDso>(items, results.PageNumber, results.PageSize, results.TotalPages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all Settings.");
                return new PagedResponse<SettingResponseDso>(new List<SettingResponseDso>(), pageNumber, pageSize, 0);
            }
        }

        public override async Task<SettingResponseDso?> GetByIdAsync(object id)
        {
            try
            {
                _logger.LogInformation("Fetching Setting by ID: {Id}", id);
                var result = await _share.GetByIdAsync(id);
                if (result == null)
                {
                    _logger.LogWarning("Setting not found with ID: {Id}", id);
                    return null;
                }

                _logger.LogInformation("Retrieved Setting successfully.");
                return GetMapper().Map<SettingResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving Setting by ID: {Id}", id);
                return null;
            }
        }

        public override async Task DeleteAsync(object value, string key = "Id")
        {
            try
            {
                _logger.LogInformation("Deleting Setting with {Key}: {Value}", key, value);
                await _share.DeleteAsync(value, key);
                _logger.LogInformation("Setting with {Key}: {Value} deleted successfully.", key, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting Setting with {Key}: {Value}", key, value);
            }
        }

        public override async Task DeleteRange(List<SettingRequestDso> entities)
        {
            try
            {
                var builddtos = entities.OfType<SettingRequestShareDto>().ToList();
                _logger.LogInformation("Deleting {Count} Settings...", 201);
                await _share.DeleteRange(builddtos);
                _logger.LogInformation("{Count} Settings deleted successfully.", 202);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting multiple Settings.");
            }
        }

        public override async Task<PagedResponse<SettingResponseDso>> GetAllByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving all Setting entities...");
                var results = await _share.GetAllAsync();
                var response = await _share.GetAllByAsync(conditions, options);
                return response.ToResponse(GetMapper().Map<IEnumerable<SettingResponseDso>>(response.Data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for Setting entities.");
                return null;
            }
        }

        public override async Task<SettingResponseDso?> GetOneByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving Setting entity...");
                return GetMapper().Map<SettingResponseDso>(await _share.GetOneByAsync(conditions, options));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetOneByAsync  for Setting entity.");
                return null;
            }
        }
    }
}