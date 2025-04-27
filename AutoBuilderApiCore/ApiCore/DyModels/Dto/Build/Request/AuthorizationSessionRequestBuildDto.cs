using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using AutoGenerator.Config;
using System;

namespace ApiCore.DyModels.Dto.Build.Requests
{
    public class AuthorizationSessionRequestBuildDto : ITBuildDto
    {
        /// <summary>
        /// Id property for DTO.
        /// </summary>
        public String? Id { get; set; }
        /// <summary>
        /// SessionToken property for DTO.
        /// </summary>
        public String? SessionToken { get; set; }
        /// <summary>
        /// UserToken property for DTO.
        /// </summary>
        public String? UserToken { get; set; }
        /// <summary>
        /// AuthorizationType property for DTO.
        /// </summary>
        public String? AuthorizationType { get; set; }
        /// <summary>
        /// StartTime property for DTO.
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// EndTime property for DTO.
        /// </summary>
        public Nullable<DateTime> EndTime { get; set; }
        /// <summary>
        /// IsActive property for DTO.
        /// </summary>
        public Boolean IsActive { get; set; }
        /// <summary>
        /// UserId property for DTO.
        /// </summary>
        public String? UserId { get; set; }
        public ApplicationUserRequestBuildDto? User { get; set; }
        /// <summary>
        /// IpAddress property for DTO.
        /// </summary>
        public String? IpAddress { get; set; }
        /// <summary>
        /// DeviceInfo property for DTO.
        /// </summary>
        public String? DeviceInfo { get; set; }
        /// <summary>
        /// ServicesIds property for DTO.
        /// </summary>
        public String? ServicesIds { get; set; }
    }
}