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
    /// Dialect class for ShareRepository.
    /// </summary>
    public class DialectShareRepository : BaseShareRepository<DialectRequestShareDto, DialectResponseShareDto, DialectRequestBuildDto, DialectResponseBuildDto>, IDialectShareRepository
    {
        // Declare the builder repository.
        private readonly DialectBuilderRepository _builder;
        /// <summary>
        /// Constructor for DialectShareRepository.
        /// </summary>
        public DialectShareRepository(DataContext dbContext, IMapper mapper, ILoggerFactory logger) : base(mapper, logger)
        {
            // Initialize the builder repository.
            _builder = new DialectBuilderRepository(dbContext, mapper, logger.CreateLogger(typeof(DialectShareRepository).FullName));
        // Initialize the logger.
        }

        /// <summary>
        /// Method to count the number of entities.
        /// </summary>
        public override Task<int> CountAsync()
        {
            try
            {
                _logger.LogInformation("Counting Dialect entities...");
                return _builder.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CountAsync for Dialect entities.");
                return Task.FromResult(0);
            }
        }

        /// <summary>
        /// Method to create a new entity asynchronously.
        /// </summary>
        public override async Task<DialectResponseShareDto> CreateAsync(DialectRequestShareDto entity)
        {
            try
            {
                _logger.LogInformation("Creating new Dialect entity...");
                // Call the create method in the builder repository.
                var result = await _builder.CreateAsync(entity);
                // Convert the result to ResponseShareDto type.
                var output = MapToShareResponseDto(result);
                _logger.LogInformation("Created Dialect entity successfully.");
                // Return the final result.
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating Dialect entity.");
                return null;
            }
        }

        /// <summary>
        /// Method to retrieve all entities.
        /// </summary>
        public override async Task<IEnumerable<DialectResponseShareDto>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all Dialect entities...");
                return MapToIEnumerableShareResponseDto(await _builder.GetAllAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for Dialect entities.");
                return null;
            }
        }

        /// <summary>
        /// Method to get an entity by its unique ID.
        /// </summary>
        public override async Task<DialectResponseShareDto?> GetByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Retrieving Dialect entity with ID: {id}...");
                return MapToShareResponseDto(await _builder.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetByIdAsync for Dialect entity with ID: {id}.");
                return null;
            }
        }

        /// <summary>
        /// Method to retrieve data as an IQueryable object.
        /// </summary>
        public override IQueryable<DialectResponseShareDto> GetQueryable()
        {
            try
            {
                _logger.LogInformation("Retrieving IQueryable<DialectResponseShareDto> for Dialect entities...");
                return MapToIEnumerableShareResponseDto(_builder.GetQueryable().ToList()).AsQueryable();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetQueryable for Dialect entities.");
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
                _logger.LogInformation("Saving changes to the database for Dialect entities...");
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SaveChangesAsync for Dialect entities.");
                return Task.CompletedTask;
            }
        }

        /// <summary>
        /// Method to update a specific entity.
        /// </summary>
        public override async Task<DialectResponseShareDto> UpdateAsync(DialectRequestShareDto entity)
        {
            try
            {
                _logger.LogInformation("Updating Dialect entity...");
                return MapToShareResponseDto(await _builder.UpdateAsync(entity));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync for Dialect entity.");
                return null;
            }
        }

        public override async Task<bool> ExistsAsync(object value, string name = "Id")
        {
            try
            {
                _logger.LogInformation("Checking if Dialect exists with {Key}: {Value}", name, value);
                var exists = await _builder.ExistsAsync(value, name);
                if (!exists)
                {
                    _logger.LogWarning("Dialect not found with {Key}: {Value}", name, value);
                }

                return exists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking existence of Dialect with {Key}: {Value}", name, value);
                return false;
            }
        }

        public override async Task<PagedResponse<DialectResponseShareDto>> GetAllAsync(string[]? includes = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                _logger.LogInformation("Fetching all Dialects with pagination: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
                var results = (await _builder.GetAllAsync(includes, pageNumber, pageSize));
                var items = MapToIEnumerableShareResponseDto(results.Data);
                return new PagedResponse<DialectResponseShareDto>(items, results.PageNumber, results.PageSize, results.TotalPages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all Dialects.");
                return new PagedResponse<DialectResponseShareDto>(new List<DialectResponseShareDto>(), pageNumber, pageSize, 0);
            }
        }

        public override async Task<DialectResponseShareDto?> GetByIdAsync(object id)
        {
            try
            {
                _logger.LogInformation("Fetching Dialect by ID: {Id}", id);
                var result = await _builder.GetByIdAsync(id);
                if (result == null)
                {
                    _logger.LogWarning("Dialect not found with ID: {Id}", id);
                    return null;
                }

                _logger.LogInformation("Retrieved Dialect successfully.");
                return MapToShareResponseDto(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving Dialect by ID: {Id}", id);
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
                _logger.LogInformation("Deleting Dialect with {Key}: {Value}", key, value);
                await _builder.DeleteAsync(value, key);
                _logger.LogInformation("Dialect with {Key}: {Value} deleted successfully.", key, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting Dialect with {Key}: {Value}", key, value);
            }
        }

        public override async Task DeleteRange(List<DialectRequestShareDto> entities)
        {
            try
            {
                var builddtos = entities.OfType<DialectRequestBuildDto>().ToList();
                _logger.LogInformation("Deleting {Count} Dialects...", 201);
                await _builder.DeleteRange(builddtos);
                _logger.LogInformation("{Count} Dialects deleted successfully.", 202);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting multiple Dialects.");
            }
        }

        public override async Task<PagedResponse<DialectResponseShareDto>> GetAllByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("[Share]Retrieving  Dialect entities as pagination...");
                return MapToPagedResponse(await _builder.GetAllByAsync(conditions, options));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Share]Error in GetAllByAsync for Dialect entities as pagination.");
                return null;
            }
        }

        public override async Task<DialectResponseShareDto?> GetOneByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("[Share]Retrieving Dialect entity...");
                return MapToShareResponseDto(await _builder.GetOneByAsync(conditions, options));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Share]Error in GetOneByAsync  for Dialect entity.");
                return null;
            }
        }
    }
}