using AutoMapper;
using LAHJAAPI.Data;
using LAHJAAPI.Models;
using ApiCore.Repositories.Base;
using AutoGenerator.Repositories.Builder;
using ApiCore.DyModels.Dto.Build.Requests;
using ApiCore.DyModels.Dto.Build.Responses;
using AutoGenerator;
using ApiCore.Repositories.Builder;
using AutoGenerator.Repositories.Share;
using System.Linq.Expressions;
using AutoGenerator.Repositories.Base;
using AutoGenerator.Helper;
using ApiCore.DyModels.Dto.Share.Requests;
using ApiCore.DyModels.Dto.Share.Responses;
using System;

namespace ApiCore.Repositories.Share
{
    /// <summary>
    /// AuthorizationSession class for ShareRepository.
    /// </summary>
    public class AuthorizationSessionShareRepository : BaseShareRepository<AuthorizationSessionRequestShareDto, AuthorizationSessionResponseShareDto, AuthorizationSessionRequestBuildDto, AuthorizationSessionResponseBuildDto>, IAuthorizationSessionShareRepository
    {
        // Declare the builder repository.
        private readonly AuthorizationSessionBuilderRepository _builder;
        /// <summary>
        /// Constructor for AuthorizationSessionShareRepository.
        /// </summary>
        public AuthorizationSessionShareRepository(DataContext dbContext, IMapper mapper, ILoggerFactory logger) : base(mapper, logger)
        {
            // Initialize the builder repository.
            _builder = new AuthorizationSessionBuilderRepository(dbContext, mapper, logger.CreateLogger(typeof(AuthorizationSessionShareRepository).FullName));
        // Initialize the logger.
        }

        /// <summary>
        /// Method to count the number of entities.
        /// </summary>
        public override Task<int> CountAsync()
        {
            try
            {
                _logger.LogInformation("Counting AuthorizationSession entities...");
                return _builder.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CountAsync for AuthorizationSession entities.");
                return Task.FromResult(0);
            }
        }

        /// <summary>
        /// Method to create a new entity asynchronously.
        /// </summary>
        public override async Task<AuthorizationSessionResponseShareDto> CreateAsync(AuthorizationSessionRequestShareDto entity)
        {
            try
            {
                _logger.LogInformation("Creating new AuthorizationSession entity...");
                // Call the create method in the builder repository.
                var result = await _builder.CreateAsync(entity);
                // Convert the result to ResponseShareDto type.
                var output = MapToShareResponseDto(result);
                _logger.LogInformation("Created AuthorizationSession entity successfully.");
                // Return the final result.
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating AuthorizationSession entity.");
                return null;
            }
        }

        /// <summary>
        /// Method to retrieve all entities.
        /// </summary>
        public override async Task<IEnumerable<AuthorizationSessionResponseShareDto>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all AuthorizationSession entities...");
                return MapToIEnumerableShareResponseDto(await _builder.GetAllAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for AuthorizationSession entities.");
                return null;
            }
        }

        /// <summary>
        /// Method to get an entity by its unique ID.
        /// </summary>
        public override async Task<AuthorizationSessionResponseShareDto?> GetByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Retrieving AuthorizationSession entity with ID: {id}...");
                return MapToShareResponseDto(await _builder.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetByIdAsync for AuthorizationSession entity with ID: {id}.");
                return null;
            }
        }

        /// <summary>
        /// Method to retrieve data as an IQueryable object.
        /// </summary>
        public override IQueryable<AuthorizationSessionResponseShareDto> GetQueryable()
        {
            try
            {
                _logger.LogInformation("Retrieving IQueryable<AuthorizationSessionResponseShareDto> for AuthorizationSession entities...");
                return MapToIEnumerableShareResponseDto(_builder.GetQueryable().ToList()).AsQueryable();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetQueryable for AuthorizationSession entities.");
                return null;
            }
        }

        /// <summary>
        /// Method to save changes to the database.
        /// </summary>
        public Task SaveChangesAsync()
        {
            try
            {
                _logger.LogInformation("Saving changes to the database for AuthorizationSession entities...");
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SaveChangesAsync for AuthorizationSession entities.");
                return Task.CompletedTask;
            }
        }

        /// <summary>
        /// Method to update a specific entity.
        /// </summary>
        public override async Task<AuthorizationSessionResponseShareDto> UpdateAsync(AuthorizationSessionRequestShareDto entity)
        {
            try
            {
                _logger.LogInformation("Updating AuthorizationSession entity...");
                return MapToShareResponseDto(await _builder.UpdateAsync(entity));
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
                var exists = await _builder.ExistsAsync(value, name);
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

        public override async Task<PagedResponse<AuthorizationSessionResponseShareDto>> GetAllAsync(string[]? includes = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                _logger.LogInformation("Fetching all AuthorizationSessions with pagination: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
                var results = (await _builder.GetAllAsync(includes, pageNumber, pageSize));
                var items = MapToIEnumerableShareResponseDto(results.Data);
                return new PagedResponse<AuthorizationSessionResponseShareDto>(items, results.PageNumber, results.PageSize, results.TotalPages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all AuthorizationSessions.");
                return new PagedResponse<AuthorizationSessionResponseShareDto>(new List<AuthorizationSessionResponseShareDto>(), pageNumber, pageSize, 0);
            }
        }

        public override async Task<AuthorizationSessionResponseShareDto?> GetByIdAsync(object id)
        {
            try
            {
                _logger.LogInformation("Fetching AuthorizationSession by ID: {Id}", id);
                var result = await _builder.GetByIdAsync(id);
                if (result == null)
                {
                    _logger.LogWarning("AuthorizationSession not found with ID: {Id}", id);
                    return null;
                }

                _logger.LogInformation("Retrieved AuthorizationSession successfully.");
                return MapToShareResponseDto(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving AuthorizationSession by ID: {Id}", id);
                return null;
            }
        }

        public override Task DeleteAsync(string id)
        {
            return _builder.DeleteAsync(id);
        }

        public override async Task DeleteAsync(object value, string key = "Id")
        {
            try
            {
                _logger.LogInformation("Deleting AuthorizationSession with {Key}: {Value}", key, value);
                await _builder.DeleteAsync(value, key);
                _logger.LogInformation("AuthorizationSession with {Key}: {Value} deleted successfully.", key, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting AuthorizationSession with {Key}: {Value}", key, value);
            }
        }

        public override async Task DeleteRange(List<AuthorizationSessionRequestShareDto> entities)
        {
            try
            {
                var builddtos = entities.OfType<AuthorizationSessionRequestBuildDto>().ToList();
                _logger.LogInformation("Deleting {Count} AuthorizationSessions...", 201);
                await _builder.DeleteRange(builddtos);
                _logger.LogInformation("{Count} AuthorizationSessions deleted successfully.", 202);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting multiple AuthorizationSessions.");
            }
        }

        public override async Task<PagedResponse<AuthorizationSessionResponseShareDto>> GetAllByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("[Share]Retrieving  AuthorizationSession entities as pagination...");
                return MapToPagedResponse(await _builder.GetAllByAsync(conditions, options));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Share]Error in GetAllByAsync for AuthorizationSession entities as pagination.");
                return null;
            }
        }

        public override async Task<AuthorizationSessionResponseShareDto?> GetOneByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("[Share]Retrieving AuthorizationSession entity...");
                return MapToShareResponseDto(await _builder.GetOneByAsync(conditions, options));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Share]Error in GetOneByAsync  for AuthorizationSession entity.");
                return null;
            }
        }
    }
}