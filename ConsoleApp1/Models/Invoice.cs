
using System.ComponentModel.DataAnnotations;

namespace APILAHJA.Models
{
    public interface ITModel { }


    //  اذا  لم تستخدم الخاصية او كانت false   
    //  تعمل  المبر بشكل  تلاقائي 

    
    
    public class Invoice : ITModel
    {
        [Key]
        public required string Id { get; set; }
        public required string CustomerId { get; set; }
        public required string Status { get; set; }
        public required string Url { get; set; }
        public DateTime? InvoiceDate { get; set; }

        public string? Description { get; set; }
       
    }
 
  
}
