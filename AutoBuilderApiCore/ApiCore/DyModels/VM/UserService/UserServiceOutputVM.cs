using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using System;

namespace ApiCore.DyModels.VMs
{
    /// <summary>
    /// UserService  property for VM Output.
    /// </summary>
    public class UserServiceOutputVM : ITVM
    {
        ///
        public Int32 Id { get; set; }
        ///
        public DateTime CreatedAt { get; set; }
        ///
        public String? UserId { get; set; }
        public ApplicationUserOutputVM? User { get; set; }
        ///
        public String? ServiceId { get; set; }
        public ServiceOutputVM? Service { get; set; }
    }
}