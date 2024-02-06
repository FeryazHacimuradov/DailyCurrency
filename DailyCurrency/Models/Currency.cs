using System.ComponentModel.DataAnnotations;

namespace DailyCurrency.Models
{
    public class Currency
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50), Required]
        public string Code { get; set; }
        [MaxLength(50), Required]
        public string Nominal { get; set; }
        [MaxLength(100), Required]
        public string Name { get; set; }
        public decimal? Value { get; set; }
    }
}
