using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using AutoGenerator.Config;
using System;

namespace ApiCore.DyModels.Dto.Build.Responses
{
    public class SettingResponseBuildDto : ITBuildDto
    {
        /// <summary>
        /// Name property for DTO.
        /// </summary>
        public String? Name { get; set; }
        /// <summary>
        /// Value property for DTO.
        /// </summary>
        public String? Value { get; set; }
    }
}