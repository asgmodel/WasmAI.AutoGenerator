using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using AutoGenerator.Config;
using System;

namespace ApiCore.DyModels.Dto.Build.Requests
{
    public class ModelAiRequestBuildDto : ITBuildDto
    {
        /// <summary>
        /// Id property for DTO.
        /// </summary>
        public String? Id { get; set; }
        public TranslationData? Name { get; set; } = new();
        /// <summary>
        /// Token property for DTO.
        /// </summary>
        public String? Token { get; set; }
        /// <summary>
        /// AbsolutePath property for DTO.
        /// </summary>
        public String? AbsolutePath { get; set; }
        public TranslationData? Category { get; set; } = new();
        /// <summary>
        /// Language property for DTO.
        /// </summary>
        public String? Language { get; set; }
        /// <summary>
        /// IsStandard property for DTO.
        /// </summary>
        public Nullable<Boolean> IsStandard { get; set; }
        /// <summary>
        /// Gender property for DTO.
        /// </summary>
        public String? Gender { get; set; }
        public TranslationData? Dialect { get; set; } = new();
        /// <summary>
        /// Type property for DTO.
        /// </summary>
        public String? Type { get; set; }
        /// <summary>
        /// ModelGatewayId property for DTO.
        /// </summary>
        public String? ModelGatewayId { get; set; }
        public ModelGatewayRequestBuildDto? ModelGateway { get; set; }
        public ICollection<ServiceRequestBuildDto>? Services { get; set; }
        public ICollection<UserModelAiRequestBuildDto>? UserModelAis { get; set; }
    }
}