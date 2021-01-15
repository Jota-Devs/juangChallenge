using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TheSchool.Models
{
    public class QuestionAndAnswerModel
    {
        [Required]
        [Display(Name ="Enter a question here..")]
        public string Question { get; set; }
        
        [Required]
        [Display(Name = "Enter an answer here..")]
        public string Answer { get; set; }

        [Required]
        [Display(Name = "Enter keywords separated by commas...")]
        public string Tags { get; set; }
    }

    public class QuestionAndAnswerItemModel : QuestionAndAnswerModel
    {
        public int Id { get; set; }
        public string LastUpdateOn { get; set; }
    }

    public class QuestionAndAnswerEditModel : QuestionAndAnswerModel
    {
        public int Id { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }
    }
}