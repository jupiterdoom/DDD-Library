using System;

namespace Library.Logic.Model
{
    public class PurchasedBook
    {
        public Book Book { get; private set; }
        public Customer Customer { get; private set; }

        private PurchasedBook()
        {
        }

        internal PurchasedBook(Book book, Customer customer)
        {
            Book = book ?? throw new ArgumentNullException(nameof(book));
            Customer = customer ?? throw new ArgumentNullException(nameof(customer));
        }
    }
}
