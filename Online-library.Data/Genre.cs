namespace Online_library.Data
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        public Genre()
        {
            this.Books = new HashSet<Book>();
        }
    }
}
