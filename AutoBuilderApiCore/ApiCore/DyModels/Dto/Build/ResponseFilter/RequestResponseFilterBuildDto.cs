using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using AutoGenerator.Config;
using System;

namespace ApiCore.DyModels.Dto.Build.ResponseFilters
{
    public class RequestResponseFilterBuildDto : ITBuildDto
    {
        /// <summary>
        /// Id property for DTO.
        /// </summary>
        public String? Id { get; set; }
        /// <summary>
        /// Status property for DTO.
        /// </summary>
        public String? Status { get; set; }
        /// <summary>
        /// Question property for DTO.
        /// </summary>
        public String? Question { get; set; }
        /// <summary>
        /// Answer property for DTO.
        /// </summary>
        public String? Answer { get; set; }
        /// <summary>
        /// ModelGateway property for DTO.
        /// </summary>
        public String? ModelGateway { get; set; }
        /// <summary>
        /// ModelAi property for DTO.
        /// </summary>
        public String? ModelAi { get; set; }
        /// <summary>
        /// CreatedAt property for DTO.
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// UpdatedAt property for DTO.
        /// </summary>
        public DateTime UpdatedAt { get; set; }
        /// <summary>
        /// UserId property for DTO.
        /// </summary>
        public String? UserId { get; set; }
        public ApplicationUserResponseFilterBuildDto? User { get; set; }
        /// <summary>
        /// SubscriptionId property for DTO.
        /// </summary>
        public String? SubscriptionId { get; set; }
        public SubscriptionResponseFilterBuildDto? Subscription { get; set; }
        /// <summary>
        /// ServiceId property for DTO.
        /// </summary>
        public String? ServiceId { get; set; }
        public ServiceResponseFilterBuildDto? Service { get; set; }
        /// <summary>
        /// SpaceId property for DTO.
        /// </summary>
        public String? SpaceId { get; set; }
        public SpaceResponseFilterBuildDto? Space { get; set; }
        public ICollection<EventRequestResponseFilterBuildDto>? Events { get; set; }

        [FilterLGEnabled]
        public string? Lg { get; set; }
    }
}