using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using AutoGenerator.Config;
using System;

namespace ApiCore.DyModels.Dto.Build.Responses
{
    public class SubscriptionResponseBuildDto : ITBuildDto
    {
        /// <summary>
        /// Id property for DTO.
        /// </summary>
        public String? Id { get; set; }
        /// <summary>
        /// CustomerId property for DTO.
        /// </summary>
        public String? CustomerId { get; set; }
        /// <summary>
        /// StartDate property for DTO.
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// CurrentPeriodStart property for DTO.
        /// </summary>
        public DateTime CurrentPeriodStart { get; set; }
        /// <summary>
        /// CurrentPeriodEnd property for DTO.
        /// </summary>
        public DateTime CurrentPeriodEnd { get; set; }
        /// <summary>
        /// Status property for DTO.
        /// </summary>
        public String? Status { get; set; }
        /// <summary>
        /// CancelAtPeriodEnd property for DTO.
        /// </summary>
        public Boolean CancelAtPeriodEnd { get; set; }
        /// <summary>
        /// NumberRequests property for DTO.
        /// </summary>
        public Int32 NumberRequests { get; set; }
        /// <summary>
        /// AllowedRequests property for DTO.
        /// </summary>
        public Int32 AllowedRequests { get; set; }
        /// <summary>
        /// AllowedSpaces property for DTO.
        /// </summary>
        public Int32 AllowedSpaces { get; set; }
        /// <summary>
        /// CancelAt property for DTO.
        /// </summary>
        public Nullable<DateTime> CancelAt { get; set; }
        /// <summary>
        /// CanceledAt property for DTO.
        /// </summary>
        public Nullable<DateTime> CanceledAt { get; set; }
        /// <summary>
        /// PlanId property for DTO.
        /// </summary>
        public String? PlanId { get; set; }
        public PlanResponseBuildDto? Plan { get; set; }
        /// <summary>
        /// UserId property for DTO.
        /// </summary>
        public String? UserId { get; set; }
        public ApplicationUserResponseBuildDto? User { get; set; }
        public ICollection<RequestResponseBuildDto>? Requests { get; set; }
        public ICollection<SpaceResponseBuildDto>? Spaces { get; set; }
    }
}