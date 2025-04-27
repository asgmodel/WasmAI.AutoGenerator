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
    /// ModelGateway class for ShareRepository.
    /// </summary>
    public class ModelGatewayShareRepository : BaseShareRepository<ModelGatewayRequestShareDto, ModelGatewayResponseShareDto, ModelGatewayRequestBuildDto, ModelGatewayResponseBuildDto>, IModelGatewayShareRepository
    {
        // Declare the builder repository.
        private readonly ModelGatewayBuilderRepository _builder;
        /// <summary>
        /// Constructor for ModelGatewayShareRepository.
        /// </summary>
        public ModelGatewayShareRepository(DataContext dbContext, IMapper mapper, ILoggerFactory logger) : base(mapper, logger)
        {
            // Initialize the builder repository.
            _builder = new ModelGatewayBuilderRepository(dbContext, mapper, logger.CreateLogger(typeof(ModelGatewayShareRepository).FullName));
        // Initialize the logger.
        }

        /// <summary>
        /// Method to count the number of entities.
        /// </summary>
        public override Task<int> CountAsync()
        {
            try
            {
                _logger.LogInformation("Counting ModelGateway entities...");
                return _builder.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CountAsync for ModelGateway entities.");
                return Task.FromResult(0);
            }
        }

        /// <summary>
        /// Method to create a new entity asynchronously.
        /// </summary>
        public override async Task<ModelGatewayResponseShareDto> CreateAsync(ModelGatewayRequestShareDto entity)
        {
            try
            {
                _logger.LogInformation("Creating new ModelGateway entity...");
                // Call the create method in the builder repository.
                var result = await _builder.CreateAsync(entity);
                // Convert the result to ResponseShareDto type.
                var output = MapToShareResponseDto(result);
                _logger.LogInformation("Created ModelGateway entity successfully.");
                // Return the final result.
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating ModelGateway entity.");
                return null;
            }
        }

        /// <summary>
        /// Method to retrieve all entities.
        /// </summary>
        public override async Task<IEnumerable<ModelGatewayResponseShareDto>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all ModelGateway entities...");
                return MapToIEnumerableShareResponseDto(await _builder.GetAllAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for ModelGateway entities.");
                return null;
            }
        }

        /// <summary>
        /// Method to get an entity by its unique ID.
        /// </summary>
        public override async Task<ModelGatewayResponseShareDto?> GetByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Retrieving ModelGateway entity with ID: {id}...");
                return MapToShareResponseDto(await _builder.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetByIdAsync for ModelGateway entity with ID: {id}.");
                return null;
            }
        }

        /// <summary>
        /// Method to retrieve data as an IQueryable object.
        /// </summary>
        public override IQueryable<ModelGatewayResponseShareDto> GetQueryable()
        {
            try
            {
                _logger.LogInformation("Retrieving IQueryable<ModelGatewayResponseShareDto> for ModelGateway entities...");
                return MapToIEnumerableShareResponseDto(_builder.GetQueryable().ToList()).AsQueryable();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetQueryable for ModelGateway entities.");
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
                _logger.LogInformation("Saving changes to the database for ModelGateway entities...");
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SaveChangesAsync for ModelGateway entities.");
                return Task.CompletedTask;
            }
        }

        /// <summary>
        /// Method to update a specific entity.
        /// </summary>
        public override async Task<ModelGatewayResponseShareDto> UpdateAsync(ModelGatewayRequestShareDto entity)
        {
            try
            {
                _logger.LogInformation("Updating ModelGateway entity...");
                return MapToShareResponseDto(await _builder.UpdateAsync(entity));
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
                var exists = await _builder.ExistsAsync(value, name);
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

        public override async Task<PagedResponse<ModelGatewayResponseShareDto>> GetAllAsync(string[]? includes = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                _logger.LogInformation("Fetching all ModelGateways with pagination: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
                var results = (await _builder.GetAllAsync(includes, pageNumber, pageSize));
                var items = MapToIEnumerableShareResponseDto(results.Data);
                return new PagedResponse<ModelGatewayResponseShareDto>(items, results.PageNumber, results.PageSize, results.TotalPages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all ModelGateways.");
                return new PagedResponse<ModelGatewayResponseShareDto>(new List<ModelGatewayResponseShareDto>(), pageNumber, pageSize, 0);
            }
        }

        public override async Task<ModelGatewayResponseShareDto?> GetByIdAsync(object id)
        {
            try
            {
                _logger.LogInformation("Fetching ModelGateway by ID: {Id}", id);
                var result = await _builder.GetByIdAsync(id);
                if (result == null)
                {
                    _logger.LogWarning("ModelGateway not found with ID: {Id}", id);
                    return null;
                }

                _logger.LogInformation("Retrieved ModelGateway successfully.");
                return MapToShareResponseDto(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving ModelGateway by ID: {Id}", id);
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
                _logger.LogInformation("Deleting ModelGateway with {Key}: {Value}", key, value);
                await _builder.DeleteAsync(value, key);
                _logger.LogInformation("ModelGateway with {Key}: {Value} deleted successfully.", key, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting ModelGateway with {Key}: {Value}", key, value);
            }
        }

        public override async Task DeleteRange(List<ModelGatewayRequestShareDto> entities)
        {
            try
            {
                var builddtos = entities.OfType<ModelGatewayRequestBuildDto>().ToList();
                _logger.LogInformation("Deleting {Count} ModelGateways...", 201);
                await _builder.DeleteRange(builddtos);
                _logger.LogInformation("{Count} ModelGateways deleted successfully.", 202);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting multiple ModelGateways.");
            }
        }

        public override async Task<PagedResponse<ModelGatewayResponseShareDto>> GetAllByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("[Share]Retrieving  ModelGateway entities as pagination...");
                return MapToPagedResponse(await _builder.GetAllByAsync(conditions, options));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Share]Error in GetAllByAsync for ModelGateway entities as pagination.");
                return null;
            }
        }

        public override async Task<ModelGatewayResponseShareDto?> GetOneByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("[Share]Retrieving ModelGateway entity...");
                return MapToShareResponseDto(await _builder.GetOneByAsync(conditions, options));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Share]Error in GetOneByAsync  for ModelGateway entity.");
                return null;
            }
        }
    }
}