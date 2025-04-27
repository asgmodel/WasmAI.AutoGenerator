using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using AutoGenerator.Config;
using System;

namespace ApiCore.DyModels.Dto.Build.Requests
{
    public class ServiceRequestBuildDto : ITBuildDto
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
        public ModelAiRequestBuildDto? ModelAi { get; set; }
        public ICollection<ServiceMethodRequestBuildDto>? ServiceMethods { get; set; }
        public ICollection<UserServiceRequestBuildDto>? UserServices { get; set; }
        public ICollection<RequestRequestBuildDto>? Requests { get; set; }
        public ICollection<PlanServices>? PlanServices { get; set; }
    }
}