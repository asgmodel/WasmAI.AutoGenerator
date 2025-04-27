using AutoGenerator;
using System.ComponentModel.DataAnnotations;

namespace LAHJAAPI.Models
{

    
    public class Invoice: ITModel
    {
        [Key]
        public required string Id { get; set; }
        public required string CustomerId { get; set; }
        public required string Status { get; set; }
        public required string Url { get; set; }
        public DateTime? InvoiceDate { get; set; }
    }


 


}
