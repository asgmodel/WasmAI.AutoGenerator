using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using AutoGenerator.Config;
using System;

namespace ApiCore.DyModels.Dto.Build.Responses
{
    public class UserModelAiResponseBuildDto : ITBuildDto
    {
        /// <summary>
        /// Id property for DTO.
        /// </summary>
        public Int32 Id { get; set; }
        /// <summary>
        /// CreatedAt property for DTO.
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// UserId property for DTO.
        /// </summary>
        public String? UserId { get; set; }
        public ApplicationUserResponseBuildDto? User { get; set; }
        /// <summary>
        /// ModelAiId property for DTO.
        /// </summary>
        public String? ModelAiId { get; set; }
        public ModelAiResponseBuildDto? ModelAi { get; set; }
    }
}