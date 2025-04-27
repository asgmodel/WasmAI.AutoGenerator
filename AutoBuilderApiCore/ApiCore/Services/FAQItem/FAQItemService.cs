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
    public class FAQItemService : BaseService<FAQItemRequestDso, FAQItemResponseDso>, IUseFAQItemService
    {
        private readonly IFAQItemShareRepository _share;
        public FAQItemService(IFAQItemShareRepository buildFAQItemShareRepository, IMapper mapper, ILoggerFactory logger) : base(mapper, logger)
        {
            _share = buildFAQItemShareRepository;
        }

        public override Task<int> CountAsync()
        {
            try
            {
                _logger.LogInformation("Counting FAQItem entities...");
                return _share.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CountAsync for FAQItem entities.");
                return Task.FromResult(0);
            }
        }

        public override async Task<FAQItemResponseDso> CreateAsync(FAQItemRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Creating new FAQItem entity...");
                var result = await _share.CreateAsync(entity);
                var output = GetMapper().Map<FAQItemResponseDso>(result);
                _logger.LogInformation("Created FAQItem entity successfully.");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating FAQItem entity.");
                return null;
            }
        }

        public override Task DeleteAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Deleting FAQItem entity with ID: {id}...");
                return _share.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting FAQItem entity with ID: {id}.");
                return Task.CompletedTask;
            }
        }

        public override async Task<IEnumerable<FAQItemResponseDso>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all FAQItem entities...");
                var results = await _share.GetAllAsync();
                return GetMapper().Map<IEnumerable<FAQItemResponseDso>>(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for FAQItem entities.");
                return null;
            }
        }

        public override async Task<FAQItemResponseDso?> GetByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Retrieving FAQItem entity with ID: {id}...");
                var result = await _share.GetByIdAsync(id);
                var item = GetMapper().Map<FAQItemResponseDso>(result);
                _logger.LogInformation("Retrieved FAQItem entity successfully.");
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetByIdAsync for FAQItem entity with ID: {id}.");
                return null;
            }
        }

        public override IQueryable<FAQItemResponseDso> GetQueryable()
        {
            try
            {
                _logger.LogInformation("Retrieving IQueryable<FAQItemResponseDso> for FAQItem entities...");
                var queryable = _share.GetQueryable();
                var result = GetMapper().ProjectTo<FAQItemResponseDso>(queryable);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetQueryable for FAQItem entities.");
                return null;
            }
        }

        public override async Task<FAQItemResponseDso> UpdateAsync(FAQItemRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Updating FAQItem entity...");
                var result = await _share.UpdateAsync(entity);
                return GetMapper().Map<FAQItemResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync for FAQItem entity.");
                return null;
            }
        }

        public override async Task<bool> ExistsAsync(object value, string name = "Id")
        {
            try
            {
                _logger.LogInformation("Checking if FAQItem exists with {Key}: {Value}", name, value);
                var exists = await _share.ExistsAsync(value, name);
                if (!exists)
                {
                    _logger.LogWarning("FAQItem not found with {Key}: {Value}", name, value);
                }

                return exists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking existence of FAQItem with {Key}: {Value}", name, value);
                return false;
            }
        }

        public override async Task<PagedResponse<FAQItemResponseDso>> GetAllAsync(string[]? includes = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                _logger.LogInformation("Fetching all FAQItems with pagination: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
                var results = (await _share.GetAllAsync(includes, pageNumber, pageSize));
                var items = GetMapper().Map<List<FAQItemResponseDso>>(results.Data);
                return new PagedResponse<FAQItemResponseDso>(items, results.PageNumber, results.PageSize, results.TotalPages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all FAQItems.");
                return new PagedResponse<FAQItemResponseDso>(new List<FAQItemResponseDso>(), pageNumber, pageSize, 0);
            }
        }

        public override async Task<FAQItemResponseDso?> GetByIdAsync(object id)
        {
            try
            {
                _logger.LogInformation("Fetching FAQItem by ID: {Id}", id);
                var result = await _share.GetByIdAsync(id);
                if (result == null)
                {
                    _logger.LogWarning("FAQItem not found with ID: {Id}", id);
                    return null;
                }

                _logger.LogInformation("Retrieved FAQItem successfully.");
                return GetMapper().Map<FAQItemResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving FAQItem by ID: {Id}", id);
                return null;
            }
        }

        public override async Task DeleteAsync(object value, string key = "Id")
        {
            try
            {
                _logger.LogInformation("Deleting FAQItem with {Key}: {Value}", key, value);
                await _share.DeleteAsync(value, key);
                _logger.LogInformation("FAQItem with {Key}: {Value} deleted successfully.", key, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting FAQItem with {Key}: {Value}", key, value);
            }
        }

        public override async Task DeleteRange(List<FAQItemRequestDso> entities)
        {
            try
            {
                var builddtos = entities.OfType<FAQItemRequestShareDto>().ToList();
                _logger.LogInformation("Deleting {Count} FAQItems...", 201);
                await _share.DeleteRange(builddtos);
                _logger.LogInformation("{Count} FAQItems deleted successfully.", 202);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting multiple FAQItems.");
            }
        }

        public override async Task<PagedResponse<FAQItemResponseDso>> GetAllByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving all FAQItem entities...");
                var results = await _share.GetAllAsync();
                var response = await _share.GetAllByAsync(conditions, options);
                return response.ToResponse(GetMapper().Map<IEnumerable<FAQItemResponseDso>>(response.Data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for FAQItem entities.");
                return null;
            }
        }

        public override async Task<FAQItemResponseDso?> GetOneByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving FAQItem entity...");
                return GetMapper().Map<FAQItemResponseDso>(await _share.GetOneByAsync(conditions, options));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetOneByAsync  for FAQItem entity.");
                return null;
            }
        }
    }
}