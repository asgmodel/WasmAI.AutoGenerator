using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using AutoGenerator.Config;
using System;

namespace ApiCore.DyModels.Dto.Build.ResponseFilters
{
    public class AdvertisementTabResponseFilterBuildDto : ITBuildDto
    {
        /// <summary>
        /// Id property for DTO.
        /// </summary>
        public String? Id { get; set; }
        /// <summary>
        /// AdvertisementId property for DTO.
        /// </summary>
        public String? AdvertisementId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        /// <summary>
        /// ImageAlt property for DTO.
        /// </summary>
        public String? ImageAlt { get; set; }
        public AdvertisementResponseFilterBuildDto? Advertisement { get; set; }

        [FilterLGEnabled]
        public string? Lg { get; set; }
    }
}