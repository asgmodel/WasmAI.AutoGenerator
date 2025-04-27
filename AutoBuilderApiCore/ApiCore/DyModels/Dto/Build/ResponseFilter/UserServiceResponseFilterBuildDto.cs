using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using AutoGenerator.Config;
using System;

namespace ApiCore.DyModels.Dto.Build.ResponseFilters
{
    public class UserServiceResponseFilterBuildDto : ITBuildDto
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
        public ApplicationUserResponseFilterBuildDto? User { get; set; }
        /// <summary>
        /// ServiceId property for DTO.
        /// </summary>
        public String? ServiceId { get; set; }
        public ServiceResponseFilterBuildDto? Service { get; set; }

        [FilterLGEnabled]
        public string? Lg { get; set; }
    }
}