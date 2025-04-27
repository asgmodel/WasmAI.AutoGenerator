using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using System;

namespace ApiCore.DyModels.VMs
{
    /// <summary>
    /// AuthorizationSession  property for VM Update.
    /// </summary>
    public class AuthorizationSessionUpdateVM : ITVM
    {
        ///
        public string? Id { get; set; }
        ///
        public AuthorizationSessionCreateVM? Body { get; set; }
    }
}