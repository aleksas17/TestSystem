namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answer",
                c => new
                    {
                        AnswerId = c.Int(nullable: false, identity: true),
                        QuestionId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 140),
                        IsCorrect = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AnswerId)
                .ForeignKey("dbo.Question", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.Question",
                c => new
                    {
                        QuestionId = c.Int(nullable: false, identity: true),
                        TestId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.QuestionId)
                .ForeignKey("dbo.Test", t => t.TestId, cascadeDelete: true)
                .Index(t => t.TestId);
            
            CreateTable(
                "dbo.Test",
                c => new
                    {
                        TestId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 25),
                        Language = c.String(nullable: false),
                        Duration = c.Int(),
                        Status = c.String(nullable: false),
                        TestEnd = c.DateTime(),
                    })
                .PrimaryKey(t => t.TestId);
            
            CreateTable(
                "dbo.UserTests",
                c => new
                    {
                        UserTestId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        TestId = c.Int(nullable: false),
                        Status = c.String(),
                        TestStart = c.DateTime(),
                        Time = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.UserTestId)
                .ForeignKey("dbo.Test", t => t.TestId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.TestId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Role = c.String(maxLength: 140),
                        Username = c.String(nullable: false, maxLength: 25),
                        Password = c.String(nullable: false, maxLength: 25),
                        Position = c.String(maxLength: 140),
                        Group = c.String(maxLength: 140),
                        FirstName = c.String(maxLength: 140),
                        Lastname = c.String(maxLength: 140),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.UserAnswers",
                c => new
                    {
                        UserAnswerId = c.Int(nullable: false, identity: true),
                        UserTestId = c.Int(nullable: false),
                        QuestionId = c.Int(),
                        AnswerId = c.Int(),
                    })
                .PrimaryKey(t => t.UserAnswerId)
                .ForeignKey("dbo.Answer", t => t.AnswerId)
                .ForeignKey("dbo.Question", t => t.QuestionId)
                .ForeignKey("dbo.UserTests", t => t.UserTestId, cascadeDelete: true)
                .Index(t => t.UserTestId)
                .Index(t => t.QuestionId)
                .Index(t => t.AnswerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserAnswers", "UserTestId", "dbo.UserTests");
            DropForeignKey("dbo.UserAnswers", "QuestionId", "dbo.Question");
            DropForeignKey("dbo.UserAnswers", "AnswerId", "dbo.Answer");
            DropForeignKey("dbo.UserTests", "UserId", "dbo.User");
            DropForeignKey("dbo.UserTests", "TestId", "dbo.Test");
            DropForeignKey("dbo.Question", "TestId", "dbo.Test");
            DropForeignKey("dbo.Answer", "QuestionId", "dbo.Question");
            DropIndex("dbo.UserAnswers", new[] { "AnswerId" });
            DropIndex("dbo.UserAnswers", new[] { "QuestionId" });
            DropIndex("dbo.UserAnswers", new[] { "UserTestId" });
            DropIndex("dbo.UserTests", new[] { "TestId" });
            DropIndex("dbo.UserTests", new[] { "UserId" });
            DropIndex("dbo.Question", new[] { "TestId" });
            DropIndex("dbo.Answer", new[] { "QuestionId" });
            DropTable("dbo.UserAnswers");
            DropTable("dbo.User");
            DropTable("dbo.UserTests");
            DropTable("dbo.Test");
            DropTable("dbo.Question");
            DropTable("dbo.Answer");
        }
    }
}
