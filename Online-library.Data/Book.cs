namespace Online_library.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Book
    {
        public Book()
        {
            this.Comments = new HashSet<Comment>();
            this.IsPublic = true;
        }
        public virtual ICollection<Comment> Comments { get; set; }
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        
        public string ImageUrl { get; set; }     

        public string BookUserId { get; set; }

        public virtual ApplicationUser BookUser { get; set; }

        [Required]
        public string PublishedYear { get; set; }

        public string Description { get; set; }

        [Required]
        [MaxLength(200)]
        public string Author { get; set; }

        [Required]
        public virtual Genre Genre { get;  set; }

        public bool IsPublic { get; set; }
    }
}
