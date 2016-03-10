namespace Online_library.Web.Controllers
{
    using Microsoft.AspNet.Identity;
    using Models;
    using System.Linq;
    using System.Web.Mvc;
    using System;
    using Data;
    using PagedList;
    public class HomeController : BaseController
    { 
        public ActionResult Index(string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var books = this.db.Books
                .OrderBy(b => b.PublishedYear).
                Select(BookViewModel.ViewModel);

            //var bookFilterByAuthors = books.OrderBy(b => b.Author);
            var bookFilterByGenres = books.OrderBy(b => b.Genre);
            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.Title.Contains(searchString)
                                      || s.Author.Contains(searchString)
                                      || s.Genre.Contains(searchString));
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(books.ToPagedList(pageNumber, pageSize));

        }
              public ActionResult BookDetailsById(int id)
        {
            var currentUserId = this.User.Identity.GetUserId();
            var isAdmin = IsAdmin();
            var bookDetails = this.db.Books
                  .Where(e => e.Id == id)
                
                .Select(BookDetailsViewModel.ViewModel)
                .FirstOrDefault();

            var isOwner = (bookDetails != null && bookDetails.BookUserId != null &&
                bookDetails.BookUserId == currentUserId);
            this.ViewBag.CanEdit = isOwner || isAdmin;

            return this.PartialView("_BookDetails", bookDetails);
        }
        public ActionResult PostComment(PostCommentViewModel commentView)
        {
            if (ModelState.IsValid)
            {
                var userName = this.User.Identity.GetUserName();
                var UserId = this.User.Identity.GetUserId();


                this.db.Comments.Add(new Comment()
                {
                    BookUserId = UserId,
                    Content = commentView.Comment,
                    BookId=commentView.BookId
                });
                //this.db.SaveChanges();
                var viewModel = new CommentViewModel { BookUser= userName, Content = commentView.Comment };
                return PartialView("_CommentView", viewModel);
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, ModelState.Values.First().ToString());
        }


    }
}