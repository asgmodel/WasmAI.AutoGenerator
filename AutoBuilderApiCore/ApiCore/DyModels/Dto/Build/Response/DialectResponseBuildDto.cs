using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using AutoGenerator.Config;
using System;

namespace ApiCore.DyModels.Dto.Build.Responses
{
    public class DialectResponseBuildDto : ITBuildDto
    {
        /// <summary>
        /// Id property for DTO.
        /// </summary>
        public String? Id { get; set; }
        public TranslationData? Name { get; set; } = new();
        public TranslationData? Description { get; set; } = new();
        /// <summary>
        /// LanguageId property for DTO.
        /// </summary>
        public String? LanguageId { get; set; }
        public LanguageResponseBuildDto? Language { get; set; }
    }
}