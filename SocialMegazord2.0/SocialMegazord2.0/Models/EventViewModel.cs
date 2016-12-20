using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialMegazord2._0.Models
{
    public class EventViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Type { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        public string AdditionalContent { get; set; }

        [Required]
        [StringLength(30)]
        public string Place { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{hh-mm}")]
        public DateTime Time { get; set; }

        public string AuthorId { get; set; }
    }
}