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
    /// Service class for ShareRepository.
    /// </summary>
    public class ServiceShareRepository : BaseShareRepository<ServiceRequestShareDto, ServiceResponseShareDto, ServiceRequestBuildDto, ServiceResponseBuildDto>, IServiceShareRepository
    {
        // Declare the builder repository.
        private readonly ServiceBuilderRepository _builder;
        /// <summary>
        /// Constructor for ServiceShareRepository.
        /// </summary>
        public ServiceShareRepository(DataContext dbContext, IMapper mapper, ILoggerFactory logger) : base(mapper, logger)
        {
            // Initialize the builder repository.
            _builder = new ServiceBuilderRepository(dbContext, mapper, logger.CreateLogger(typeof(ServiceShareRepository).FullName));
        // Initialize the logger.
        }

        /// <summary>
        /// Method to count the number of entities.
        /// </summary>
        public override Task<int> CountAsync()
        {
            try
            {
                _logger.LogInformation("Counting Service entities...");
                return _builder.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CountAsync for Service entities.");
                return Task.FromResult(0);
            }
        }

        /// <summary>
        /// Method to create a new entity asynchronously.
        /// </summary>
        public override async Task<ServiceResponseShareDto> CreateAsync(ServiceRequestShareDto entity)
        {
            try
            {
                _logger.LogInformation("Creating new Service entity...");
                // Call the create method in the builder repository.
                var result = await _builder.CreateAsync(entity);
                // Convert the result to ResponseShareDto type.
                var output = MapToShareResponseDto(result);
                _logger.LogInformation("Created Service entity successfully.");
                // Return the final result.
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating Service entity.");
                return null;
            }
        }

        /// <summary>
        /// Method to retrieve all entities.
        /// </summary>
        public override async Task<IEnumerable<ServiceResponseShareDto>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all Service entities...");
                return MapToIEnumerableShareResponseDto(await _builder.GetAllAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for Service entities.");
                return null;
            }
        }

        /// <summary>
        /// Method to get an entity by its unique ID.
        /// </summary>
        public override async Task<ServiceResponseShareDto?> GetByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Retrieving Service entity with ID: {id}...");
                return MapToShareResponseDto(await _builder.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetByIdAsync for Service entity with ID: {id}.");
                return null;
            }
        }

        /// <summary>
        /// Method to retrieve data as an IQueryable object.
        /// </summary>
        public override IQueryable<ServiceResponseShareDto> GetQueryable()
        {
            try
            {
                _logger.LogInformation("Retrieving IQueryable<ServiceResponseShareDto> for Service entities...");
                return MapToIEnumerableShareResponseDto(_builder.GetQueryable().ToList()).AsQueryable();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetQueryable for Service entities.");
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
                _logger.LogInformation("Saving changes to the database for Service entities...");
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SaveChangesAsync for Service entities.");
                return Task.CompletedTask;
            }
        }

        /// <summary>
        /// Method to update a specific entity.
        /// </summary>
        public override async Task<ServiceResponseShareDto> UpdateAsync(ServiceRequestShareDto entity)
        {
            try
            {
                _logger.LogInformation("Updating Service entity...");
                return MapToShareResponseDto(await _builder.UpdateAsync(entity));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync for Service entity.");
                return null;
            }
        }

        public override async Task<bool> ExistsAsync(object value, string name = "Id")
        {
            try
            {
                _logger.LogInformation("Checking if Service exists with {Key}: {Value}", name, value);
                var exists = await _builder.ExistsAsync(value, name);
                if (!exists)
                {
                    _logger.LogWarning("Service not found with {Key}: {Value}", name, value);
                }

                return exists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking existence of Service with {Key}: {Value}", name, value);
                return false;
            }
        }

        public override async Task<PagedResponse<ServiceResponseShareDto>> GetAllAsync(string[]? includes = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                _logger.LogInformation("Fetching all Services with pagination: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
                var results = (await _builder.GetAllAsync(includes, pageNumber, pageSize));
                var items = MapToIEnumerableShareResponseDto(results.Data);
                return new PagedResponse<ServiceResponseShareDto>(items, results.PageNumber, results.PageSize, results.TotalPages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all Services.");
                return new PagedResponse<ServiceResponseShareDto>(new List<ServiceResponseShareDto>(), pageNumber, pageSize, 0);
            }
        }

        public override async Task<ServiceResponseShareDto?> GetByIdAsync(object id)
        {
            try
            {
                _logger.LogInformation("Fetching Service by ID: {Id}", id);
                var result = await _builder.GetByIdAsync(id);
                if (result == null)
                {
                    _logger.LogWarning("Service not found with ID: {Id}", id);
                    return null;
                }

                _logger.LogInformation("Retrieved Service successfully.");
                return MapToShareResponseDto(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving Service by ID: {Id}", id);
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
                _logger.LogInformation("Deleting Service with {Key}: {Value}", key, value);
                await _builder.DeleteAsync(value, key);
                _logger.LogInformation("Service with {Key}: {Value} deleted successfully.", key, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting Service with {Key}: {Value}", key, value);
            }
        }

        public override async Task DeleteRange(List<ServiceRequestShareDto> entities)
        {
            try
            {
                var builddtos = entities.OfType<ServiceRequestBuildDto>().ToList();
                _logger.LogInformation("Deleting {Count} Services...", 201);
                await _builder.DeleteRange(builddtos);
                _logger.LogInformation("{Count} Services deleted successfully.", 202);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting multiple Services.");
            }
        }

        public override async Task<PagedResponse<ServiceResponseShareDto>> GetAllByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("[Share]Retrieving  Service entities as pagination...");
                return MapToPagedResponse(await _builder.GetAllByAsync(conditions, options));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Share]Error in GetAllByAsync for Service entities as pagination.");
                return null;
            }
        }

        public override async Task<ServiceResponseShareDto?> GetOneByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("[Share]Retrieving Service entity...");
                return MapToShareResponseDto(await _builder.GetOneByAsync(conditions, options));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Share]Error in GetOneByAsync  for Service entity.");
                return null;
            }
        }
    }
}