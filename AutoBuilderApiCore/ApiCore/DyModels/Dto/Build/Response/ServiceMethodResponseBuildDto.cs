using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using AutoGenerator.Config;
using System;

namespace ApiCore.DyModels.Dto.Build.Responses
{
    public class ServiceMethodResponseBuildDto : ITBuildDto
    {
        /// <summary>
        /// Id property for DTO.
        /// </summary>
        public String? Id { get; set; }
        /// <summary>
        /// Method property for DTO.
        /// </summary>
        public String? Method { get; set; }
        /// <summary>
        /// InputParameters property for DTO.
        /// </summary>
        public String? InputParameters { get; set; }
        /// <summary>
        /// OutputParameters property for DTO.
        /// </summary>
        public String? OutputParameters { get; set; }
        /// <summary>
        /// ServiceId property for DTO.
        /// </summary>
        public String? ServiceId { get; set; }
        public ServiceResponseBuildDto? Service { get; set; }
    }
}