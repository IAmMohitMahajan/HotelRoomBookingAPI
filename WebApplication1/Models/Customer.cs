using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerEmailId { get; set; }
        public string CustomerGender { get; set; }
        public long ContactNumber { get; set; }
        public DateTime CustomerDateOfBirth { get; set; }
        public string CustomerPassword { get; set; }

        //Relationship
        public virtual List<Booking> Bookings { get; set; }
        public Payment Payment { get; set; }
    }
}
