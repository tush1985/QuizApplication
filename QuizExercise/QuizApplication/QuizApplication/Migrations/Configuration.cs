namespace QuizApplication.Migrations
{
    using QuizApplication.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<QuizApplication.Models.QuizDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<QuizDB>());
            
            
        }

        protected override void Seed(QuizApplication.Models.QuizDB context)
        {
            
            context.Users.AddOrUpdate(x => x.ID,
            new User { ID = 1, UserName="admin",Password="admin" },
            new User { ID = 2, UserName="client",Password="client" }    
            );
            context.SaveChanges(); 


            context.Questions.AddOrUpdate(x => x.ID,
           new Question { ID = 1, question = "what is an angular" },
           new Question() { ID = 2, question = "What is MVC stands for" },
           new Question() { ID = 3, question = "How many Types of Constructors are in C Sharp" }
           );
            context.SaveChanges();
            context.Answers.AddOrUpdate(x => x.ID,
                new AnswersByQuestion()
                {
                    ID = 1,
                    answer = "FrameWork",
                     DisplayValue=false,
                    CorrectAnswer = true,
                    QuestionID=1
                },
                new AnswersByQuestion()
                {
                    ID = 2,
                    answer = "Library",
                    DisplayValue = false,
                    CorrectAnswer = false,
                    QuestionID = 1
                },
                new AnswersByQuestion()
                {
                    ID = 3,
                    answer = "Model Controller View",
                    DisplayValue = false,
                    CorrectAnswer = false,
                    QuestionID = 2
                },
                new AnswersByQuestion()
                {
                    ID = 4,
                    answer = "Model View Controller",
                    DisplayValue = false,
                    CorrectAnswer = true,
                    QuestionID = 2
                },
                new AnswersByQuestion()
                {
                    ID = 5,
                    answer = "3",
                    DisplayValue = false,
                    CorrectAnswer = false,
                    QuestionID = 3
                },
                new AnswersByQuestion()
                {
                    ID = 6,
                    answer = "4",
                    DisplayValue = false,
                    CorrectAnswer = false,
                    QuestionID = 3
                },
                new AnswersByQuestion()
                {
                    ID = 7,
                    answer = "5",
                    DisplayValue = false,
                    CorrectAnswer = true,
                    QuestionID = 3
                }
                );
            context.SaveChanges();
                context.UsersQuestions.AddOrUpdate(x => x.ID,
                new UserQuestion { ID = 1, UserID=1, QuestionID=1,Answered=true },
                new UserQuestion { ID = 2, UserID=1, QuestionID=2,Answered=true },
                new UserQuestion { ID = 3,UserID=2, QuestionID=1,Answered=false },
                new UserQuestion { ID = 4, UserID=2, QuestionID=3,Answered=false }
                );
                context.SaveChanges();
                context.UsersAnswers.AddOrUpdate(x => x.ID,
                    new UserAnswer {  UserID = 1, UserQuestionID = 1, AnswerByQuestionID = 1, AnswerDateTime=DateTime.Now },
                    new UserAnswer {  UserID = 1, UserQuestionID = 2, AnswerByQuestionID = 1, AnswerDateTime = DateTime.Now }
                    );
                context.SaveChanges();
        }
    }
}
