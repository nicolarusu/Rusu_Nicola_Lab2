using Microsoft.AspNetCore.Mvc.RazorPages;
using Rusu_Nicola_Lab2.Data;
using System.Collections.Generic;
using System.Linq;

namespace Rusu_Nicola_Lab2.Models
{
    public class BookCategoriesPageModel : PageModel
    {
        public List<AssignedCategoryData> AssignedCategoryDataList { get; set; } = new List<AssignedCategoryData>();

        public void PopulateAssignedCategoryData(Rusu_Nicola_Lab2Context context, Book book)
        {
            // Verifică dacă contextul este null
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context), "Context cannot be null.");
            }

            // Verifică dacă book este null
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book), "Book cannot be null.");
            }

            // Asigură-te că BookCategories nu este null
            var bookCategories = book.BookCategories ?? new List<BookCategory>();
            var bookCategoryIds = new HashSet<int>(bookCategories.Select(c => c.CategoryID)); // Asigură-te că este inițializat

            var allCategories = context.Category.ToList(); // Asigură-te că obții o listă validă

            foreach (var cat in allCategories)
            {
                AssignedCategoryDataList.Add(new AssignedCategoryData
                {
                    CategoryID = cat.ID,
                    Name = cat.CategoryName,
                    Assigned = bookCategoryIds.Contains(cat.ID)
                });
            }
        }

        public void UpdateBookCategories(Rusu_Nicola_Lab2Context context, string[] selectedCategories, Book bookToUpdate)
        {
            if (selectedCategories == null)
            {
                bookToUpdate.BookCategories = new List<BookCategory>();
                return;
            }

            var selectedCategoriesHS = new HashSet<string>(selectedCategories);
            var bookCategories = new HashSet<int>(bookToUpdate.BookCategories.Select(c => c.Category.ID));

            foreach (var cat in context.Category)
            {
                if (selectedCategoriesHS.Contains(cat.ID.ToString()))
                {
                    if (!bookCategories.Contains(cat.ID))
                    {
                        bookToUpdate.BookCategories.Add(new BookCategory
                        {
                            BookID = bookToUpdate.ID,
                            CategoryID = cat.ID
                        });
                    }
                }
                else
                {
                    if (bookCategories.Contains(cat.ID))
                    {
                        BookCategory bookToRemove = bookToUpdate.BookCategories.SingleOrDefault(i => i.CategoryID == cat.ID);
                        if (bookToRemove != null)
                        {
                            context.Remove(bookToRemove);
                        }
                    }
                }
            }
        }
    }
}
