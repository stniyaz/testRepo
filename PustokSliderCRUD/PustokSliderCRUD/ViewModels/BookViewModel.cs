using PustokSliderCRUD.Models;

namespace PustokSliderCRUD.ViewModels
{
    public class BookViewModel
    {
        public List<Book> Books { get; set; }
        public List<Genre> Genres { get; set; }
        public List<Author> Authors { get; set; }
    }
}
