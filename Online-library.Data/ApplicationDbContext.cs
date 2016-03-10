namespace Online_library.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;
    using System.Data.Entity.Validation;
    using System.Linq;
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public IDbSet<Book> Books { get; set; }

        public IDbSet<Comment>Comments { get; set; }

        public IDbSet<Genre> Genres { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
       

    }
}