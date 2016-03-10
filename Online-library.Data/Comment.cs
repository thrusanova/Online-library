namespace Online_library.Data
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class Comment
    {
        public Comment()
        {
            this.Date = DateTime.Now;
        }
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public int BookId { get; set; }

        public DateTime Date { get; set; }

        public string BookUserId { get; set; }

        public virtual ApplicationUser BookUser { get; set; }

       // [Required]
        public virtual Book Book { get; set; }

    }
}