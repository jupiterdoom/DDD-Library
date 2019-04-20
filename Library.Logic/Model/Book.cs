using Library.Logic.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Logic.Model
{
    public class Book : Entity
    {
        public string Title { get; private set; }
        public string Author { get; private set; }

        public bool InArchive { get; private set; }

        public Customer BookReader { get; private set; }

        public Book(string title, string author)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullException(nameof(title));
            Title = title;

            if (string.IsNullOrWhiteSpace(author))
                throw new ArgumentNullException(nameof(author));
            Author = author;

            InArchive = false;
            BookReader = null;
        }

        public Book(string title, string author, bool inArchive)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullException(nameof(title));
            Title = title;

            if (string.IsNullOrWhiteSpace(author))
                throw new ArgumentNullException(nameof(author));
            Author = author;

            InArchive = inArchive;
            BookReader = null;
        }

        public IReadOnlyList<ErrorCode> CanChangeInArchiveStatus()
        {
            var errors = new List<ErrorCode>();
            if (BookReader != null)
                errors.Add(ErrorCode.BOOK_IS_ALREADY_HAVE_A_READER);

            return errors;
        }

        public void ChangeInArchiveStatus(bool inArchive)
        {
            if (CanChangeInArchiveStatus().Any())
                throw new InvalidOperationException();

            InArchive = inArchive;
        }

        public IReadOnlyList<ErrorCode> CanAddBookReader(Customer bookReader)
        {
            var errors = new List<ErrorCode>();           
     
            if(InArchive == true)
                errors.Add(ErrorCode.BOOK_IN_ARCHIVE);
            if (BookReader != null)
                errors.Add(ErrorCode.BOOK_IS_ALREADY_HAVE_A_READER);
            if (bookReader == null)
                errors.Add(ErrorCode.CUSTOMER_NULL_ERROR);

            return errors;
        }

        public void AddBookReader(Customer bookReader)
        {
            if (CanAddBookReader(bookReader).Any())
                throw new InvalidOperationException();

            BookReader = bookReader;
        }

        public IReadOnlyList<ErrorCode> CanClearBookReader()
        {
            var errors = new List<ErrorCode>();

            if (InArchive == true)
                errors.Add(ErrorCode.BOOK_IN_ARCHIVE);

            return errors;
        }

        public void ClearBookReader()
        {
            if (CanClearBookReader().Any())
                throw new InvalidOperationException();

            BookReader = null;
        }
    }
}
