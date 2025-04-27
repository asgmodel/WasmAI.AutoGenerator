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
    public class CategoryTabService : BaseService<CategoryTabRequestDso, CategoryTabResponseDso>, IUseCategoryTabService
    {
        private readonly ICategoryTabShareRepository _share;
        public CategoryTabService(ICategoryTabShareRepository buildCategoryTabShareRepository, IMapper mapper, ILoggerFactory logger) : base(mapper, logger)
        {
            _share = buildCategoryTabShareRepository;
        }

        public override Task<int> CountAsync()
        {
            try
            {
                _logger.LogInformation("Counting CategoryTab entities...");
                return _share.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CountAsync for CategoryTab entities.");
                return Task.FromResult(0);
            }
        }

        public override async Task<CategoryTabResponseDso> CreateAsync(CategoryTabRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Creating new CategoryTab entity...");
                var result = await _share.CreateAsync(entity);
                var output = GetMapper().Map<CategoryTabResponseDso>(result);
                _logger.LogInformation("Created CategoryTab entity successfully.");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating CategoryTab entity.");
                return null;
            }
        }

        public override Task DeleteAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Deleting CategoryTab entity with ID: {id}...");
                return _share.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting CategoryTab entity with ID: {id}.");
                return Task.CompletedTask;
            }
        }

        public override async Task<IEnumerable<CategoryTabResponseDso>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all CategoryTab entities...");
                var results = await _share.GetAllAsync();
                return GetMapper().Map<IEnumerable<CategoryTabResponseDso>>(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for CategoryTab entities.");
                return null;
            }
        }

        public override async Task<CategoryTabResponseDso?> GetByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Retrieving CategoryTab entity with ID: {id}...");
                var result = await _share.GetByIdAsync(id);
                var item = GetMapper().Map<CategoryTabResponseDso>(result);
                _logger.LogInformation("Retrieved CategoryTab entity successfully.");
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetByIdAsync for CategoryTab entity with ID: {id}.");
                return null;
            }
        }

        public override IQueryable<CategoryTabResponseDso> GetQueryable()
        {
            try
            {
                _logger.LogInformation("Retrieving IQueryable<CategoryTabResponseDso> for CategoryTab entities...");
                var queryable = _share.GetQueryable();
                var result = GetMapper().ProjectTo<CategoryTabResponseDso>(queryable);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetQueryable for CategoryTab entities.");
                return null;
            }
        }

        public override async Task<CategoryTabResponseDso> UpdateAsync(CategoryTabRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Updating CategoryTab entity...");
                var result = await _share.UpdateAsync(entity);
                return GetMapper().Map<CategoryTabResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync for CategoryTab entity.");
                return null;
            }
        }

        public override async Task<bool> ExistsAsync(object value, string name = "Id")
        {
            try
            {
                _logger.LogInformation("Checking if CategoryTab exists with {Key}: {Value}", name, value);
                var exists = await _share.ExistsAsync(value, name);
                if (!exists)
                {
                    _logger.LogWarning("CategoryTab not found with {Key}: {Value}", name, value);
                }

                return exists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking existence of CategoryTab with {Key}: {Value}", name, value);
                return false;
            }
        }

        public override async Task<PagedResponse<CategoryTabResponseDso>> GetAllAsync(string[]? includes = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                _logger.LogInformation("Fetching all CategoryTabs with pagination: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
                var results = (await _share.GetAllAsync(includes, pageNumber, pageSize));
                var items = GetMapper().Map<List<CategoryTabResponseDso>>(results.Data);
                return new PagedResponse<CategoryTabResponseDso>(items, results.PageNumber, results.PageSize, results.TotalPages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all CategoryTabs.");
                return new PagedResponse<CategoryTabResponseDso>(new List<CategoryTabResponseDso>(), pageNumber, pageSize, 0);
            }
        }

        public override async Task<CategoryTabResponseDso?> GetByIdAsync(object id)
        {
            try
            {
                _logger.LogInformation("Fetching CategoryTab by ID: {Id}", id);
                var result = await _share.GetByIdAsync(id);
                if (result == null)
                {
                    _logger.LogWarning("CategoryTab not found with ID: {Id}", id);
                    return null;
                }

                _logger.LogInformation("Retrieved CategoryTab successfully.");
                return GetMapper().Map<CategoryTabResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving CategoryTab by ID: {Id}", id);
                return null;
            }
        }

        public override async Task DeleteAsync(object value, string key = "Id")
        {
            try
            {
                _logger.LogInformation("Deleting CategoryTab with {Key}: {Value}", key, value);
                await _share.DeleteAsync(value, key);
                _logger.LogInformation("CategoryTab with {Key}: {Value} deleted successfully.", key, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting CategoryTab with {Key}: {Value}", key, value);
            }
        }

        public override async Task DeleteRange(List<CategoryTabRequestDso> entities)
        {
            try
            {
                var builddtos = entities.OfType<CategoryTabRequestShareDto>().ToList();
                _logger.LogInformation("Deleting {Count} CategoryTabs...", 201);
                await _share.DeleteRange(builddtos);
                _logger.LogInformation("{Count} CategoryTabs deleted successfully.", 202);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting multiple CategoryTabs.");
            }
        }

        public override async Task<PagedResponse<CategoryTabResponseDso>> GetAllByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving all CategoryTab entities...");
                var results = await _share.GetAllAsync();
                var response = await _share.GetAllByAsync(conditions, options);
                return response.ToResponse(GetMapper().Map<IEnumerable<CategoryTabResponseDso>>(response.Data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for CategoryTab entities.");
                return null;
            }
        }

        public override async Task<CategoryTabResponseDso?> GetOneByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving CategoryTab entity...");
                return GetMapper().Map<CategoryTabResponseDso>(await _share.GetOneByAsync(conditions, options));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetOneByAsync  for CategoryTab entity.");
                return null;
            }
        }
    }
}