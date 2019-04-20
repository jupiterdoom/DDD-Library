using FluentAssertions;
using Library.Logic.Common;
using Library.Logic.Model;
using System;
using System.Collections.Generic;
using Xunit;

namespace Library.Tests
{
    public class CustomersSpec
    {
        [Fact]
        public void Create_book_reader()
        {
            Customer bookReader = new Customer("Test", "test@mail.ru");

            bookReader.CustomerRole.Should().Equals(Role.BookReader);
        }

        [Fact]
        public void Purchase_book()
        {
            Customer bookReader = new Customer("Test", "test@mail.ru");
            Book book = new Book("Война и мир", "Л. Толстой");
            bookReader.CanPurchaseBook(book).Should().HaveCount(0);

            Action purchaseBook = () => bookReader.PurchaseBook(book);
            purchaseBook.Should().NotThrow<InvalidOperationException>();
        }

        [Fact]
        public void Cant_purchase_book_the_same_book_twice()
        {
            Customer bookReader = new Customer("Test", "test@mail.ru");
            Book book = new Book("Война и мир", "Л. Толстой");
            bookReader.CanPurchaseBook(book).Should().HaveCount(0);

            Action purchaseBook = () => bookReader.PurchaseBook(book);
            purchaseBook.Should().NotThrow<InvalidOperationException>();

            var canPurchaseBook = (List<ErrorCode>) bookReader.CanPurchaseBook(book);
            canPurchaseBook.Should().HaveCount(2);
            canPurchaseBook.Should().Contain(ErrorCode.YOU_HAVE_THIS_BOOK);
            canPurchaseBook.Should().Contain(ErrorCode.BOOK_IS_ALREADY_HAVE_A_READER);
        }

        [Fact]
        public void Only_archivarius_can_move_free_book_to_archive()
        {
            Customer bookReader = new Customer("Test", "test@mail.ru");
            Book book = new Book("Война и мир", "Л. Толстой");

            var canMoveBookToArchive = (List<ErrorCode>)bookReader.CanMoveBookToArchive(book);
            canMoveBookToArchive.Should().HaveCount(1);
            canMoveBookToArchive.Should().Contain(ErrorCode.ROLE_ERROR_ARCHIVARIUS);
        }
    }
}
