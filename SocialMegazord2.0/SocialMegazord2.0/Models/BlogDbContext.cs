using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SocialMegazord2._0.Models
{
    

    public class BlogDbContext : IdentityDbContext<ApplicationUser>
    {
        public BlogDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public virtual IDbSet<Event> Events { get; set; }

        public virtual IDbSet<Post> Posts { get; set; } 

        public virtual IDbSet<Communities> Communities { get; set; }

        public static BlogDbContext Create()
        {
            return new BlogDbContext();
        }
    }
}