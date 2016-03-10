using Online_library.Data;
using System;
using System.Linq.Expressions;

namespace Online_library.Web.Models
{
    public class BookViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public string Genre { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public string PublishedYear { get; set; }

        public string BookUser { get; set; }

        public static Expression <Func<Book,BookViewModel>>ViewModel
        {
            get
            {
                return b => new BookViewModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    ImageUrl = b.ImageUrl,
                    Description = b.Description,
                    PublishedYear = b.PublishedYear,
                    Genre = b.Genre.Title,
                    BookUser = b.BookUser.FullName
                };
            }
        }
    //  public  PagedList.IPagedList<BookViewModel> Steps { get; set; }


    }
}