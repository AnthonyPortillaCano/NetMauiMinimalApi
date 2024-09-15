using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo nombre es obligatorio")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "El campo precio es obligatorio")]
        public decimal? Price { get; set; }
        [Required(ErrorMessage = "El campo tipo es obligatorio")]
        public string ProductType { get; set; }
        public double totalPages { get; set; }
    }
}
