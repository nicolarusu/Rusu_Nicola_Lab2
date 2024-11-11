using Rusu_Nicola_Lab2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rusu_Nicola_Lab2.Data;
using Rusu_Nicola_Lab2.Models;
using Microsoft.AspNetCore.Authorization;

namespace Rusu_Nicola_Lab2.Pages.Books
{
    [Authorize(Roles = "Admin")]
    public class EditModel : BookCategoriesPageModel
    {
        private readonly Rusu_Nicola_Lab2.Data.Rusu_Nicola_Lab2Context _context;

        public EditModel(Rusu_Nicola_Lab2.Data.Rusu_Nicola_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Book = await _context.Book
 .Include(b => b.Publisher)
 .Include(b => b.Author)
 .Include(b => b.BookCategories).ThenInclude(b => b.Category)
 .AsNoTracking()
 .FirstOrDefaultAsync(m => m.ID == id);

            var book = await _context.Book.FirstOrDefaultAsync(m => m.ID == id);
            if (book == null)
            {
                return NotFound();
            }

            Book = book;
            PopulateAssignedCategoryData(_context, Book);

            var authorList = _context.Authors.Select(x => new
            {
                x.ID,
                FullName = x.FirstName + " " + x.LastName
            });
            ViewData["AuthorID"] = new SelectList(_context.Set<Models.Author>(), "ID",
"FullName");
            ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID",
"PublisherName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[]
  selectedCategories)
        {
            if (id == null)
            {
                return NotFound();
            }
            //se va include Author conform cu sarcina de la lab 2
            var bookToUpdate = await _context.Book
            .Include(i => i.Publisher)
            .Include(i => i.BookCategories)
            .ThenInclude(i => i.Category)
            .FirstOrDefaultAsync(s => s.ID == id);
            if (bookToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Book>(
  bookToUpdate,
  "Book",
  i => i.Title, i => i.Author,
  i => i.Price, i => i.PublishingDate, i => i.PublisherID))
            {
                UpdateBookCategories(_context, selectedCategories, bookToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            UpdateBookCategories(_context, selectedCategories, bookToUpdate);
            PopulateAssignedCategoryData(_context, bookToUpdate);
            return Page();
        }
    }
}