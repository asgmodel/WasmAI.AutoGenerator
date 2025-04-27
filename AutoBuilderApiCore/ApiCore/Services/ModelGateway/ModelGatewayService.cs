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
    public class ModelGatewayService : BaseService<ModelGatewayRequestDso, ModelGatewayResponseDso>, IUseModelGatewayService
    {
        private readonly IModelGatewayShareRepository _share;
        public ModelGatewayService(IModelGatewayShareRepository buildModelGatewayShareRepository, IMapper mapper, ILoggerFactory logger) : base(mapper, logger)
        {
            _share = buildModelGatewayShareRepository;
        }

        public override Task<int> CountAsync()
        {
            try
            {
                _logger.LogInformation("Counting ModelGateway entities...");
                return _share.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CountAsync for ModelGateway entities.");
                return Task.FromResult(0);
            }
        }

        public override async Task<ModelGatewayResponseDso> CreateAsync(ModelGatewayRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Creating new ModelGateway entity...");
                var result = await _share.CreateAsync(entity);
                var output = GetMapper().Map<ModelGatewayResponseDso>(result);
                _logger.LogInformation("Created ModelGateway entity successfully.");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating ModelGateway entity.");
                return null;
            }
        }

        public override Task DeleteAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Deleting ModelGateway entity with ID: {id}...");
                return _share.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting ModelGateway entity with ID: {id}.");
                return Task.CompletedTask;
            }
        }

        public override async Task<IEnumerable<ModelGatewayResponseDso>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all ModelGateway entities...");
                var results = await _share.GetAllAsync();
                return GetMapper().Map<IEnumerable<ModelGatewayResponseDso>>(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for ModelGateway entities.");
                return null;
            }
        }

        public override async Task<ModelGatewayResponseDso?> GetByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Retrieving ModelGateway entity with ID: {id}...");
                var result = await _share.GetByIdAsync(id);
                var item = GetMapper().Map<ModelGatewayResponseDso>(result);
                _logger.LogInformation("Retrieved ModelGateway entity successfully.");
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetByIdAsync for ModelGateway entity with ID: {id}.");
                return null;
            }
        }

        public override IQueryable<ModelGatewayResponseDso> GetQueryable()
        {
            try
            {
                _logger.LogInformation("Retrieving IQueryable<ModelGatewayResponseDso> for ModelGateway entities...");
                var queryable = _share.GetQueryable();
                var result = GetMapper().ProjectTo<ModelGatewayResponseDso>(queryable);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetQueryable for ModelGateway entities.");
                return null;
            }
        }

        public override async Task<ModelGatewayResponseDso> UpdateAsync(ModelGatewayRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Updating ModelGateway entity...");
                var result = await _share.UpdateAsync(entity);
                return GetMapper().Map<ModelGatewayResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync for ModelGateway entity.");
                return null;
            }
        }

        public override async Task<bool> ExistsAsync(object value, string name = "Id")
        {
            try
            {
                _logger.LogInformation("Checking if ModelGateway exists with {Key}: {Value}", name, value);
                var exists = await _share.ExistsAsync(value, name);
                if (!exists)
                {
                    _logger.LogWarning("ModelGateway not found with {Key}: {Value}", name, value);
                }

                return exists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking existence of ModelGateway with {Key}: {Value}", name, value);
                return false;
            }
        }

        public override async Task<PagedResponse<ModelGatewayResponseDso>> GetAllAsync(string[]? includes = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                _logger.LogInformation("Fetching all ModelGateways with pagination: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
                var results = (await _share.GetAllAsync(includes, pageNumber, pageSize));
                var items = GetMapper().Map<List<ModelGatewayResponseDso>>(results.Data);
                return new PagedResponse<ModelGatewayResponseDso>(items, results.PageNumber, results.PageSize, results.TotalPages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all ModelGateways.");
                return new PagedResponse<ModelGatewayResponseDso>(new List<ModelGatewayResponseDso>(), pageNumber, pageSize, 0);
            }
        }

        public override async Task<ModelGatewayResponseDso?> GetByIdAsync(object id)
        {
            try
            {
                _logger.LogInformation("Fetching ModelGateway by ID: {Id}", id);
                var result = await _share.GetByIdAsync(id);
                if (result == null)
                {
                    _logger.LogWarning("ModelGateway not found with ID: {Id}", id);
                    return null;
                }

                _logger.LogInformation("Retrieved ModelGateway successfully.");
                return GetMapper().Map<ModelGatewayResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving ModelGateway by ID: {Id}", id);
                return null;
            }
        }

        public override async Task DeleteAsync(object value, string key = "Id")
        {
            try
            {
                _logger.LogInformation("Deleting ModelGateway with {Key}: {Value}", key, value);
                await _share.DeleteAsync(value, key);
                _logger.LogInformation("ModelGateway with {Key}: {Value} deleted successfully.", key, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting ModelGateway with {Key}: {Value}", key, value);
            }
        }

        public override async Task DeleteRange(List<ModelGatewayRequestDso> entities)
        {
            try
            {
                var builddtos = entities.OfType<ModelGatewayRequestShareDto>().ToList();
                _logger.LogInformation("Deleting {Count} ModelGateways...", 201);
                await _share.DeleteRange(builddtos);
                _logger.LogInformation("{Count} ModelGateways deleted successfully.", 202);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting multiple ModelGateways.");
            }
        }

        public override async Task<PagedResponse<ModelGatewayResponseDso>> GetAllByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving all ModelGateway entities...");
                var results = await _share.GetAllAsync();
                var response = await _share.GetAllByAsync(conditions, options);
                return response.ToResponse(GetMapper().Map<IEnumerable<ModelGatewayResponseDso>>(response.Data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for ModelGateway entities.");
                return null;
            }
        }

        public override async Task<ModelGatewayResponseDso?> GetOneByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving ModelGateway entity...");
                return GetMapper().Map<ModelGatewayResponseDso>(await _share.GetOneByAsync(conditions, options));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetOneByAsync  for ModelGateway entity.");
                return null;
            }
        }
    }
}