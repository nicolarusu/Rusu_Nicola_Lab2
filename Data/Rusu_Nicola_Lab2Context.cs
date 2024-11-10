using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rusu_Nicola_Lab2.Models;
using Rusu_Nicola_Lab2.Models.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Rusu_Nicola_Lab2.Data
{
    public class Rusu_Nicola_Lab2Context : IdentityDbContext
    {
        public Rusu_Nicola_Lab2Context(DbContextOptions<Rusu_Nicola_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Rusu_Nicola_Lab2.Models.Book> Book { get; set; } = default!;
        public DbSet<Rusu_Nicola_Lab2.Models.Publisher> Publisher { get; set; } = default!;
        public DbSet<Rusu_Nicola_Lab2.Models.Author> Authors { get; set; } = default!;
        public DbSet<Rusu_Nicola_Lab2.Models.Category> Category { get; set; } = default!;
        public DbSet<Rusu_Nicola_Lab2.Models.Member> Member { get; set; } = default!;
        public DbSet<Rusu_Nicola_Lab2.Models.ViewModels.Borrowing> Borrowing { get; set; } = default!;
    }
}
