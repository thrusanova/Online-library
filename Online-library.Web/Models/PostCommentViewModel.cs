namespace Online_library.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class PostCommentViewModel
    {
        [Required]
        [ShouldNotContainEmail]
        public string Comment { get; set; }

        [Required]
        public int BookId { get; set; }
    }
}