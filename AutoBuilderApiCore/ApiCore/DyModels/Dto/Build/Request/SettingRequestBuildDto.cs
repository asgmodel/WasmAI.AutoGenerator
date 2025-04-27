using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using AutoGenerator.Config;
using System;

namespace ApiCore.DyModels.Dto.Build.Requests
{
    public class SettingRequestBuildDto : ITBuildDto
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