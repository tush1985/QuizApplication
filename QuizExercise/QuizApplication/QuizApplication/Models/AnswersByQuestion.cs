using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuizApplication.Models
{
    public class AnswersByQuestion
    {
        public int ID { get; set; }
        public string answer { get; set; }
        public bool CorrectAnswer { get; set; }
        public bool DisplayValue { get; set; }
        public int QuestionID { get; set; }
    }
}
