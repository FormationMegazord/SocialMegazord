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

        public Post(string authorId, string title, string content, int communityId, string email)
        {
            this.Author = Author;
            this.AuthorId = authorId;
            this.Title = title;
            this.Content = content;
            this.Communities = Communities;
            this.CommunityId = communityId;
            this.DateAdded = DateTime.Now;
            this.AuthorEmail = email;
        }

        [Key]
        public int Id { get; set; }
        
        public string AuthorEmail { get; set; }
        public DateTime DateAdded { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        public bool IsAuthor(string name)
        {
            return this.Author.UserName.Equals(name);
        }

        public string Content { get; set; }

        [ForeigntKey("Author")]
        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        

        [ForeignKey("Communities")]
        public int CommunityId { get; set; }
        public virtual Communities Communities { get; set; }
     


    }
}