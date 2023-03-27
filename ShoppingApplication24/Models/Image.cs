namespace ShoppingApplication24.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public virtual Product Product { get; set; }
        public int ProductId { get; set; }

    }
}
