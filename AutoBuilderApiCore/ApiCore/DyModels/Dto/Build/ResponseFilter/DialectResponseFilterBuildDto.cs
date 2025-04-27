using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using AutoGenerator.Config;
using System;

namespace ApiCore.DyModels.Dto.Build.ResponseFilters
{
    public class DialectResponseFilterBuildDto : ITBuildDto
    {
        /// <summary>
        /// Id property for DTO.
        /// </summary>
        public String? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        /// <summary>
        /// LanguageId property for DTO.
        /// </summary>
        public String? LanguageId { get; set; }
        public LanguageResponseFilterBuildDto? Language { get; set; }

        [FilterLGEnabled]
        public string? Lg { get; set; }
    }
}