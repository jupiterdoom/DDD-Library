using FluentAssertions;
using Library.Logic.Model;
using System;
using Xunit;

namespace Library.Tests
{
    public class BooksSpecs
    {
        [Fact]
        public void Create_book()
        {
            Book book1 = new Book("Война и мир", "Л. Толстой", false);

            book1.InArchive.Should().BeFalse();
            book1.BookReader.Should().BeNull();
        }

        [Fact]
        public void Move_book_to_archive()
        {
            Book book = new Book("Война и мир", "Л. Толстой", false);

            book.InArchive.Should().BeFalse();
            book.CanChangeInArchiveStatus().Should().HaveCount(0);

            Action action = () => book.ChangeInArchiveStatus(true);
            action.Should().NotThrow<InvalidOperationException>();

            book.InArchive.Should().BeTrue();
        }

        [Fact]
        public void Move_book_from_archive()
        {
            Book book = new Book("Война и мир", "Л. Толстой", false);

            book.InArchive.Should().BeFalse();
            book.CanChangeInArchiveStatus().Should().HaveCount(0);

            Action action = () => book.ChangeInArchiveStatus(false);
            action.Should().NotThrow<InvalidOperationException>();

            book.InArchive.Should().BeFalse();
        }

        [Fact]
        public void Move_book_to_archive_when_it_has_reader()
        {
            Book book = new Book("Война и мир", "Л. Толстой", false);
            Customer bookReader = new Customer("Тест", "test@mail.ru");

            book.InArchive.Should().BeFalse();
            book.CanChangeInArchiveStatus().Should().HaveCount(0);
            book.CanAddBookReader(bookReader).Should().HaveCount(0);

            Action addBookReader = () => book.AddBookReader(bookReader);
            addBookReader.Should().NotThrow<InvalidOperationException>();

            book.BookReader.Should().Be(bookReader);

            Action changeInArchiveStatus = () => book.ChangeInArchiveStatus(false);
            changeInArchiveStatus.Should().Throw<InvalidOperationException>();
        }
    }
}

