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
    /// Space class for ShareRepository.
    /// </summary>
    public class SpaceShareRepository : BaseShareRepository<SpaceRequestShareDto, SpaceResponseShareDto, SpaceRequestBuildDto, SpaceResponseBuildDto>, ISpaceShareRepository
    {
        // Declare the builder repository.
        private readonly SpaceBuilderRepository _builder;
        /// <summary>
        /// Constructor for SpaceShareRepository.
        /// </summary>
        public SpaceShareRepository(DataContext dbContext, IMapper mapper, ILoggerFactory logger) : base(mapper, logger)
        {
            // Initialize the builder repository.
            _builder = new SpaceBuilderRepository(dbContext, mapper, logger.CreateLogger(typeof(SpaceShareRepository).FullName));
        // Initialize the logger.
        }

        /// <summary>
        /// Method to count the number of entities.
        /// </summary>
        public override Task<int> CountAsync()
        {
            try
            {
                _logger.LogInformation("Counting Space entities...");
                return _builder.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CountAsync for Space entities.");
                return Task.FromResult(0);
            }
        }

        /// <summary>
        /// Method to create a new entity asynchronously.
        /// </summary>
        public override async Task<SpaceResponseShareDto> CreateAsync(SpaceRequestShareDto entity)
        {
            try
            {
                _logger.LogInformation("Creating new Space entity...");
                // Call the create method in the builder repository.
                var result = await _builder.CreateAsync(entity);
                // Convert the result to ResponseShareDto type.
                var output = MapToShareResponseDto(result);
                _logger.LogInformation("Created Space entity successfully.");
                // Return the final result.
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating Space entity.");
                return null;
            }
        }

        /// <summary>
        /// Method to retrieve all entities.
        /// </summary>
        public override async Task<IEnumerable<SpaceResponseShareDto>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all Space entities...");
                return MapToIEnumerableShareResponseDto(await _builder.GetAllAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for Space entities.");
                return null;
            }
        }

        /// <summary>
        /// Method to get an entity by its unique ID.
        /// </summary>
        public override async Task<SpaceResponseShareDto?> GetByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Retrieving Space entity with ID: {id}...");
                return MapToShareResponseDto(await _builder.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetByIdAsync for Space entity with ID: {id}.");
                return null;
            }
        }

        /// <summary>
        /// Method to retrieve data as an IQueryable object.
        /// </summary>
        public override IQueryable<SpaceResponseShareDto> GetQueryable()
        {
            try
            {
                _logger.LogInformation("Retrieving IQueryable<SpaceResponseShareDto> for Space entities...");
                return MapToIEnumerableShareResponseDto(_builder.GetQueryable().ToList()).AsQueryable();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetQueryable for Space entities.");
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
                _logger.LogInformation("Saving changes to the database for Space entities...");
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SaveChangesAsync for Space entities.");
                return Task.CompletedTask;
            }
        }

        /// <summary>
        /// Method to update a specific entity.
        /// </summary>
        public override async Task<SpaceResponseShareDto> UpdateAsync(SpaceRequestShareDto entity)
        {
            try
            {
                _logger.LogInformation("Updating Space entity...");
                return MapToShareResponseDto(await _builder.UpdateAsync(entity));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync for Space entity.");
                return null;
            }
        }

        public override async Task<bool> ExistsAsync(object value, string name = "Id")
        {
            try
            {
                _logger.LogInformation("Checking if Space exists with {Key}: {Value}", name, value);
                var exists = await _builder.ExistsAsync(value, name);
                if (!exists)
                {
                    _logger.LogWarning("Space not found with {Key}: {Value}", name, value);
                }

                return exists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking existence of Space with {Key}: {Value}", name, value);
                return false;
            }
        }

        public override async Task<PagedResponse<SpaceResponseShareDto>> GetAllAsync(string[]? includes = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                _logger.LogInformation("Fetching all Spaces with pagination: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
                var results = (await _builder.GetAllAsync(includes, pageNumber, pageSize));
                var items = MapToIEnumerableShareResponseDto(results.Data);
                return new PagedResponse<SpaceResponseShareDto>(items, results.PageNumber, results.PageSize, results.TotalPages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all Spaces.");
                return new PagedResponse<SpaceResponseShareDto>(new List<SpaceResponseShareDto>(), pageNumber, pageSize, 0);
            }
        }

        public override async Task<SpaceResponseShareDto?> GetByIdAsync(object id)
        {
            try
            {
                _logger.LogInformation("Fetching Space by ID: {Id}", id);
                var result = await _builder.GetByIdAsync(id);
                if (result == null)
                {
                    _logger.LogWarning("Space not found with ID: {Id}", id);
                    return null;
                }

                _logger.LogInformation("Retrieved Space successfully.");
                return MapToShareResponseDto(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving Space by ID: {Id}", id);
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
                _logger.LogInformation("Deleting Space with {Key}: {Value}", key, value);
                await _builder.DeleteAsync(value, key);
                _logger.LogInformation("Space with {Key}: {Value} deleted successfully.", key, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting Space with {Key}: {Value}", key, value);
            }
        }

        public override async Task DeleteRange(List<SpaceRequestShareDto> entities)
        {
            try
            {
                var builddtos = entities.OfType<SpaceRequestBuildDto>().ToList();
                _logger.LogInformation("Deleting {Count} Spaces...", 201);
                await _builder.DeleteRange(builddtos);
                _logger.LogInformation("{Count} Spaces deleted successfully.", 202);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting multiple Spaces.");
            }
        }

        public override async Task<PagedResponse<SpaceResponseShareDto>> GetAllByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("[Share]Retrieving  Space entities as pagination...");
                return MapToPagedResponse(await _builder.GetAllByAsync(conditions, options));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Share]Error in GetAllByAsync for Space entities as pagination.");
                return null;
            }
        }

        public override async Task<SpaceResponseShareDto?> GetOneByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("[Share]Retrieving Space entity...");
                return MapToShareResponseDto(await _builder.GetOneByAsync(conditions, options));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Share]Error in GetOneByAsync  for Space entity.");
                return null;
            }
        }
    }
}