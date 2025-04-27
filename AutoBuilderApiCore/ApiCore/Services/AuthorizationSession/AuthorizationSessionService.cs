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
    public class AuthorizationSessionService : BaseService<AuthorizationSessionRequestDso, AuthorizationSessionResponseDso>, IUseAuthorizationSessionService
    {
        private readonly IAuthorizationSessionShareRepository _share;
        public AuthorizationSessionService(IAuthorizationSessionShareRepository buildAuthorizationSessionShareRepository, IMapper mapper, ILoggerFactory logger) : base(mapper, logger)
        {
            _share = buildAuthorizationSessionShareRepository;
        }

        public override Task<int> CountAsync()
        {
            try
            {
                _logger.LogInformation("Counting AuthorizationSession entities...");
                return _share.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CountAsync for AuthorizationSession entities.");
                return Task.FromResult(0);
            }
        }

        public override async Task<AuthorizationSessionResponseDso> CreateAsync(AuthorizationSessionRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Creating new AuthorizationSession entity...");
                var result = await _share.CreateAsync(entity);
                var output = GetMapper().Map<AuthorizationSessionResponseDso>(result);
                _logger.LogInformation("Created AuthorizationSession entity successfully.");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating AuthorizationSession entity.");
                return null;
            }
        }

        public override Task DeleteAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Deleting AuthorizationSession entity with ID: {id}...");
                return _share.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting AuthorizationSession entity with ID: {id}.");
                return Task.CompletedTask;
            }
        }

        public override async Task<IEnumerable<AuthorizationSessionResponseDso>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all AuthorizationSession entities...");
                var results = await _share.GetAllAsync();
                return GetMapper().Map<IEnumerable<AuthorizationSessionResponseDso>>(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for AuthorizationSession entities.");
                return null;
            }
        }

        public override async Task<AuthorizationSessionResponseDso?> GetByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Retrieving AuthorizationSession entity with ID: {id}...");
                var result = await _share.GetByIdAsync(id);
                var item = GetMapper().Map<AuthorizationSessionResponseDso>(result);
                _logger.LogInformation("Retrieved AuthorizationSession entity successfully.");
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetByIdAsync for AuthorizationSession entity with ID: {id}.");
                return null;
            }
        }

        public override IQueryable<AuthorizationSessionResponseDso> GetQueryable()
        {
            try
            {
                _logger.LogInformation("Retrieving IQueryable<AuthorizationSessionResponseDso> for AuthorizationSession entities...");
                var queryable = _share.GetQueryable();
                var result = GetMapper().ProjectTo<AuthorizationSessionResponseDso>(queryable);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetQueryable for AuthorizationSession entities.");
                return null;
            }
        }

        public override async Task<AuthorizationSessionResponseDso> UpdateAsync(AuthorizationSessionRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Updating AuthorizationSession entity...");
                var result = await _share.UpdateAsync(entity);
                return GetMapper().Map<AuthorizationSessionResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync for AuthorizationSession entity.");
                return null;
            }
        }

        public override async Task<bool> ExistsAsync(object value, string name = "Id")
        {
            try
            {
                _logger.LogInformation("Checking if AuthorizationSession exists with {Key}: {Value}", name, value);
                var exists = await _share.ExistsAsync(value, name);
                if (!exists)
                {
                    _logger.LogWarning("AuthorizationSession not found with {Key}: {Value}", name, value);
                }

                return exists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking existence of AuthorizationSession with {Key}: {Value}", name, value);
                return false;
            }
        }

        public override async Task<PagedResponse<AuthorizationSessionResponseDso>> GetAllAsync(string[]? includes = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                _logger.LogInformation("Fetching all AuthorizationSessions with pagination: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
                var results = (await _share.GetAllAsync(includes, pageNumber, pageSize));
                var items = GetMapper().Map<List<AuthorizationSessionResponseDso>>(results.Data);
                return new PagedResponse<AuthorizationSessionResponseDso>(items, results.PageNumber, results.PageSize, results.TotalPages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all AuthorizationSessions.");
                return new PagedResponse<AuthorizationSessionResponseDso>(new List<AuthorizationSessionResponseDso>(), pageNumber, pageSize, 0);
            }
        }

        public override async Task<AuthorizationSessionResponseDso?> GetByIdAsync(object id)
        {
            try
            {
                _logger.LogInformation("Fetching AuthorizationSession by ID: {Id}", id);
                var result = await _share.GetByIdAsync(id);
                if (result == null)
                {
                    _logger.LogWarning("AuthorizationSession not found with ID: {Id}", id);
                    return null;
                }

                _logger.LogInformation("Retrieved AuthorizationSession successfully.");
                return GetMapper().Map<AuthorizationSessionResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving AuthorizationSession by ID: {Id}", id);
                return null;
            }
        }

        public override async Task DeleteAsync(object value, string key = "Id")
        {
            try
            {
                _logger.LogInformation("Deleting AuthorizationSession with {Key}: {Value}", key, value);
                await _share.DeleteAsync(value, key);
                _logger.LogInformation("AuthorizationSession with {Key}: {Value} deleted successfully.", key, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting AuthorizationSession with {Key}: {Value}", key, value);
            }
        }

        public override async Task DeleteRange(List<AuthorizationSessionRequestDso> entities)
        {
            try
            {
                var builddtos = entities.OfType<AuthorizationSessionRequestShareDto>().ToList();
                _logger.LogInformation("Deleting {Count} AuthorizationSessions...", 201);
                await _share.DeleteRange(builddtos);
                _logger.LogInformation("{Count} AuthorizationSessions deleted successfully.", 202);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting multiple AuthorizationSessions.");
            }
        }

        public override async Task<PagedResponse<AuthorizationSessionResponseDso>> GetAllByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving all AuthorizationSession entities...");
                var results = await _share.GetAllAsync();
                var response = await _share.GetAllByAsync(conditions, options);
                return response.ToResponse(GetMapper().Map<IEnumerable<AuthorizationSessionResponseDso>>(response.Data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for AuthorizationSession entities.");
                return null;
            }
        }

        public override async Task<AuthorizationSessionResponseDso?> GetOneByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving AuthorizationSession entity...");
                return GetMapper().Map<AuthorizationSessionResponseDso>(await _share.GetOneByAsync(conditions, options));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetOneByAsync  for AuthorizationSession entity.");
                return null;
            }
        }
    }
}