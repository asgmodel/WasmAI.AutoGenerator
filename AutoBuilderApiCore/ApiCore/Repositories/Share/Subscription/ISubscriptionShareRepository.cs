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
    /// Subscription interface for RepositoriesRepository.
    /// </summary>
    public interface ISubscriptionShareRepository : IBaseShareRepository<SubscriptionRequestShareDto, SubscriptionResponseShareDto> //
    , IBasePublicRepository<SubscriptionRequestShareDto, SubscriptionResponseShareDto>
    //  يمكنك  التزويد بكل  دوال   طبقة Builder   ببوابات  الطبقة   هذه نفسها      
    //,ISubscriptionBuilderRepository<SubscriptionRequestShareDto, SubscriptionResponseShareDto>
    {
    // Define methods or properties specific to the share repository interface.
    }
}