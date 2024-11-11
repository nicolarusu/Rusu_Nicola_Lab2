using Rusu_Nicola_Lab2.Models.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Rusu_Nicola_Lab2.Models
{
    public class Member
    {
        public int ID { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z\s-]*$", ErrorMessage = "Prenumele trebuie sa inceapa cu majuscula (ex. Ana sau Ana Maria sau Ana-Maria")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Prenumele trebuie să aibă între 3 și 30 de caractere")]
        public string? FirstName { get; set; }

        [RegularExpression(@"^[A-Z]+[a-z\s]*$", ErrorMessage = "Numele trebuie sa inceapa cu majuscula și să conțină doar litere și spații")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Numele trebuie să aibă între 3 și 30 de caractere")]
        public string? LastName { get; set; }

        [StringLength(70, ErrorMessage = "Adresa nu poate depăși 70 de caractere")]
        public string? Adress { get; set; }

        public string Email { get; set; }

        [RegularExpression(@"^0\d{3}[-. ]?\d{3}[-. ]?\d{3}$", ErrorMessage = "Telefonul trebuie să înceapă cu 0 și să fie de forma '0722-123-123', '0722.123.123' sau '0722 123 123'")]
        public string? Phone { get; set; }

        [Display(Name = "Full Name")]
        public string? FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public ICollection<Borrowing>? Borrowings { get; set; }
    }
}
