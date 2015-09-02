using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizApplication.Models
{
    public class Question
    {
         public int ID {get;set;}
         public string question { get; set; }
       
        public ICollection<AnswersByQuestion> AnswersOfQuestion { get; set;}
    }
}