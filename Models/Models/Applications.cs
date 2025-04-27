//using Microsoft.AspNetCore.Identity;

//namespace LAHJAAPI.Models
//{
//    public class ApplicationRole : IdentityRole 
//    {
//        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
//        public virtual ICollection<ApplicationRoleClaim> RoleClaims { get; set; }
//    }

//    public class ApplicationUserRole : Microsoft.AspNetCore.Identity.IdentityUserRole<string>
//    {
//        public virtual ApplicationUser User { get; set; }
//        public virtual ApplicationRole Role { get; set; }
//    }

//    public class ApplicationUserClaim : IdentityUserClaim<string>
//    {
//        public virtual ApplicationUser User { get; set; }
//    }

//    public class ApplicationUserLogin : IdentityUserLogin<string>
//    {
//        public virtual ApplicationUser User { get; set; }
//    }

//    public class ApplicationRoleClaim : IdentityRoleClaim<string>
//    {
//        public virtual ApplicationRole Role { get; set; }
//    }

//    public class ApplicationUserToken : IdentityUserToken<string>
//    {
//        public virtual ApplicationUser User { get; set; }
//    }
//}
