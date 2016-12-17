using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SocialMegazord2._0.Models
{
    public class Post
    {
        public Post()
        {
        }

        public Post(string authorId, string title, string content, int communityId)
        {
            this.AuthorId = authorId;
            this.Title = title;
            this.Content = content;
            this.CommunityId = communityId;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Title { get; set; }
        public string Content { get; set; }

        [ForeignKey("Author")]
        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        [ForeignKey("Communities")]
        public int CommunityId { get; set; }
        public virtual Communities Communities { get; set; }

        public bool IsAuthor (string name)
        {
            return this.Author.UserName.Equals(name);
        }
    }
}