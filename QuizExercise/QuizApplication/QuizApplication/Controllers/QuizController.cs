using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Http.Dependencies;
using System.Net.Http;
using System.Web.Http;
using QuizApplication.Models;
using System.Web.Http.Description;
using System.Web.Http.Cors;

namespace QuizApplication.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class QuizController : ApiController
    {
        //create an instance of database
        private QuizDB db = new QuizDB();

        //dispose the instance if no longer used
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }
            base.Dispose(disposing);
        }
        // GET api/Quiz
        //get the question for quiz
        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)//Received the userid as parameter
        {
            try
            {
                //find the user question which is not answered...
                var userquestion = this.db.UsersQuestions.FirstOrDefault(u => u.UserID == id && u.Answered == false);
                //if all the questions answered than no more question
                if (userquestion != null)
                {
                    //find the actual  question
                    var question = this.db.Questions.Find(userquestion.QuestionID);
                    //find the multiple answer for related to that question
                    var answers = from ans in this.db.Answers
                                  where ans.QuestionID == question.ID
                                  select new {ans.ID,ans.answer,ans.DisplayValue,ans.QuestionID};
                    //return question and answer seprately and errorcode :0 means success
                    return Json(new { question = question, answers = answers, ErrorCode = 0 });
                }
                else
                {
                    //no questions found associated to user , return errorcode : -1 and message to user
                    return Json(new { ErrorCode = -1, ErrorMessage = "Quiz Question has't assign,please contact to your quiz provider..." });
                }
            }
            catch (Exception ex)
            {
                //if any error occurs than return errorcode:1  and message to user
                return Json(new {ErrorCode = 1,ErrorMessage="Please contact to your quiz provider..." });
            }
        }
        //post the user answer and stored in database
        [HttpPost]
        public async Task<IHttpActionResult> Post(UserResponse model)
        {
            try
            {
                //find the question that we have sent to user
                var question = this.db.UsersQuestions.FirstOrDefault(u => u.UserID == model.userID && u.QuestionID == model.ID);
                //marked the question as answered in database so we will not ask same question
                question.Answered = true;
                //save the changes to table
                this.db.SaveChanges();
                //for each question there may be a multiple anser
                foreach (AnswersByQuestion ans in model.AnswersOfQuestion)
                {
                    //id user has selected an answer than we will stored in database
                    if (ans.DisplayValue == true)
                    {
                        //stored the answer in UserAnswer table with userid,questionid,answerdatetime,answerid(each answer has uniqueid)
                        UserAnswer UA = new UserAnswer();                        
                        UA.UserID = model.userID;
                        UA.UserQuestionID = model.ID;
                        UA.AnswerDateTime = DateTime.Now;
                        UA.AnswerByQuestionID = ans.ID;
                        this.db.UsersAnswers.Add(UA);
                        this.db.SaveChanges();
                    }
                }
                //see if any more question needs to send to user
                var userquestion = this.db.UsersQuestions.FirstOrDefault(u => u.UserID == model.userID && u.Answered == false);
                if (userquestion != null)
                {
                    //instruct the user click on next tab errorcode:0 and message
                    return Json(new { ErrorCode = 0, ErrorMessage = "Answer saved...click on next tab" });
                }
                else
                {
                    //instruct the user that quiz finished errorcode:0 and message
                    return Json(new { ErrorCode = 0, ErrorMessage = "Answer saved...Quiz Finished..." });
                }
            }
            catch(Exception ex)
            { 
                //if any error occurs return seprate error code to debug making easier from which function error coming from
                    return Json(new { ErrorCode = 10, ErrorMessage = "Please contact to your quiz provider..." });
            }
                
        }
            
         
    }

}
