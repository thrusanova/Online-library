
using System;
using Online_library.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Online_library.Web.Models
{
    public class BookDetailsViewModel
    {
        public int Id { get; set; }

        public string BookUserId { get; set; }

        public string ImageUrl { get; set; }
        
        public string PublishedYear { get; set; }

        public  IEnumerable<CommentViewModel>Comments { get; set; }

        public static Expression<Func<Book, BookDetailsViewModel>> ViewModel
        {
            get
            {
                return b => new BookDetailsViewModel()
                {
                    Id = b.Id,
                    BookUserId = b.BookUserId,
                    ImageUrl = b.ImageUrl,
                    PublishedYear = b.PublishedYear,
                    Comments=b.Comments.AsQueryable().Select(CommentViewModel.ViewModel),
                   
                };
            }
        }
    }
}