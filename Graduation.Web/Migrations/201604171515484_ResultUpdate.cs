//namespace Graduation.Web.Migrations
//{
//    using System;
//    using System.Data.Entity.Migrations;

//    public partial class ResultUpdate : DbMigration
//    {
//        public override void Up()
//        {
//            CreateTable(
//                "dbo.TriviaAnswers",
//                c => new
//                {
//                    QuestionId = c.Int(nullable: false),
//                    OptionId = c.Int(nullable: false),
//                    Id = c.Int(nullable: false, identity: true),
//                    UserId = c.String(),
//                })
//                .PrimaryKey(t => t.Id)
//                .ForeignKey("dbo.TriviaOptions", t => new { t.QuestionId, t.OptionId }, cascadeDelete: true)
//                .Index(t => new { t.QuestionId, t.OptionId });

//            CreateTable(
//                "dbo.TriviaOptions",
//                c => new
//                {
//                    QuestionId = c.Int(nullable: false),
//                    Id = c.Int(nullable: false, identity: true),
//                    Title = c.String(nullable: false),
//                    IsCorrect = c.Boolean(nullable: false),
//                })
//                .PrimaryKey(t => new { t.QuestionId, t.Id })
//                .ForeignKey("dbo.TriviaQuestions", t => t.QuestionId, cascadeDelete: true)
//                .Index(t => t.QuestionId);

//            CreateTable(
//                "dbo.TriviaQuestions",
//                c => new
//                {
//                    Id = c.Int(nullable: false, identity: true),
//                    Title = c.String(nullable: false),
//                    Test_Id = c.Int(),
//                })
//                .PrimaryKey(t => t.Id)
//                .ForeignKey("dbo.TriviaTests", t => t.Test_Id)
//                .Index(t => t.Test_Id);

//            CreateTable(
//                "dbo.TriviaTests",
//                c => new
//                {
//                    Id = c.Int(nullable: false, identity: true),
//                    Title = c.String(nullable: false),
//                    UserId = c.Int(nullable: false)
//                })
//                .PrimaryKey(t => t.Id);
//        }

//        public override void Down()
//        {
//            DropForeignKey("dbo.TriviaAnswers", new[] { "QuestionId", "OptionId" }, "dbo.TriviaOptions");
//            DropForeignKey("dbo.TriviaOptions", "QuestionId", "dbo.TriviaQuestions");
//            DropForeignKey("dbo.TriviaQuestions", "Test_Id", "dbo.TriviaTests");
//            DropIndex("dbo.TriviaQuestions", new[] { "Test_Id" });
//            DropIndex("dbo.TriviaOptions", new[] { "QuestionId" });
//            DropIndex("dbo.TriviaAnswers", new[] { "QuestionId", "OptionId" });
//            DropTable("dbo.TriviaTests");
//            DropTable("dbo.TriviaQuestions");
//            DropTable("dbo.TriviaOptions");
//            DropTable("dbo.TriviaAnswers");
//        }
//    }
//}
