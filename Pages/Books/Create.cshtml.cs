using Rusu_Nicola_Lab2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rusu_Nicola_Lab2.Data;
using Microsoft.AspNetCore.Authorization;

namespace Rusu_Nicola_Lab2.Pages.Books
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : BookCategoriesPageModel
    {
        private readonly Rusu_Nicola_Lab2.Data.Rusu_Nicola_Lab2Context _context;

        public CreateModel(Rusu_Nicola_Lab2.Data.Rusu_Nicola_Lab2Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var authorList = _context.Authors.Select(x => new
            {
                x.ID,
                FullName = x.FirstName + " " + x.LastName
            });
            var book = new Book();
            book.BookCategories = new List<BookCategory>();
            PopulateAssignedCategoryData(_context, book);

            ViewData["AuthorID"] = new SelectList(authorList, "ID", "FullName");
            ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID", "PublisherName");
            return Page();
        }

        [BindProperty]
        public Book Book { get; set; }

        public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
        {
            if (!ModelState.IsValid)
            {
                // Populează din nou datele necesare în cazul în care validarea nu reușește
                var authorList = _context.Authors.Select(x => new
                {
                    x.ID,
                    FullName = x.FirstName + " " + x.LastName
                });
                ViewData["AuthorID"] = new SelectList(authorList, "ID", "FullName");
                ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID", "PublisherName");
                return Page();
            }

            // Initializează lista de categorii pentru noul book
            if (selectedCategories != null)
            {
                Book.BookCategories = new List<BookCategory>();
                foreach (var cat in selectedCategories)
                {
                    var catToAdd = new BookCategory
                    {
                        CategoryID = int.Parse(cat)
                    };
                    Book.BookCategories.Add(catToAdd);
                }
            }

            _context.Book.Add(Book);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
