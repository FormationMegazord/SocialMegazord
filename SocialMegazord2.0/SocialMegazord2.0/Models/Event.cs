using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SocialMegazord2._0.Models
{
    public class Event
    {
        
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public string AuthorEmail { get; set; }

        [Required]
        [StringLength(20)]
        public string Type { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode =true,DataFormatString = "{hh-mm}")]
        public DateTime Time { get; set; }

        [Required]
        [StringLength(20)]
        public string Place { get; set; }

        [StringLength(250)]
        public string AdditionalContent { get; set; }

        public bool IsAuthora(string name)
        {
            return this.Author.UserName.Equals(name);
        }
        
        [ForeignKey("Author")]
        public string AuthorId { get; set; }
        
        public virtual ApplicationUser Author { get; set; }
    }
}