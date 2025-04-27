using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using AutoGenerator.Config;
using System;

namespace ApiCore.DyModels.Dto.Build.ResponseFilters
{
    public class SettingResponseFilterBuildDto : ITBuildDto
    {
        /// <summary>
        /// Name property for DTO.
        /// </summary>
        public String? Name { get; set; }
        /// <summary>
        /// Value property for DTO.
        /// </summary>
        public String? Value { get; set; }

        [FilterLGEnabled]
        public string? Lg { get; set; }
    }
}