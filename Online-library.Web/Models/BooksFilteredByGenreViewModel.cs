namespace Online_library.Web.Models
{
    using System.Collections.Generic;

    public class BooksFilteredByGenreViewModel
    {
        public IEnumerable<BookViewModel>BooksByGenres { get; set; }

        //public virtual PagedList.IPagedList<BookViewModel> Steps { get; set; }

    }
}