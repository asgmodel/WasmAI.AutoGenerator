using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using System;

namespace ApiCore.DyModels.VMs
{
    /// <summary>
    /// AuthorizationSession  property for VM Create.
    /// </summary>
    public class AuthorizationSessionCreateVM : ITVM
    {
        ///
        public String? SessionToken { get; set; }
        ///
        public String? UserToken { get; set; }
        ///
        public String? AuthorizationType { get; set; }
        ///
        public DateTime StartTime { get; set; }
        ///
        public Nullable<DateTime> EndTime { get; set; }
        ///
        public Boolean IsActive { get; set; }
        ///
        public String? UserId { get; set; }
        public ApplicationUserCreateVM? User { get; set; }
        ///
        public String? IpAddress { get; set; }
        ///
        public String? DeviceInfo { get; set; }
        ///
        public String? ServicesIds { get; set; }
    }
}