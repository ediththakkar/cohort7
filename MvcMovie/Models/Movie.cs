using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovie.Models
{
    public class Movie
    {
        public int  ID { get; set; }

        [StringLength(60, MinimumLength =1)]
        [Required]

        public string Title { get; set; }

        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Text, ErrorMessage = "Please enter a valid date")]
        [Display(Name ="Release Date")]
        public DateTime ReleaseDate { get; set; }


        [StringLength(30)]
        [Required]
        [RegularExpression(@"[A-Z]+[a-zA-z""'\s-]*$", ErrorMessage = "Genre must start with a capital letter and only contain letters, single quotes and dashes")]
        public string Genre { get; set; }

        [Column(TypeName ="decimal(18,2)")]
        [DataType(DataType.Currency)]
        [Range(1,100, ErrorMessage ="Please enter a valid price between $1 and !00")]
        [Required(ErrorMessage = "Please enter a valid price")]
        public decimal Price { get; set; }

        [StringLength(5)]
        [Required]
        [RegularExpression(@"[A-Z]+[a-zA-z""'\s-]*$")]
        public string Rating { get; set; }


    }
}
