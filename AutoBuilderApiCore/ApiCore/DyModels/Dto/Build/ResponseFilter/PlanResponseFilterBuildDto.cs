using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using AutoGenerator.Config;
using System;

namespace ApiCore.DyModels.Dto.Build.ResponseFilters
{
    public class PlanResponseFilterBuildDto : ITBuildDto
    {
        /// <summary>
        /// Id property for DTO.
        /// </summary>
        public String? Id { get; set; }
        /// <summary>
        /// ProductId property for DTO.
        /// </summary>
        public String? ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public ICollection<String>? Images { get; set; }
        /// <summary>
        /// BillingPeriod property for DTO.
        /// </summary>
        public String? BillingPeriod { get; set; }
        /// <summary>
        /// Amount property for DTO.
        /// </summary>
        public Double Amount { get; set; }
        /// <summary>
        /// Active property for DTO.
        /// </summary>
        public Boolean Active { get; set; }
        /// <summary>
        /// UpdatedAt property for DTO.
        /// </summary>
        public DateTime UpdatedAt { get; set; }
        /// <summary>
        /// CreatedAt property for DTO.
        /// </summary>
        public DateTime CreatedAt { get; set; }
        public ICollection<SubscriptionResponseFilterBuildDto>? Subscriptions { get; set; }
        public ICollection<PlanServices>? PlanServices { get; set; }
        public ICollection<PlanFeatureResponseFilterBuildDto>? PlanFeatures { get; set; }

        [FilterLGEnabled]
        public string? Lg { get; set; }
    }
}