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
    public class ModelAiService : BaseService<ModelAiRequestDso, ModelAiResponseDso>, IUseModelAiService
    {
        private readonly IModelAiShareRepository _share;
        public ModelAiService(IModelAiShareRepository buildModelAiShareRepository, IMapper mapper, ILoggerFactory logger) : base(mapper, logger)
        {
            _share = buildModelAiShareRepository;
        }

        public override Task<int> CountAsync()
        {
            try
            {
                _logger.LogInformation("Counting ModelAi entities...");
                return _share.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CountAsync for ModelAi entities.");
                return Task.FromResult(0);
            }
        }

        public override async Task<ModelAiResponseDso> CreateAsync(ModelAiRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Creating new ModelAi entity...");
                var result = await _share.CreateAsync(entity);
                var output = GetMapper().Map<ModelAiResponseDso>(result);
                _logger.LogInformation("Created ModelAi entity successfully.");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating ModelAi entity.");
                return null;
            }
        }

        public override Task DeleteAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Deleting ModelAi entity with ID: {id}...");
                return _share.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting ModelAi entity with ID: {id}.");
                return Task.CompletedTask;
            }
        }

        public override async Task<IEnumerable<ModelAiResponseDso>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all ModelAi entities...");
                var results = await _share.GetAllAsync();
                return GetMapper().Map<IEnumerable<ModelAiResponseDso>>(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for ModelAi entities.");
                return null;
            }
        }

        public override async Task<ModelAiResponseDso?> GetByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Retrieving ModelAi entity with ID: {id}...");
                var result = await _share.GetByIdAsync(id);
                var item = GetMapper().Map<ModelAiResponseDso>(result);
                _logger.LogInformation("Retrieved ModelAi entity successfully.");
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetByIdAsync for ModelAi entity with ID: {id}.");
                return null;
            }
        }

        public override IQueryable<ModelAiResponseDso> GetQueryable()
        {
            try
            {
                _logger.LogInformation("Retrieving IQueryable<ModelAiResponseDso> for ModelAi entities...");
                var queryable = _share.GetQueryable();
                var result = GetMapper().ProjectTo<ModelAiResponseDso>(queryable);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetQueryable for ModelAi entities.");
                return null;
            }
        }

        public override async Task<ModelAiResponseDso> UpdateAsync(ModelAiRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Updating ModelAi entity...");
                var result = await _share.UpdateAsync(entity);
                return GetMapper().Map<ModelAiResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync for ModelAi entity.");
                return null;
            }
        }

        public override async Task<bool> ExistsAsync(object value, string name = "Id")
        {
            try
            {
                _logger.LogInformation("Checking if ModelAi exists with {Key}: {Value}", name, value);
                var exists = await _share.ExistsAsync(value, name);
                if (!exists)
                {
                    _logger.LogWarning("ModelAi not found with {Key}: {Value}", name, value);
                }

                return exists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking existence of ModelAi with {Key}: {Value}", name, value);
                return false;
            }
        }

        public override async Task<PagedResponse<ModelAiResponseDso>> GetAllAsync(string[]? includes = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                _logger.LogInformation("Fetching all ModelAis with pagination: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
                var results = (await _share.GetAllAsync(includes, pageNumber, pageSize));
                var items = GetMapper().Map<List<ModelAiResponseDso>>(results.Data);
                return new PagedResponse<ModelAiResponseDso>(items, results.PageNumber, results.PageSize, results.TotalPages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all ModelAis.");
                return new PagedResponse<ModelAiResponseDso>(new List<ModelAiResponseDso>(), pageNumber, pageSize, 0);
            }
        }

        public override async Task<ModelAiResponseDso?> GetByIdAsync(object id)
        {
            try
            {
                _logger.LogInformation("Fetching ModelAi by ID: {Id}", id);
                var result = await _share.GetByIdAsync(id);
                if (result == null)
                {
                    _logger.LogWarning("ModelAi not found with ID: {Id}", id);
                    return null;
                }

                _logger.LogInformation("Retrieved ModelAi successfully.");
                return GetMapper().Map<ModelAiResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving ModelAi by ID: {Id}", id);
                return null;
            }
        }

        public override async Task DeleteAsync(object value, string key = "Id")
        {
            try
            {
                _logger.LogInformation("Deleting ModelAi with {Key}: {Value}", key, value);
                await _share.DeleteAsync(value, key);
                _logger.LogInformation("ModelAi with {Key}: {Value} deleted successfully.", key, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting ModelAi with {Key}: {Value}", key, value);
            }
        }

        public override async Task DeleteRange(List<ModelAiRequestDso> entities)
        {
            try
            {
                var builddtos = entities.OfType<ModelAiRequestShareDto>().ToList();
                _logger.LogInformation("Deleting {Count} ModelAis...", 201);
                await _share.DeleteRange(builddtos);
                _logger.LogInformation("{Count} ModelAis deleted successfully.", 202);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting multiple ModelAis.");
            }
        }

        public override async Task<PagedResponse<ModelAiResponseDso>> GetAllByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving all ModelAi entities...");
                var results = await _share.GetAllAsync();
                var response = await _share.GetAllByAsync(conditions, options);
                return response.ToResponse(GetMapper().Map<IEnumerable<ModelAiResponseDso>>(response.Data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for ModelAi entities.");
                return null;
            }
        }

        public override async Task<ModelAiResponseDso?> GetOneByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving ModelAi entity...");
                return GetMapper().Map<ModelAiResponseDso>(await _share.GetOneByAsync(conditions, options));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetOneByAsync  for ModelAi entity.");
                return null;
            }
        }
    }
}