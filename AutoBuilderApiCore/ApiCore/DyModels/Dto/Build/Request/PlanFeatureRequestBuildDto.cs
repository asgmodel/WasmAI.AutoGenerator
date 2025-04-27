using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using AutoGenerator.Config;
using System;

namespace ApiCore.DyModels.Dto.Build.Requests
{
    public class PlanFeatureRequestBuildDto : ITBuildDto
    {
        /// <summary>
        /// Id property for DTO.
        /// </summary>
        public Int32 Id { get; set; }
        /// <summary>
        /// Key property for DTO.
        /// </summary>
        public String? Key { get; set; }
        /// <summary>
        /// Value property for DTO.
        /// </summary>
        public String? Value { get; set; }
        public TranslationData? Name { get; set; } = new();
        public TranslationData? Description { get; set; } = new();
        /// <summary>
        /// PlanId property for DTO.
        /// </summary>
        public String? PlanId { get; set; }
        public PlanRequestBuildDto? Plan { get; set; }
    }
}