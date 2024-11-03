namespace Rusu_Nicola_Lab2.Models.ViewModels
{
    public class CategoryIndexData
    {
        public List<Category> Categories { get; set; } = new List<Category>();
        public IEnumerable<Book> Books { get; set; } = new List<Book>();
    }
}
