using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using AutoGenerator.Config;
using System;

namespace ApiCore.DyModels.Dto.Build.ResponseFilters
{
    public class SpaceResponseFilterBuildDto : ITBuildDto
    {
        /// <summary>
        /// Id property for DTO.
        /// </summary>
        public String? Id { get; set; }
        /// <summary>
        /// Name property for DTO.
        /// </summary>
        public String? Name { get; set; }
        /// <summary>
        /// Description property for DTO.
        /// </summary>
        public String? Description { get; set; }
        /// <summary>
        /// Ram property for DTO.
        /// </summary>
        public Nullable<Int32> Ram { get; set; }
        /// <summary>
        /// CpuCores property for DTO.
        /// </summary>
        public Nullable<Int32> CpuCores { get; set; }
        /// <summary>
        /// DiskSpace property for DTO.
        /// </summary>
        public Nullable<Single> DiskSpace { get; set; }
        /// <summary>
        /// IsGpu property for DTO.
        /// </summary>
        public Nullable<Boolean> IsGpu { get; set; }
        /// <summary>
        /// IsGlobal property for DTO.
        /// </summary>
        public Nullable<Boolean> IsGlobal { get; set; }
        /// <summary>
        /// Bandwidth property for DTO.
        /// </summary>
        public Nullable<Single> Bandwidth { get; set; }
        /// <summary>
        /// Token property for DTO.
        /// </summary>
        public String? Token { get; set; }
        /// <summary>
        /// SubscriptionId property for DTO.
        /// </summary>
        public String? SubscriptionId { get; set; }
        public SubscriptionResponseFilterBuildDto? Subscription { get; set; }
        public ICollection<RequestResponseFilterBuildDto>? Requests { get; set; }

        [FilterLGEnabled]
        public string? Lg { get; set; }
    }
}