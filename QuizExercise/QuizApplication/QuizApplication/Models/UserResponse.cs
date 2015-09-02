using System;
using System.Collections.Generic;
namespace QuizApplication.Models
{
    public class UserResponse
    {
         public int ID {get;set;}
         public string question { get; set; }
        public int userID {get;set;}
       
        public ICollection<AnswersByQuestion> AnswersOfQuestion { get; set;}
    }
}
