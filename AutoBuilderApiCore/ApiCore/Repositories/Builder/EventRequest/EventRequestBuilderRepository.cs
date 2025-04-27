using AutoMapper;
using LAHJAAPI.Data;
using LAHJAAPI.Models;
using ApiCore.Repositories.Base;
using AutoGenerator.Repositories.Builder;
using ApiCore.DyModels.Dto.Build.Requests;
using ApiCore.DyModels.Dto.Build.Responses;
using System;

namespace ApiCore.Repositories.Builder
{
    /// <summary>
    /// EventRequest class property for BuilderRepository.
    /// </summary>
     //
    public class EventRequestBuilderRepository : BaseBuilderRepository<EventRequest, EventRequestRequestBuildDto, EventRequestResponseBuildDto>, IEventRequestBuilderRepository<EventRequestRequestBuildDto, EventRequestResponseBuildDto>
    {
        /// <summary>
        /// Constructor for EventRequestBuilderRepository.
        /// </summary>
        public EventRequestBuilderRepository(DataContext dbContext, IMapper mapper, ILogger logger) : base(dbContext, mapper, logger) // Initialize  constructor.
        {
        // Initialize necessary fields or call base constructor.
        ///
        /// 
         
        /// 
        }
    //
    // Add additional methods or properties as needed.
    }
}