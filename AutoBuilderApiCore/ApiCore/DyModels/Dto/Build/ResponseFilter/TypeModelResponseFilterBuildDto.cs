using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using AutoGenerator.Config;
using System;

namespace ApiCore.DyModels.Dto.Build.ResponseFilters
{
    public class TypeModelResponseFilterBuildDto : ITBuildDto
    {
        /// <summary>
        /// Id property for DTO.
        /// </summary>
        public String? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        /// <summary>
        /// Active property for DTO.
        /// </summary>
        public Boolean Active { get; set; }
        /// <summary>
        /// Image property for DTO.
        /// </summary>
        public String? Image { get; set; }

        [FilterLGEnabled]
        public string? Lg { get; set; }
    }
}