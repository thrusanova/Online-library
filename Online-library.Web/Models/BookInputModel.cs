
namespace Online_library.Web.Models
{
    using Data;
    using System.ComponentModel.DataAnnotations;

    public class BookInputModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Book title is required.")]
        [StringLength(200,ErrorMessage ="The {0} must be between {2} and {1} characters long.")]
        [Display(Name ="Title *")]
        public string Title { get; set; }

        [Required]
        [Display(Name ="Author *")]
        public string Author { get; set; }

        [Required]
        [Display(Name = "Genre *")]
        public virtual Genre Genre { get; set; }

        
        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }

        [Display(Name = "Published Year")]
        public string PublishedYear { get; set; }
        

        public static BookInputModel CreateFromBook(Book b)
        {
            return new BookInputModel()
            {
                Id=b.Id,
                Title = b.Title,
                ImageUrl = b.ImageUrl,
               PublishedYear=b.PublishedYear,
               Genre=b.Genre,
               Author=b.Author,
            };
        }
    }
}