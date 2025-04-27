using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using System;

namespace ApiCore.DyModels.VMs
{
    /// <summary>
    /// UserService  property for VM Create.
    /// </summary>
    public class UserServiceCreateVM : ITVM
    {
        ///
        public DateTime CreatedAt { get; set; }
        ///
        public String? UserId { get; set; }
        public ApplicationUserCreateVM? User { get; set; }
        ///
        public String? ServiceId { get; set; }
        public ServiceCreateVM? Service { get; set; }
    }
}