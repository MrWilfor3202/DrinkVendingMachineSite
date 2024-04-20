namespace DrinkVendingMachineSite.Models
{
    public class DrinkEntity : BaseEntity
    {
        public string Name { get; set; }
        
        public int Price { get; set; }

        public string PathToImage { get; set; }
    }
}
