using System.ComponentModel.DataAnnotations;

namespace DrinkVendingMachine.Core.Models
{
    public class DrinkEntity : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Price { get; set; }

        public string PathToImage { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Count { get; set; }
    }
}
