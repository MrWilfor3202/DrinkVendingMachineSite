using System.ComponentModel.DataAnnotations;

namespace DrinkVendingMachine.Core.Models
{
    public class CoinEntity : BaseEntity
    {
        public bool Locked { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Count { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Price { get; set; }
    }
}
