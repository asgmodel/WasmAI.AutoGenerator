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
    /// Payment interface property for BuilderRepository.
    /// </summary>
    public interface IPaymentBuilderRepository<TBuildRequestDto, TBuildResponseDto> : IBaseBuilderRepository<TBuildRequestDto, TBuildResponseDto> //
 where TBuildRequestDto : class //
 where TBuildResponseDto : class //
    {
    // Define methods or properties specific to the builder interface.
    }
}