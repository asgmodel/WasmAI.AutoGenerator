using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using AutoGenerator.Config;
using System;

namespace ApiCore.DyModels.Dto.Build.ResponseFilters
{
    public class ApplicationUserResponseFilterBuildDto : ITBuildDto
    {
        /// <summary>
        /// CustomerId property for DTO.
        /// </summary>
        public String? CustomerId { get; set; }
        /// <summary>
        /// FirstName property for DTO.
        /// </summary>
        public String? FirstName { get; set; }
        /// <summary>
        /// LastName property for DTO.
        /// </summary>
        public String? LastName { get; set; }
        /// <summary>
        /// DisplayName property for DTO.
        /// </summary>
        public String? DisplayName { get; set; }
        /// <summary>
        /// ProfileUrl property for DTO.
        /// </summary>
        public String? ProfileUrl { get; set; }
        /// <summary>
        /// Image property for DTO.
        /// </summary>
        public String? Image { get; set; }
        /// <summary>
        /// IsArchived property for DTO.
        /// </summary>
        public Boolean IsArchived { get; set; }
        /// <summary>
        /// ArchivedDate property for DTO.
        /// </summary>
        public Nullable<DateTime> ArchivedDate { get; set; }
        /// <summary>
        /// LastLoginIp property for DTO.
        /// </summary>
        public String? LastLoginIp { get; set; }
        /// <summary>
        /// LastLoginDate property for DTO.
        /// </summary>
        public Nullable<DateTime> LastLoginDate { get; set; }
        /// <summary>
        /// CreatedAt property for DTO.
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// UpdatedAt property for DTO.
        /// </summary>
        public DateTime UpdatedAt { get; set; }
        public SubscriptionResponseFilterBuildDto? Subscription { get; set; }
        public ICollection<UserModelAiResponseFilterBuildDto>? UserModelAis { get; set; }
        public ICollection<UserServiceResponseFilterBuildDto>? UserServices { get; set; }
        public ICollection<RequestResponseFilterBuildDto>? Requests { get; set; }
        /// <summary>
        /// Id property for DTO.
        /// </summary>
        public String? Id { get; set; }
        /// <summary>
        /// UserName property for DTO.
        /// </summary>
        public String? UserName { get; set; }
        /// <summary>
        /// NormalizedUserName property for DTO.
        /// </summary>
        public String? NormalizedUserName { get; set; }
        /// <summary>
        /// Email property for DTO.
        /// </summary>
        public String? Email { get; set; }
        /// <summary>
        /// NormalizedEmail property for DTO.
        /// </summary>
        public String? NormalizedEmail { get; set; }
        /// <summary>
        /// EmailConfirmed property for DTO.
        /// </summary>
        public Boolean EmailConfirmed { get; set; }
        /// <summary>
        /// PasswordHash property for DTO.
        /// </summary>
        public String? PasswordHash { get; set; }
        /// <summary>
        /// SecurityStamp property for DTO.
        /// </summary>
        public String? SecurityStamp { get; set; }
        /// <summary>
        /// ConcurrencyStamp property for DTO.
        /// </summary>
        public String? ConcurrencyStamp { get; set; }
        /// <summary>
        /// PhoneNumber property for DTO.
        /// </summary>
        public String? PhoneNumber { get; set; }
        /// <summary>
        /// PhoneNumberConfirmed property for DTO.
        /// </summary>
        public Boolean PhoneNumberConfirmed { get; set; }
        /// <summary>
        /// TwoFactorEnabled property for DTO.
        /// </summary>
        public Boolean TwoFactorEnabled { get; set; }
        /// <summary>
        /// LockoutEnd property for DTO.
        /// </summary>
        public Nullable<DateTimeOffset> LockoutEnd { get; set; }
        /// <summary>
        /// LockoutEnabled property for DTO.
        /// </summary>
        public Boolean LockoutEnabled { get; set; }
        /// <summary>
        /// AccessFailedCount property for DTO.
        /// </summary>
        public Int32 AccessFailedCount { get; set; }

        [FilterLGEnabled]
        public string? Lg { get; set; }
    }
}