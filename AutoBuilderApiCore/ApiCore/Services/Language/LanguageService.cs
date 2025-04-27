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
    public class LanguageService : BaseService<LanguageRequestDso, LanguageResponseDso>, IUseLanguageService
    {
        private readonly ILanguageShareRepository _share;
        public LanguageService(ILanguageShareRepository buildLanguageShareRepository, IMapper mapper, ILoggerFactory logger) : base(mapper, logger)
        {
            _share = buildLanguageShareRepository;
        }

        public override Task<int> CountAsync()
        {
            try
            {
                _logger.LogInformation("Counting Language entities...");
                return _share.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CountAsync for Language entities.");
                return Task.FromResult(0);
            }
        }

        public override async Task<LanguageResponseDso> CreateAsync(LanguageRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Creating new Language entity...");
                var result = await _share.CreateAsync(entity);
                var output = GetMapper().Map<LanguageResponseDso>(result);
                _logger.LogInformation("Created Language entity successfully.");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating Language entity.");
                return null;
            }
        }

        public override Task DeleteAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Deleting Language entity with ID: {id}...");
                return _share.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting Language entity with ID: {id}.");
                return Task.CompletedTask;
            }
        }

        public override async Task<IEnumerable<LanguageResponseDso>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all Language entities...");
                var results = await _share.GetAllAsync();
                return GetMapper().Map<IEnumerable<LanguageResponseDso>>(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for Language entities.");
                return null;
            }
        }

        public override async Task<LanguageResponseDso?> GetByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Retrieving Language entity with ID: {id}...");
                var result = await _share.GetByIdAsync(id);
                var item = GetMapper().Map<LanguageResponseDso>(result);
                _logger.LogInformation("Retrieved Language entity successfully.");
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetByIdAsync for Language entity with ID: {id}.");
                return null;
            }
        }

        public override IQueryable<LanguageResponseDso> GetQueryable()
        {
            try
            {
                _logger.LogInformation("Retrieving IQueryable<LanguageResponseDso> for Language entities...");
                var queryable = _share.GetQueryable();
                var result = GetMapper().ProjectTo<LanguageResponseDso>(queryable);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetQueryable for Language entities.");
                return null;
            }
        }

        public override async Task<LanguageResponseDso> UpdateAsync(LanguageRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Updating Language entity...");
                var result = await _share.UpdateAsync(entity);
                return GetMapper().Map<LanguageResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync for Language entity.");
                return null;
            }
        }

        public override async Task<bool> ExistsAsync(object value, string name = "Id")
        {
            try
            {
                _logger.LogInformation("Checking if Language exists with {Key}: {Value}", name, value);
                var exists = await _share.ExistsAsync(value, name);
                if (!exists)
                {
                    _logger.LogWarning("Language not found with {Key}: {Value}", name, value);
                }

                return exists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking existence of Language with {Key}: {Value}", name, value);
                return false;
            }
        }

        public override async Task<PagedResponse<LanguageResponseDso>> GetAllAsync(string[]? includes = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                _logger.LogInformation("Fetching all Languages with pagination: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
                var results = (await _share.GetAllAsync(includes, pageNumber, pageSize));
                var items = GetMapper().Map<List<LanguageResponseDso>>(results.Data);
                return new PagedResponse<LanguageResponseDso>(items, results.PageNumber, results.PageSize, results.TotalPages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all Languages.");
                return new PagedResponse<LanguageResponseDso>(new List<LanguageResponseDso>(), pageNumber, pageSize, 0);
            }
        }

        public override async Task<LanguageResponseDso?> GetByIdAsync(object id)
        {
            try
            {
                _logger.LogInformation("Fetching Language by ID: {Id}", id);
                var result = await _share.GetByIdAsync(id);
                if (result == null)
                {
                    _logger.LogWarning("Language not found with ID: {Id}", id);
                    return null;
                }

                _logger.LogInformation("Retrieved Language successfully.");
                return GetMapper().Map<LanguageResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving Language by ID: {Id}", id);
                return null;
            }
        }

        public override async Task DeleteAsync(object value, string key = "Id")
        {
            try
            {
                _logger.LogInformation("Deleting Language with {Key}: {Value}", key, value);
                await _share.DeleteAsync(value, key);
                _logger.LogInformation("Language with {Key}: {Value} deleted successfully.", key, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting Language with {Key}: {Value}", key, value);
            }
        }

        public override async Task DeleteRange(List<LanguageRequestDso> entities)
        {
            try
            {
                var builddtos = entities.OfType<LanguageRequestShareDto>().ToList();
                _logger.LogInformation("Deleting {Count} Languages...", 201);
                await _share.DeleteRange(builddtos);
                _logger.LogInformation("{Count} Languages deleted successfully.", 202);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting multiple Languages.");
            }
        }

        public override async Task<PagedResponse<LanguageResponseDso>> GetAllByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving all Language entities...");
                var results = await _share.GetAllAsync();
                var response = await _share.GetAllByAsync(conditions, options);
                return response.ToResponse(GetMapper().Map<IEnumerable<LanguageResponseDso>>(response.Data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for Language entities.");
                return null;
            }
        }

        public override async Task<LanguageResponseDso?> GetOneByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving Language entity...");
                return GetMapper().Map<LanguageResponseDso>(await _share.GetOneByAsync(conditions, options));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetOneByAsync  for Language entity.");
                return null;
            }
        }
    }
}