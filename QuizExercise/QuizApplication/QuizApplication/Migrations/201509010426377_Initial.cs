namespace QuizApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnswersByQuestion",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        answer = c.String(),
                        CorrectAnswer = c.Boolean(nullable: false),
                        DisplayValue = c.Boolean(nullable: false),
                        QuestionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Question", t => t.QuestionID, cascadeDelete: true)
                .Index(t => t.QuestionID);
            
            CreateTable(
                "dbo.Question",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        question = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserAnswer",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        UserQuestionID = c.Int(nullable: false),
                        AnswerByQuestionID = c.Int(nullable: false),
                        AnswerDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserQuestion",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Answered = c.Boolean(nullable: false),
                        UserID = c.Int(nullable: false),
                        QuestionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AnswersByQuestion", "QuestionID", "dbo.Question");
            DropIndex("dbo.AnswersByQuestion", new[] { "QuestionID" });
            DropTable("dbo.UserQuestion");
            DropTable("dbo.UserAnswer");
            DropTable("dbo.User");
            DropTable("dbo.Question");
            DropTable("dbo.AnswersByQuestion");
        }
    }
}
