using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizApplication.Models
{
    public class UserQuestion
    {
        public int ID { get; set; }
        public bool Answered { get; set; }
        public int UserID { get; set; }
        public int QuestionID { get; set; }
        
        
    }
}