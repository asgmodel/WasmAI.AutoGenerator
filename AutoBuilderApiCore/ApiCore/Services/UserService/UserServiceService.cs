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
    public class UserServiceService : BaseService<UserServiceRequestDso, UserServiceResponseDso>, IUseUserServiceService
    {
        private readonly IUserServiceShareRepository _share;
        public UserServiceService(IUserServiceShareRepository buildUserServiceShareRepository, IMapper mapper, ILoggerFactory logger) : base(mapper, logger)
        {
            _share = buildUserServiceShareRepository;
        }

        public override Task<int> CountAsync()
        {
            try
            {
                _logger.LogInformation("Counting UserService entities...");
                return _share.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CountAsync for UserService entities.");
                return Task.FromResult(0);
            }
        }

        public override async Task<UserServiceResponseDso> CreateAsync(UserServiceRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Creating new UserService entity...");
                var result = await _share.CreateAsync(entity);
                var output = GetMapper().Map<UserServiceResponseDso>(result);
                _logger.LogInformation("Created UserService entity successfully.");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating UserService entity.");
                return null;
            }
        }

        public override Task DeleteAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Deleting UserService entity with ID: {id}...");
                return _share.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting UserService entity with ID: {id}.");
                return Task.CompletedTask;
            }
        }

        public override async Task<IEnumerable<UserServiceResponseDso>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all UserService entities...");
                var results = await _share.GetAllAsync();
                return GetMapper().Map<IEnumerable<UserServiceResponseDso>>(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for UserService entities.");
                return null;
            }
        }

        public override async Task<UserServiceResponseDso?> GetByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Retrieving UserService entity with ID: {id}...");
                var result = await _share.GetByIdAsync(id);
                var item = GetMapper().Map<UserServiceResponseDso>(result);
                _logger.LogInformation("Retrieved UserService entity successfully.");
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetByIdAsync for UserService entity with ID: {id}.");
                return null;
            }
        }

        public override IQueryable<UserServiceResponseDso> GetQueryable()
        {
            try
            {
                _logger.LogInformation("Retrieving IQueryable<UserServiceResponseDso> for UserService entities...");
                var queryable = _share.GetQueryable();
                var result = GetMapper().ProjectTo<UserServiceResponseDso>(queryable);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetQueryable for UserService entities.");
                return null;
            }
        }

        public override async Task<UserServiceResponseDso> UpdateAsync(UserServiceRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Updating UserService entity...");
                var result = await _share.UpdateAsync(entity);
                return GetMapper().Map<UserServiceResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync for UserService entity.");
                return null;
            }
        }

        public override async Task<bool> ExistsAsync(object value, string name = "Id")
        {
            try
            {
                _logger.LogInformation("Checking if UserService exists with {Key}: {Value}", name, value);
                var exists = await _share.ExistsAsync(value, name);
                if (!exists)
                {
                    _logger.LogWarning("UserService not found with {Key}: {Value}", name, value);
                }

                return exists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking existence of UserService with {Key}: {Value}", name, value);
                return false;
            }
        }

        public override async Task<PagedResponse<UserServiceResponseDso>> GetAllAsync(string[]? includes = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                _logger.LogInformation("Fetching all UserServices with pagination: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
                var results = (await _share.GetAllAsync(includes, pageNumber, pageSize));
                var items = GetMapper().Map<List<UserServiceResponseDso>>(results.Data);
                return new PagedResponse<UserServiceResponseDso>(items, results.PageNumber, results.PageSize, results.TotalPages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all UserServices.");
                return new PagedResponse<UserServiceResponseDso>(new List<UserServiceResponseDso>(), pageNumber, pageSize, 0);
            }
        }

        public override async Task<UserServiceResponseDso?> GetByIdAsync(object id)
        {
            try
            {
                _logger.LogInformation("Fetching UserService by ID: {Id}", id);
                var result = await _share.GetByIdAsync(id);
                if (result == null)
                {
                    _logger.LogWarning("UserService not found with ID: {Id}", id);
                    return null;
                }

                _logger.LogInformation("Retrieved UserService successfully.");
                return GetMapper().Map<UserServiceResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving UserService by ID: {Id}", id);
                return null;
            }
        }

        public override async Task DeleteAsync(object value, string key = "Id")
        {
            try
            {
                _logger.LogInformation("Deleting UserService with {Key}: {Value}", key, value);
                await _share.DeleteAsync(value, key);
                _logger.LogInformation("UserService with {Key}: {Value} deleted successfully.", key, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting UserService with {Key}: {Value}", key, value);
            }
        }

        public override async Task DeleteRange(List<UserServiceRequestDso> entities)
        {
            try
            {
                var builddtos = entities.OfType<UserServiceRequestShareDto>().ToList();
                _logger.LogInformation("Deleting {Count} UserServices...", 201);
                await _share.DeleteRange(builddtos);
                _logger.LogInformation("{Count} UserServices deleted successfully.", 202);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting multiple UserServices.");
            }
        }

        public override async Task<PagedResponse<UserServiceResponseDso>> GetAllByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving all UserService entities...");
                var results = await _share.GetAllAsync();
                var response = await _share.GetAllByAsync(conditions, options);
                return response.ToResponse(GetMapper().Map<IEnumerable<UserServiceResponseDso>>(response.Data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for UserService entities.");
                return null;
            }
        }

        public override async Task<UserServiceResponseDso?> GetOneByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving UserService entity...");
                return GetMapper().Map<UserServiceResponseDso>(await _share.GetOneByAsync(conditions, options));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetOneByAsync  for UserService entity.");
                return null;
            }
        }
    }
}