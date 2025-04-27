using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using System;

namespace ApiCore.DyModels.VMs
{
    /// <summary>
    /// ApplicationUser  property for VM Output.
    /// </summary>
    public class ApplicationUserOutputVM : ITVM
    {
        ///
        public String? CustomerId { get; set; }
        ///
        public String? FirstName { get; set; }
        ///
        public String? LastName { get; set; }
        ///
        public String? DisplayName { get; set; }
        ///
        public String? ProfileUrl { get; set; }
        ///
        public String? Image { get; set; }
        ///
        public Boolean IsArchived { get; set; }
        ///
        public Nullable<DateTime> ArchivedDate { get; set; }
        ///
        public String? LastLoginIp { get; set; }
        ///
        public Nullable<DateTime> LastLoginDate { get; set; }
        ///
        public DateTime CreatedAt { get; set; }
        ///
        public DateTime UpdatedAt { get; set; }
        public SubscriptionOutputVM? Subscription { get; set; }
        //
        public List<UserModelAiOutputVM>? UserModelAis { get; set; }
        //
        public List<UserServiceOutputVM>? UserServices { get; set; }
        //
        public List<RequestOutputVM>? Requests { get; set; }
        ///
        public String? Id { get; set; }
        ///
        public String? UserName { get; set; }
        ///
        public String? NormalizedUserName { get; set; }
        ///
        public String? Email { get; set; }
        ///
        public String? NormalizedEmail { get; set; }
        ///
        public Boolean EmailConfirmed { get; set; }
        ///
        public String? PasswordHash { get; set; }
        ///
        public String? SecurityStamp { get; set; }
        ///
        public String? ConcurrencyStamp { get; set; }
        ///
        public String? PhoneNumber { get; set; }
        ///
        public Boolean PhoneNumberConfirmed { get; set; }
        ///
        public Boolean TwoFactorEnabled { get; set; }
        ///
        public Nullable<DateTimeOffset> LockoutEnd { get; set; }
        ///
        public Boolean LockoutEnabled { get; set; }
        ///
        public Int32 AccessFailedCount { get; set; }
    }
}