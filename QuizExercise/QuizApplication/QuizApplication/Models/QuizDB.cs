using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace QuizApplication.Models
{
    public class QuizDB : DbContext
    {
         public QuizDB() : base("QuizDB")
        {
        }
         public DbSet<User> Users { get; set; }
        public DbSet<Question>  Questions { get; set; }
        public DbSet<AnswersByQuestion> Answers { get; set; }        
        public DbSet<UserQuestion> UsersQuestions { get; set; }
        public DbSet<UserAnswer> UsersAnswers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}