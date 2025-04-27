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
    /// ModelAi class property for BuilderRepository.
    /// </summary>
     //
    public class ModelAiBuilderRepository : BaseBuilderRepository<ModelAi, ModelAiRequestBuildDto, ModelAiResponseBuildDto>, IModelAiBuilderRepository<ModelAiRequestBuildDto, ModelAiResponseBuildDto>
    {
        /// <summary>
        /// Constructor for ModelAiBuilderRepository.
        /// </summary>
        public ModelAiBuilderRepository(DataContext dbContext, IMapper mapper, ILogger logger) : base(dbContext, mapper, logger) // Initialize  constructor.
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