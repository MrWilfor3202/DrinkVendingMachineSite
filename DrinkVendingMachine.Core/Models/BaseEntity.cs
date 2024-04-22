using System.ComponentModel.DataAnnotations;

namespace DrinkVendingMachine.Core.Models
{
    public abstract class BaseEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }
    }
}
