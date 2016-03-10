namespace Online_library.Web.Controllers
{
    using Data;
    using Extensions;
    using Microsoft.AspNet.Identity;
    using Models;
    using System.Linq;
    using System.Web.Mvc;
    using System;
    using System.Web;
    using System.IO;
    using PagedList;
    [Authorize]
    public class BooksController : BaseController
    {

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
    [ValidateAntiForgeryToken]
        public ActionResult Add(BookInputModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {

                var b = new Book()
                {
                    BookUserId = this.User.Identity.GetUserId(),
                    Id=model.Id,
                    Title = model.Title,
                    Author = model.Author,
                    Genre = model.Genre,
                    ImageUrl = model.ImageUrl,
                    PublishedYear = model.PublishedYear
                };
                   this.db.Books.Add(b);
                     this.db.SaveChanges();
                this.AddNotification("Book created", NotificationType.INFO);
                return this.RedirectToAction("My");
            }
            return this.View(model);
        }

        public ActionResult My(int? page)
        {
            string currentUserId = this.User.Identity.GetUserId();
            var books = this.db.Books.Where(b => b.BookUserId == currentUserId)
                .OrderBy(b => b.Title).Select(BookViewModel.ViewModel);
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            var bookByTitle = books.OrderBy(b => b.Title);
            return View(bookByTitle.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var bookToEdit = this.LoadBook(id);
            if (bookToEdit==null)
            {
                this.AddNotification("Cannot edit book #" + id, NotificationType.ERROR);
                return this.RedirectToAction("My");
            }
            var model = BookInputModel.CreateFromBook(bookToEdit);
            return this.View(model);
        }


        private Book LoadBook(int id)
        {
            var curentUserId = this.User.Identity.GetUserId();
            var isAdmin = this.IsAdmin();
            var bookToEdit = this.db.Books.Where(b=>b.Id==id).
              FirstOrDefault(b => b.BookUserId == curentUserId || isAdmin);
            return bookToEdit;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,BookInputModel model)
        {
            var bookToEdit = this.LoadBook(id);
            if (bookToEdit == null)
            {
                this.AddNotification("Cannot edit book #" + id, NotificationType.ERROR);
                return this.RedirectToAction("My");
            }
            if (model != null && this.ModelState.IsValid)
            {
                bookToEdit.Id = model.Id;
                bookToEdit.Title = model.Title;
                bookToEdit.Author = model.Author;
                bookToEdit.Genre = model.Genre;
                bookToEdit.PublishedYear = model.PublishedYear;
                bookToEdit.ImageUrl = model.ImageUrl;

                this.db.SaveChanges();
                this.AddNotification("Book edited.", NotificationType.INFO);
                return RedirectToAction("My");

            }
            return this.View(model);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var bookToDelete = this.LoadBook(id);
            if (bookToDelete == null)
            {
                this.AddNotification("Cannot delete event #" + id, NotificationType.ERROR);
                return this.RedirectToAction("My");
            }

            var model = BookInputModel.CreateFromBook(bookToDelete);
            return this.View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, BookInputModel model)
        {
            var bookToDelete = this.LoadBook(id);
            if (bookToDelete == null)
            {
                this.AddNotification("Cannot delete book #" + id, NotificationType.ERROR);
                return this.RedirectToAction("My");
            }
            if (model != null && this.ModelState.IsValid)
            {
                bookToDelete.Id = model.Id;
                bookToDelete.Title = model.Title;
                bookToDelete.Author = model.Author;
                bookToDelete.Genre = model.Genre;
                bookToDelete.PublishedYear = model.PublishedYear;
                bookToDelete.ImageUrl = model.ImageUrl;
                this.db.Books.Remove(bookToDelete);
                this.db.SaveChanges();
                this.AddNotification("Book deleted.", NotificationType.INFO);
                return RedirectToAction("My");

            }
            return this.View(model);
        }

    //    [HttpPost]
  //      public ActionResult Upload(HttpPostedFileBase file)
    //    {
   //         try
       //     {
         //       if (file.ContentLength > 0)
           //     {
              //      var fileName = Path.GetFileName(file.FileName);
             // //      var path = Path.Combine(Server.MapPath("~/App_Data/Files"), fileName);
             //       file.SaveAs(path);
             //   }
              //  this.AddNotification("Upload successful", NotificationType.INFO);
               // return RedirectToAction("Index");
         //   }
         //   catch
          //  {
              //  this.AddNotification("Upload failed", NotificationType.INFO);
             //   return RedirectToAction("Add");
          //  }
      //  }

    }
}
