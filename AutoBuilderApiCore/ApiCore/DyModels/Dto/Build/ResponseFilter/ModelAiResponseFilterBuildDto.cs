using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using AutoGenerator.Config;
using System;

namespace ApiCore.DyModels.Dto.Build.ResponseFilters
{
    public class ModelAiResponseFilterBuildDto : ITBuildDto
    {
        /// <summary>
        /// Id property for DTO.
        /// </summary>
        public String? Id { get; set; }
        public string? Name { get; set; }
        /// <summary>
        /// Token property for DTO.
        /// </summary>
        public String? Token { get; set; }
        /// <summary>
        /// AbsolutePath property for DTO.
        /// </summary>
        public String? AbsolutePath { get; set; }
        public string? Category { get; set; }
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
        public string? Dialect { get; set; }
        /// <summary>
        /// Type property for DTO.
        /// </summary>
        public String? Type { get; set; }
        /// <summary>
        /// ModelGatewayId property for DTO.
        /// </summary>
        public String? ModelGatewayId { get; set; }
        public ModelGatewayResponseFilterBuildDto? ModelGateway { get; set; }
        public ICollection<ServiceResponseFilterBuildDto>? Services { get; set; }
        public ICollection<UserModelAiResponseFilterBuildDto>? UserModelAis { get; set; }

        [FilterLGEnabled]
        public string? Lg { get; set; }
    }
}