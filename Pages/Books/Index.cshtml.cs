﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Rusu_Nicola_Lab2.Data;
using Rusu_Nicola_Lab2.Models;

namespace Rusu_Nicola_Lab2.Pages.Books
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
         {
            BookD = new BookData();

        BookD.Books = await _context.Book

        BookD.Books = await _context.Book
    
            if (id != null)
            {
                BookID = id.Value;
                Book book = BookD.Books
                .Where(i => i.ID == id.Value).Single();
        BookD.Categories = book.BookCategories.Select(s => s.Category);
            }
}
    }
}