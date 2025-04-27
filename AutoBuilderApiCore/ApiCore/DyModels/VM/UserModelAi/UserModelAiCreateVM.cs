using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using System;

namespace ApiCore.DyModels.VMs
{
    /// <summary>
    /// UserModelAi  property for VM Create.
    /// </summary>
    public class UserModelAiCreateVM : ITVM
    {
        ///
        public DateTime CreatedAt { get; set; }
        ///
        public String? UserId { get; set; }
        public ApplicationUserCreateVM? User { get; set; }
        ///
        public String? ModelAiId { get; set; }
        public ModelAiCreateVM? ModelAi { get; set; }
    }
}