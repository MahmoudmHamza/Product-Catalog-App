namespace ProductsCatalogApp.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Filename { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}