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
    public class ApplicationUserService : BaseService<ApplicationUserRequestDso, ApplicationUserResponseDso>, IUseApplicationUserService
    {
        private readonly IApplicationUserShareRepository _share;
        public ApplicationUserService(IApplicationUserShareRepository buildApplicationUserShareRepository, IMapper mapper, ILoggerFactory logger) : base(mapper, logger)
        {
            _share = buildApplicationUserShareRepository;
        }

        public override Task<int> CountAsync()
        {
            try
            {
                _logger.LogInformation("Counting ApplicationUser entities...");
                return _share.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CountAsync for ApplicationUser entities.");
                return Task.FromResult(0);
            }
        }

        public override async Task<ApplicationUserResponseDso> CreateAsync(ApplicationUserRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Creating new ApplicationUser entity...");
                var result = await _share.CreateAsync(entity);
                var output = GetMapper().Map<ApplicationUserResponseDso>(result);
                _logger.LogInformation("Created ApplicationUser entity successfully.");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating ApplicationUser entity.");
                return null;
            }
        }

        public override Task DeleteAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Deleting ApplicationUser entity with ID: {id}...");
                return _share.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting ApplicationUser entity with ID: {id}.");
                return Task.CompletedTask;
            }
        }

        public override async Task<IEnumerable<ApplicationUserResponseDso>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all ApplicationUser entities...");
                var results = await _share.GetAllAsync();
                return GetMapper().Map<IEnumerable<ApplicationUserResponseDso>>(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for ApplicationUser entities.");
                return null;
            }
        }

        public override async Task<ApplicationUserResponseDso?> GetByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Retrieving ApplicationUser entity with ID: {id}...");
                var result = await _share.GetByIdAsync(id);
                var item = GetMapper().Map<ApplicationUserResponseDso>(result);
                _logger.LogInformation("Retrieved ApplicationUser entity successfully.");
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetByIdAsync for ApplicationUser entity with ID: {id}.");
                return null;
            }
        }

        public override IQueryable<ApplicationUserResponseDso> GetQueryable()
        {
            try
            {
                _logger.LogInformation("Retrieving IQueryable<ApplicationUserResponseDso> for ApplicationUser entities...");
                var queryable = _share.GetQueryable();
                var result = GetMapper().ProjectTo<ApplicationUserResponseDso>(queryable);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetQueryable for ApplicationUser entities.");
                return null;
            }
        }

        public override async Task<ApplicationUserResponseDso> UpdateAsync(ApplicationUserRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Updating ApplicationUser entity...");
                var result = await _share.UpdateAsync(entity);
                return GetMapper().Map<ApplicationUserResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync for ApplicationUser entity.");
                return null;
            }
        }

        public override async Task<bool> ExistsAsync(object value, string name = "Id")
        {
            try
            {
                _logger.LogInformation("Checking if ApplicationUser exists with {Key}: {Value}", name, value);
                var exists = await _share.ExistsAsync(value, name);
                if (!exists)
                {
                    _logger.LogWarning("ApplicationUser not found with {Key}: {Value}", name, value);
                }

                return exists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking existence of ApplicationUser with {Key}: {Value}", name, value);
                return false;
            }
        }

        public override async Task<PagedResponse<ApplicationUserResponseDso>> GetAllAsync(string[]? includes = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                _logger.LogInformation("Fetching all ApplicationUsers with pagination: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
                var results = (await _share.GetAllAsync(includes, pageNumber, pageSize));
                var items = GetMapper().Map<List<ApplicationUserResponseDso>>(results.Data);
                return new PagedResponse<ApplicationUserResponseDso>(items, results.PageNumber, results.PageSize, results.TotalPages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all ApplicationUsers.");
                return new PagedResponse<ApplicationUserResponseDso>(new List<ApplicationUserResponseDso>(), pageNumber, pageSize, 0);
            }
        }

        public override async Task<ApplicationUserResponseDso?> GetByIdAsync(object id)
        {
            try
            {
                _logger.LogInformation("Fetching ApplicationUser by ID: {Id}", id);
                var result = await _share.GetByIdAsync(id);
                if (result == null)
                {
                    _logger.LogWarning("ApplicationUser not found with ID: {Id}", id);
                    return null;
                }

                _logger.LogInformation("Retrieved ApplicationUser successfully.");
                return GetMapper().Map<ApplicationUserResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving ApplicationUser by ID: {Id}", id);
                return null;
            }
        }

        public override async Task DeleteAsync(object value, string key = "Id")
        {
            try
            {
                _logger.LogInformation("Deleting ApplicationUser with {Key}: {Value}", key, value);
                await _share.DeleteAsync(value, key);
                _logger.LogInformation("ApplicationUser with {Key}: {Value} deleted successfully.", key, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting ApplicationUser with {Key}: {Value}", key, value);
            }
        }

        public override async Task DeleteRange(List<ApplicationUserRequestDso> entities)
        {
            try
            {
                var builddtos = entities.OfType<ApplicationUserRequestShareDto>().ToList();
                _logger.LogInformation("Deleting {Count} ApplicationUsers...", 201);
                await _share.DeleteRange(builddtos);
                _logger.LogInformation("{Count} ApplicationUsers deleted successfully.", 202);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting multiple ApplicationUsers.");
            }
        }

        public override async Task<PagedResponse<ApplicationUserResponseDso>> GetAllByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving all ApplicationUser entities...");
                var results = await _share.GetAllAsync();
                var response = await _share.GetAllByAsync(conditions, options);
                return response.ToResponse(GetMapper().Map<IEnumerable<ApplicationUserResponseDso>>(response.Data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for ApplicationUser entities.");
                return null;
            }
        }

        public override async Task<ApplicationUserResponseDso?> GetOneByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving ApplicationUser entity...");
                return GetMapper().Map<ApplicationUserResponseDso>(await _share.GetOneByAsync(conditions, options));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetOneByAsync  for ApplicationUser entity.");
                return null;
            }
        }
    }
}