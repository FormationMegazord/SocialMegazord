using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialMegazord2._0.Models
{
    public class PostViewModel
    {
        public int Id { get; set; }

        public int CommunityId { get; set; }

        public ICollection<Communities> Communities { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public string AuthorId { get; set; }
    }
}