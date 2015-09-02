using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuizApplication.Models
{
    public class UserAnswer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]        
        public int ID { get; set; }
        public int UserID { get; set; }
        public int UserQuestionID { get; set; }
        public int AnswerByQuestionID { get; set; }
        public DateTime AnswerDateTime { get; set; }

        
    }
}