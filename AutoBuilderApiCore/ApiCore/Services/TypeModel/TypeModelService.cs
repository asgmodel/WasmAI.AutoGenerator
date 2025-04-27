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
    public class TypeModelService : BaseService<TypeModelRequestDso, TypeModelResponseDso>, IUseTypeModelService
    {
        private readonly ITypeModelShareRepository _share;
        public TypeModelService(ITypeModelShareRepository buildTypeModelShareRepository, IMapper mapper, ILoggerFactory logger) : base(mapper, logger)
        {
            _share = buildTypeModelShareRepository;
        }

        public override Task<int> CountAsync()
        {
            try
            {
                _logger.LogInformation("Counting TypeModel entities...");
                return _share.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CountAsync for TypeModel entities.");
                return Task.FromResult(0);
            }
        }

        public override async Task<TypeModelResponseDso> CreateAsync(TypeModelRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Creating new TypeModel entity...");
                var result = await _share.CreateAsync(entity);
                var output = GetMapper().Map<TypeModelResponseDso>(result);
                _logger.LogInformation("Created TypeModel entity successfully.");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating TypeModel entity.");
                return null;
            }
        }

        public override Task DeleteAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Deleting TypeModel entity with ID: {id}...");
                return _share.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting TypeModel entity with ID: {id}.");
                return Task.CompletedTask;
            }
        }

        public override async Task<IEnumerable<TypeModelResponseDso>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all TypeModel entities...");
                var results = await _share.GetAllAsync();
                return GetMapper().Map<IEnumerable<TypeModelResponseDso>>(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for TypeModel entities.");
                return null;
            }
        }

        public override async Task<TypeModelResponseDso?> GetByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Retrieving TypeModel entity with ID: {id}...");
                var result = await _share.GetByIdAsync(id);
                var item = GetMapper().Map<TypeModelResponseDso>(result);
                _logger.LogInformation("Retrieved TypeModel entity successfully.");
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetByIdAsync for TypeModel entity with ID: {id}.");
                return null;
            }
        }

        public override IQueryable<TypeModelResponseDso> GetQueryable()
        {
            try
            {
                _logger.LogInformation("Retrieving IQueryable<TypeModelResponseDso> for TypeModel entities...");
                var queryable = _share.GetQueryable();
                var result = GetMapper().ProjectTo<TypeModelResponseDso>(queryable);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetQueryable for TypeModel entities.");
                return null;
            }
        }

        public override async Task<TypeModelResponseDso> UpdateAsync(TypeModelRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Updating TypeModel entity...");
                var result = await _share.UpdateAsync(entity);
                return GetMapper().Map<TypeModelResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync for TypeModel entity.");
                return null;
            }
        }

        public override async Task<bool> ExistsAsync(object value, string name = "Id")
        {
            try
            {
                _logger.LogInformation("Checking if TypeModel exists with {Key}: {Value}", name, value);
                var exists = await _share.ExistsAsync(value, name);
                if (!exists)
                {
                    _logger.LogWarning("TypeModel not found with {Key}: {Value}", name, value);
                }

                return exists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking existence of TypeModel with {Key}: {Value}", name, value);
                return false;
            }
        }

        public override async Task<PagedResponse<TypeModelResponseDso>> GetAllAsync(string[]? includes = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                _logger.LogInformation("Fetching all TypeModels with pagination: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
                var results = (await _share.GetAllAsync(includes, pageNumber, pageSize));
                var items = GetMapper().Map<List<TypeModelResponseDso>>(results.Data);
                return new PagedResponse<TypeModelResponseDso>(items, results.PageNumber, results.PageSize, results.TotalPages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all TypeModels.");
                return new PagedResponse<TypeModelResponseDso>(new List<TypeModelResponseDso>(), pageNumber, pageSize, 0);
            }
        }

        public override async Task<TypeModelResponseDso?> GetByIdAsync(object id)
        {
            try
            {
                _logger.LogInformation("Fetching TypeModel by ID: {Id}", id);
                var result = await _share.GetByIdAsync(id);
                if (result == null)
                {
                    _logger.LogWarning("TypeModel not found with ID: {Id}", id);
                    return null;
                }

                _logger.LogInformation("Retrieved TypeModel successfully.");
                return GetMapper().Map<TypeModelResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving TypeModel by ID: {Id}", id);
                return null;
            }
        }

        public override async Task DeleteAsync(object value, string key = "Id")
        {
            try
            {
                _logger.LogInformation("Deleting TypeModel with {Key}: {Value}", key, value);
                await _share.DeleteAsync(value, key);
                _logger.LogInformation("TypeModel with {Key}: {Value} deleted successfully.", key, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting TypeModel with {Key}: {Value}", key, value);
            }
        }

        public override async Task DeleteRange(List<TypeModelRequestDso> entities)
        {
            try
            {
                var builddtos = entities.OfType<TypeModelRequestShareDto>().ToList();
                _logger.LogInformation("Deleting {Count} TypeModels...", 201);
                await _share.DeleteRange(builddtos);
                _logger.LogInformation("{Count} TypeModels deleted successfully.", 202);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting multiple TypeModels.");
            }
        }

        public override async Task<PagedResponse<TypeModelResponseDso>> GetAllByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving all TypeModel entities...");
                var results = await _share.GetAllAsync();
                var response = await _share.GetAllByAsync(conditions, options);
                return response.ToResponse(GetMapper().Map<IEnumerable<TypeModelResponseDso>>(response.Data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for TypeModel entities.");
                return null;
            }
        }

        public override async Task<TypeModelResponseDso?> GetOneByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving TypeModel entity...");
                return GetMapper().Map<TypeModelResponseDso>(await _share.GetOneByAsync(conditions, options));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetOneByAsync  for TypeModel entity.");
                return null;
            }
        }
    }
}