using Library.Logic.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Logic.Model
{
    public class Customer : Entity
    {
        public const byte MAX_BOOK_COUNT = 3;

        public IList<PurchasedBook> PurchasedBooks { get; private set; }

        public string Name { get; private set; }
        public string Email { get; private set; }

        public Role CustomerRole { get; private set; }

        public Customer(string name, string email)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Email = email ?? throw new ArgumentNullException(nameof(email));

            PurchasedBooks = new List<PurchasedBook>();
            CustomerRole = Role.BookReader;
        }

        public IReadOnlyList<ErrorCode> CanMoveBookToArchive(Book book)
        {
            var errors = (List<ErrorCode>) book.CanChangeInArchiveStatus();

            if (!CustomerRole.IsArchivarius())
                errors.Add(ErrorCode.ROLE_ERROR_ARCHIVARIUS);
            if (book.InArchive)
                errors.Add(ErrorCode.BOOK_IN_ARCHIVE);

            return errors;
        }

        public void MoveBookToArchive(Book book)
        {
            if (CanMoveBookToArchive(book).Any())
                throw new InvalidOperationException();

            book.ChangeInArchiveStatus(true);
        }

        public IReadOnlyList<ErrorCode> CanGetBookFromArchive(Book book)
        {
            var errors = (List<ErrorCode>)book.CanChangeInArchiveStatus();

            if (!CustomerRole.IsArchivarius())
                errors.Add(ErrorCode.ROLE_ERROR_ARCHIVARIUS);
            if (!book.InArchive)
                errors.Add(ErrorCode.BOOK_ISNT_IN_ARCHIVE);

            return errors;
        }

        public void GetBookFromArchive(Book book)
        {
            if (CanGetBookFromArchive(book).Any())
                throw new InvalidOperationException();

            book.ChangeInArchiveStatus(false);
        }

        public bool HasPurchasedBook(Book book)
        {
            return PurchasedBooks.Any(x => x.Book == book);
        }

        public IReadOnlyList<ErrorCode> CanPurchaseBook(Book book)
        {
            var errors = (List<ErrorCode>) book.CanAddBookReader(this);

            if (HasPurchasedBook(book))
                errors.Add(ErrorCode.YOU_HAVE_THIS_BOOK);
            if (PurchasedBooks.Count() >= MAX_BOOK_COUNT)
                errors.Add(ErrorCode.BOOK_LIMIT_ERROR);

            return errors;
        }

        public void PurchaseBook(Book book)
        {
            if (CanPurchaseBook(book).Any())
                throw new InvalidOperationException();

            book.AddBookReader(this);
            var purchasedBook = new PurchasedBook(book, this);
            PurchasedBooks.Add(purchasedBook);
        }                                    
    }
}
