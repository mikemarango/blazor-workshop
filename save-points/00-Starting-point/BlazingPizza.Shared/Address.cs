using System.ComponentModel.DataAnnotations;

namespace BlazingPizza
{
    public class Address
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required!"), MaxLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Delivery address is required!"), MaxLength(100)]
        public string Line1 { get; set; }

        [MaxLength(100)]
        public string Line2 { get; set; }

        [Required(ErrorMessage = "Enter the City for the delivery address"), MaxLength(50)]
        public string City { get; set; }

        [Required(ErrorMessage = "Enter region/province/state/county/borough"), MaxLength(20)]
        public string Region { get; set; }

        [Required(ErrorMessage = "Please enter your postal address"), MaxLength(20)]
        public string PostalCode { get; set; }
    }
}
