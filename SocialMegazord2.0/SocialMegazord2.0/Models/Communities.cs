using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SocialMegazord2._0.Models
{
    public class Communities
    {
        private ICollection<Post> posts;

        public Communities()
        {
            this.posts = new HashSet<Post>();
        }

        [Key]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [StringLength(20)]
        public string Name { get; set; }

        private ICollection<Post> Posts;
    }
}