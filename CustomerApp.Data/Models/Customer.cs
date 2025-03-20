using System.ComponentModel.DataAnnotations;

namespace CustomerApp.Data.Models
{
    public class Customer
    {
        /// <summary>
        /// Primary key for Customer entity
        /// </summary>
        [Key]
        public int CustomerID { get; set; }
        /// <summary>
        /// Customer's name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Customer's email address
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Customer's phone number
        /// </summary>
        public string PhoneNumber { get; set; }
    }
}
