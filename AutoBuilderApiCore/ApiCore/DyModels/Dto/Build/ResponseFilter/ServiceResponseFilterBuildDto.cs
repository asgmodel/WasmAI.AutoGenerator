using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using AutoGenerator.Config;
using System;

namespace ApiCore.DyModels.Dto.Build.ResponseFilters
{
    public class ServiceResponseFilterBuildDto : ITBuildDto
    {
        /// <summary>
        /// Id property for DTO.
        /// </summary>
        public String? Id { get; set; }
        /// <summary>
        /// Name property for DTO.
        /// </summary>
        public String? Name { get; set; }
        /// <summary>
        /// AbsolutePath property for DTO.
        /// </summary>
        public String? AbsolutePath { get; set; }
        /// <summary>
        /// Token property for DTO.
        /// </summary>
        public String? Token { get; set; }
        /// <summary>
        /// ModelAiId property for DTO.
        /// </summary>
        public String? ModelAiId { get; set; }
        public ModelAiResponseFilterBuildDto? ModelAi { get; set; }
        public ICollection<ServiceMethodResponseFilterBuildDto>? ServiceMethods { get; set; }
        public ICollection<UserServiceResponseFilterBuildDto>? UserServices { get; set; }
        public ICollection<RequestResponseFilterBuildDto>? Requests { get; set; }
        public ICollection<PlanServices>? PlanServices { get; set; }

        [FilterLGEnabled]
        public string? Lg { get; set; }
    }
}