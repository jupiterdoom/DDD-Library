using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Logic.Common
{
    public enum ErrorCode
    {
        ROLE_ERROR_ARCHIVARIUS,
        BOOK_IN_ARCHIVE,
        BOOK_ISNT_IN_ARCHIVE,
        YOU_HAVE_THIS_BOOK,
        BOOK_LIMIT_ERROR,
        BOOK_IS_ALREADY_HAVE_A_READER,
        BOOK_DOESNT_HAVE_A_READER,
        CUSTOMER_NULL_ERROR

    }

    public static class ErrorLevelExtensions
    {
        public static string ToFriendlyString(this ErrorCode me)
        {
            switch (me)
            {
                case ErrorCode.ROLE_ERROR_ARCHIVARIUS:
                    return "Only archivarius can do it";
                case ErrorCode.BOOK_IN_ARCHIVE:
                    return "This book in archive";
                case ErrorCode.BOOK_ISNT_IN_ARCHIVE:
                    return "This book isn't in archive";
                case ErrorCode.YOU_HAVE_THIS_BOOK:
                    return "You are already have this book";
                case ErrorCode.BOOK_LIMIT_ERROR:
                    return "You cannot have more than your book limit";
                case ErrorCode.BOOK_IS_ALREADY_HAVE_A_READER:
                    return "This book is already have a reader";
                case ErrorCode.BOOK_DOESNT_HAVE_A_READER:
                    return "This book doesn't have a reader";
                case ErrorCode.CUSTOMER_NULL_ERROR:
                    return "The customer shouldn't be null";
                default:
                    return "Something went wrong";
            }
        }
    }
}
