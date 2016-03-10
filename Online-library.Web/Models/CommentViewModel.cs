
using Online_library.Data;
using System;
using System.Linq.Expressions;

namespace Online_library.Web.Models
{
    public class CommentViewModel
    {
        public string Content { get; set; }

        public string BookUser { get; set; }

        public static Expression<Func<Comment, CommentViewModel>> ViewModel
        {
            get
            {
                return c => new CommentViewModel()
                {
                    Content = c.Content,
                    BookUser = c.BookUser.FullName
                };
            }
        }
    }
}