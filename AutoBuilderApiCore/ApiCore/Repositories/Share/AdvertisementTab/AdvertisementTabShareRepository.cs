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
    /// AdvertisementTab class for ShareRepository.
    /// </summary>
    public class AdvertisementTabShareRepository : BaseShareRepository<AdvertisementTabRequestShareDto, AdvertisementTabResponseShareDto, AdvertisementTabRequestBuildDto, AdvertisementTabResponseBuildDto>, IAdvertisementTabShareRepository
    {
        // Declare the builder repository.
        private readonly AdvertisementTabBuilderRepository _builder;
        /// <summary>
        /// Constructor for AdvertisementTabShareRepository.
        /// </summary>
        public AdvertisementTabShareRepository(DataContext dbContext, IMapper mapper, ILoggerFactory logger) : base(mapper, logger)
        {
            // Initialize the builder repository.
            _builder = new AdvertisementTabBuilderRepository(dbContext, mapper, logger.CreateLogger(typeof(AdvertisementTabShareRepository).FullName));
        // Initialize the logger.
        }

        /// <summary>
        /// Method to count the number of entities.
        /// </summary>
        public override Task<int> CountAsync()
        {
            try
            {
                _logger.LogInformation("Counting AdvertisementTab entities...");
                return _builder.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CountAsync for AdvertisementTab entities.");
                return Task.FromResult(0);
            }
        }

        /// <summary>
        /// Method to create a new entity asynchronously.
        /// </summary>
        public override async Task<AdvertisementTabResponseShareDto> CreateAsync(AdvertisementTabRequestShareDto entity)
        {
            try
            {
                _logger.LogInformation("Creating new AdvertisementTab entity...");
                // Call the create method in the builder repository.
                var result = await _builder.CreateAsync(entity);
                // Convert the result to ResponseShareDto type.
                var output = MapToShareResponseDto(result);
                _logger.LogInformation("Created AdvertisementTab entity successfully.");
                // Return the final result.
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating AdvertisementTab entity.");
                return null;
            }
        }

        /// <summary>
        /// Method to retrieve all entities.
        /// </summary>
        public override async Task<IEnumerable<AdvertisementTabResponseShareDto>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all AdvertisementTab entities...");
                return MapToIEnumerableShareResponseDto(await _builder.GetAllAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for AdvertisementTab entities.");
                return null;
            }
        }

        /// <summary>
        /// Method to get an entity by its unique ID.
        /// </summary>
        public override async Task<AdvertisementTabResponseShareDto?> GetByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Retrieving AdvertisementTab entity with ID: {id}...");
                return MapToShareResponseDto(await _builder.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetByIdAsync for AdvertisementTab entity with ID: {id}.");
                return null;
            }
        }

        /// <summary>
        /// Method to retrieve data as an IQueryable object.
        /// </summary>
        public override IQueryable<AdvertisementTabResponseShareDto> GetQueryable()
        {
            try
            {
                _logger.LogInformation("Retrieving IQueryable<AdvertisementTabResponseShareDto> for AdvertisementTab entities...");
                return MapToIEnumerableShareResponseDto(_builder.GetQueryable().ToList()).AsQueryable();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetQueryable for AdvertisementTab entities.");
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
                _logger.LogInformation("Saving changes to the database for AdvertisementTab entities...");
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SaveChangesAsync for AdvertisementTab entities.");
                return Task.CompletedTask;
            }
        }

        /// <summary>
        /// Method to update a specific entity.
        /// </summary>
        public override async Task<AdvertisementTabResponseShareDto> UpdateAsync(AdvertisementTabRequestShareDto entity)
        {
            try
            {
                _logger.LogInformation("Updating AdvertisementTab entity...");
                return MapToShareResponseDto(await _builder.UpdateAsync(entity));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync for AdvertisementTab entity.");
                return null;
            }
        }

        public override async Task<bool> ExistsAsync(object value, string name = "Id")
        {
            try
            {
                _logger.LogInformation("Checking if AdvertisementTab exists with {Key}: {Value}", name, value);
                var exists = await _builder.ExistsAsync(value, name);
                if (!exists)
                {
                    _logger.LogWarning("AdvertisementTab not found with {Key}: {Value}", name, value);
                }

                return exists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking existence of AdvertisementTab with {Key}: {Value}", name, value);
                return false;
            }
        }

        public override async Task<PagedResponse<AdvertisementTabResponseShareDto>> GetAllAsync(string[]? includes = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                _logger.LogInformation("Fetching all AdvertisementTabs with pagination: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
                var results = (await _builder.GetAllAsync(includes, pageNumber, pageSize));
                var items = MapToIEnumerableShareResponseDto(results.Data);
                return new PagedResponse<AdvertisementTabResponseShareDto>(items, results.PageNumber, results.PageSize, results.TotalPages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all AdvertisementTabs.");
                return new PagedResponse<AdvertisementTabResponseShareDto>(new List<AdvertisementTabResponseShareDto>(), pageNumber, pageSize, 0);
            }
        }

        public override async Task<AdvertisementTabResponseShareDto?> GetByIdAsync(object id)
        {
            try
            {
                _logger.LogInformation("Fetching AdvertisementTab by ID: {Id}", id);
                var result = await _builder.GetByIdAsync(id);
                if (result == null)
                {
                    _logger.LogWarning("AdvertisementTab not found with ID: {Id}", id);
                    return null;
                }

                _logger.LogInformation("Retrieved AdvertisementTab successfully.");
                return MapToShareResponseDto(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving AdvertisementTab by ID: {Id}", id);
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
                _logger.LogInformation("Deleting AdvertisementTab with {Key}: {Value}", key, value);
                await _builder.DeleteAsync(value, key);
                _logger.LogInformation("AdvertisementTab with {Key}: {Value} deleted successfully.", key, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting AdvertisementTab with {Key}: {Value}", key, value);
            }
        }

        public override async Task DeleteRange(List<AdvertisementTabRequestShareDto> entities)
        {
            try
            {
                var builddtos = entities.OfType<AdvertisementTabRequestBuildDto>().ToList();
                _logger.LogInformation("Deleting {Count} AdvertisementTabs...", 201);
                await _builder.DeleteRange(builddtos);
                _logger.LogInformation("{Count} AdvertisementTabs deleted successfully.", 202);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting multiple AdvertisementTabs.");
            }
        }

        public override async Task<PagedResponse<AdvertisementTabResponseShareDto>> GetAllByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("[Share]Retrieving  AdvertisementTab entities as pagination...");
                return MapToPagedResponse(await _builder.GetAllByAsync(conditions, options));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Share]Error in GetAllByAsync for AdvertisementTab entities as pagination.");
                return null;
            }
        }

        public override async Task<AdvertisementTabResponseShareDto?> GetOneByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("[Share]Retrieving AdvertisementTab entity...");
                return MapToShareResponseDto(await _builder.GetOneByAsync(conditions, options));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Share]Error in GetOneByAsync  for AdvertisementTab entity.");
                return null;
            }
        }
    }
}