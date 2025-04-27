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
    public class CategoryModelService : BaseService<CategoryModelRequestDso, CategoryModelResponseDso>, IUseCategoryModelService
    {
        private readonly ICategoryModelShareRepository _share;
        public CategoryModelService(ICategoryModelShareRepository buildCategoryModelShareRepository, IMapper mapper, ILoggerFactory logger) : base(mapper, logger)
        {
            _share = buildCategoryModelShareRepository;
        }

        public override Task<int> CountAsync()
        {
            try
            {
                _logger.LogInformation("Counting CategoryModel entities...");
                return _share.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CountAsync for CategoryModel entities.");
                return Task.FromResult(0);
            }
        }

        public override async Task<CategoryModelResponseDso> CreateAsync(CategoryModelRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Creating new CategoryModel entity...");
                var result = await _share.CreateAsync(entity);
                var output = GetMapper().Map<CategoryModelResponseDso>(result);
                _logger.LogInformation("Created CategoryModel entity successfully.");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating CategoryModel entity.");
                return null;
            }
        }

        public override Task DeleteAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Deleting CategoryModel entity with ID: {id}...");
                return _share.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting CategoryModel entity with ID: {id}.");
                return Task.CompletedTask;
            }
        }

        public override async Task<IEnumerable<CategoryModelResponseDso>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all CategoryModel entities...");
                var results = await _share.GetAllAsync();
                return GetMapper().Map<IEnumerable<CategoryModelResponseDso>>(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for CategoryModel entities.");
                return null;
            }
        }

        public override async Task<CategoryModelResponseDso?> GetByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Retrieving CategoryModel entity with ID: {id}...");
                var result = await _share.GetByIdAsync(id);
                var item = GetMapper().Map<CategoryModelResponseDso>(result);
                _logger.LogInformation("Retrieved CategoryModel entity successfully.");
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetByIdAsync for CategoryModel entity with ID: {id}.");
                return null;
            }
        }

        public override IQueryable<CategoryModelResponseDso> GetQueryable()
        {
            try
            {
                _logger.LogInformation("Retrieving IQueryable<CategoryModelResponseDso> for CategoryModel entities...");
                var queryable = _share.GetQueryable();
                var result = GetMapper().ProjectTo<CategoryModelResponseDso>(queryable);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetQueryable for CategoryModel entities.");
                return null;
            }
        }

        public override async Task<CategoryModelResponseDso> UpdateAsync(CategoryModelRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Updating CategoryModel entity...");
                var result = await _share.UpdateAsync(entity);
                return GetMapper().Map<CategoryModelResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync for CategoryModel entity.");
                return null;
            }
        }

        public override async Task<bool> ExistsAsync(object value, string name = "Id")
        {
            try
            {
                _logger.LogInformation("Checking if CategoryModel exists with {Key}: {Value}", name, value);
                var exists = await _share.ExistsAsync(value, name);
                if (!exists)
                {
                    _logger.LogWarning("CategoryModel not found with {Key}: {Value}", name, value);
                }

                return exists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking existence of CategoryModel with {Key}: {Value}", name, value);
                return false;
            }
        }

        public override async Task<PagedResponse<CategoryModelResponseDso>> GetAllAsync(string[]? includes = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                _logger.LogInformation("Fetching all CategoryModels with pagination: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
                var results = (await _share.GetAllAsync(includes, pageNumber, pageSize));
                var items = GetMapper().Map<List<CategoryModelResponseDso>>(results.Data);
                return new PagedResponse<CategoryModelResponseDso>(items, results.PageNumber, results.PageSize, results.TotalPages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all CategoryModels.");
                return new PagedResponse<CategoryModelResponseDso>(new List<CategoryModelResponseDso>(), pageNumber, pageSize, 0);
            }
        }

        public override async Task<CategoryModelResponseDso?> GetByIdAsync(object id)
        {
            try
            {
                _logger.LogInformation("Fetching CategoryModel by ID: {Id}", id);
                var result = await _share.GetByIdAsync(id);
                if (result == null)
                {
                    _logger.LogWarning("CategoryModel not found with ID: {Id}", id);
                    return null;
                }

                _logger.LogInformation("Retrieved CategoryModel successfully.");
                return GetMapper().Map<CategoryModelResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving CategoryModel by ID: {Id}", id);
                return null;
            }
        }

        public override async Task DeleteAsync(object value, string key = "Id")
        {
            try
            {
                _logger.LogInformation("Deleting CategoryModel with {Key}: {Value}", key, value);
                await _share.DeleteAsync(value, key);
                _logger.LogInformation("CategoryModel with {Key}: {Value} deleted successfully.", key, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting CategoryModel with {Key}: {Value}", key, value);
            }
        }

        public override async Task DeleteRange(List<CategoryModelRequestDso> entities)
        {
            try
            {
                var builddtos = entities.OfType<CategoryModelRequestShareDto>().ToList();
                _logger.LogInformation("Deleting {Count} CategoryModels...", 201);
                await _share.DeleteRange(builddtos);
                _logger.LogInformation("{Count} CategoryModels deleted successfully.", 202);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting multiple CategoryModels.");
            }
        }

        public override async Task<PagedResponse<CategoryModelResponseDso>> GetAllByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving all CategoryModel entities...");
                var results = await _share.GetAllAsync();
                var response = await _share.GetAllByAsync(conditions, options);
                return response.ToResponse(GetMapper().Map<IEnumerable<CategoryModelResponseDso>>(response.Data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for CategoryModel entities.");
                return null;
            }
        }

        public override async Task<CategoryModelResponseDso?> GetOneByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving CategoryModel entity...");
                return GetMapper().Map<CategoryModelResponseDso>(await _share.GetOneByAsync(conditions, options));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetOneByAsync  for CategoryModel entity.");
                return null;
            }
        }
    }
}