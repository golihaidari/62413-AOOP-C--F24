using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class Address
    {
       /* [Key]
        public Guid Id { get; set; }*/

        [Required(ErrorMessage = "FirstName cannot be empty")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "LastName cannot be empty")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Email cannot be empty")]

        [Key]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Mobile number cannot be empty")]
        public string MobileNr { get; set; } = null!;

        [Required(ErrorMessage = "Company cannot be empty")]
        public string Company { get; set; } = null!;

        [Required(ErrorMessage = "Vat number cannot be empty")]
        public string VatNr { get; set; } = null!;

        [Required(ErrorMessage = "Country cannot be empty")]
        public string Country { get; set; } = null!;

        [Required(ErrorMessage = "ZipCode cannot be empty")]
        public string ZipCode { get; set; } = null!;

        [Required(ErrorMessage = "City cannot be empty")]
        public string City { get; set; } = null!;

        [Required(ErrorMessage = "AddressLine1 cannot be empty")]
        public string Address1 { get; set; } = null!;

        [Required(ErrorMessage = "AddressLine2 cannot be empty")]
        public string Address2 { get; set; } = null!;

        
        [ForeignKey(nameof(Email))]
        public virtual User? Customer { get; set; }

    }
}
