using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using System;

namespace ApiCore.DyModels.VMs
{
    /// <summary>
    /// Service  property for VM Create.
    /// </summary>
    public class ServiceCreateVM : ITVM
    {
        ///
        public String? Name { get; set; }
        ///
        public String? AbsolutePath { get; set; }
        ///
        public String? Token { get; set; }
        ///
        public String? ModelAiId { get; set; }
        public ModelAiCreateVM? ModelAi { get; set; }
        //
        public List<ServiceMethodCreateVM>? ServiceMethods { get; set; }
        //
        public List<UserServiceCreateVM>? UserServices { get; set; }
        //
        public List<RequestCreateVM>? Requests { get; set; }
        //
        public List<PlanServices>? PlanServices { get; set; }
    }
}