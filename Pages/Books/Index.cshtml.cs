using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Rusu_Nicola_Lab2.Data;
using Rusu_Nicola_Lab2.Models;

namespace Rusu_Nicola_Lab2.Pages.Books1
{
    public class IndexModel : PageModel
    {
        private readonly Rusu_Nicola_Lab2.Data.Rusu_Nicola_Lab2Context _context;

        public IndexModel(Rusu_Nicola_Lab2.Data.Rusu_Nicola_Lab2Context context)
        {
            _context = context;
        }

        public IList<Book> Book { get; set; } = default!;
        public BookData BookD { get; set; }
        public int BookID { get; set; }
        public int CategoryID { get; set; }
        public string TitleSort { get; set; }
        public string AuthorSort { get; set; }
        public string CurrentFilter { get; set; }

        public async Task OnGetAsync(int? id, int? categoryID, string sortOrder, string searchString)
        {
            BookD = new BookData();

            // Setting sorting parameters
            TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            AuthorSort = sortOrder == "author" ? "author_desc" : "author";
            CurrentFilter = searchString;

            // Fetching the list of books with includes and sorting by title initially
            BookD.Books = await _context.Book
                .Include(b => b.Publisher)
                .Include(b => b.Author)
                .Include(b => b.BookCategories)
                    .ThenInclude(bc => bc.Category)
                .AsNoTracking()
                .OrderBy(b => b.Title)
                .ToListAsync();

            // Filtering by search string if provided
            if (!String.IsNullOrEmpty(searchString))
            {
                BookD.Books = BookD.Books
                    .Where(s => s.Author.FirstName.Contains(searchString)
                             || s.Author.LastName.Contains(searchString)
                             || s.Title.Contains(searchString))
                    .ToList();
            }

            // Checking if an ID was provided to select a specific book
            if (id != null)
            {
                BookID = id.Value;
                Book selectedBook = BookD.Books.SingleOrDefault(i => i.ID == id.Value);
                if (selectedBook != null)
                {
                    BookD.Categories = selectedBook.BookCategories.Select(s => s.Category).ToList();
                }
            }

            // Sorting the books based on sortOrder
            switch (sortOrder)
            {
                case "title_desc":
                    BookD.Books = BookD.Books.OrderByDescending(s => s.Title).ToList();
                    break;
                case "author_desc":
                    BookD.Books = BookD.Books.OrderByDescending(s => s.Author.FullName).ToList();
                    break;
                case "author":
                    BookD.Books = BookD.Books.OrderBy(s => s.Author.FullName).ToList();
                    break;
                default:
                    BookD.Books = BookD.Books.OrderBy(s => s.Title).ToList();
                    break;
            }
        }
    }
}
