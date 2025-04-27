using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using AutoGenerator.Config;
using System;

namespace ApiCore.DyModels.Dto.Build.ResponseFilters
{
    public class FAQItemResponseFilterBuildDto : ITBuildDto
    {
        /// <summary>
        /// Id property for DTO.
        /// </summary>
        public String? Id { get; set; }
        public string? Question { get; set; }
        public string? Answer { get; set; }
        public string? Tag { get; set; }

        [FilterLGEnabled]
        public string? Lg { get; set; }
    }
}