namespace FptBookNew1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.accounts",
                c => new
                    {
                        username = c.String(nullable: false, maxLength: 50),
                        fullname = c.String(nullable: false, maxLength: 50),
                        password = c.String(maxLength: 100),
                        email = c.String(nullable: false),
                        phone = c.String(nullable: false, maxLength: 13),
                        address = c.String(nullable: false, maxLength: 200),
                        state = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.username);
            
            CreateTable(
                "dbo.feedbacks",
                c => new
                    {
                        username = c.String(nullable: false, maxLength: 50),
                        contentFeedback = c.String(nullable: false, maxLength: 400),
                    })
                .PrimaryKey(t => new { t.username, t.contentFeedback })
                .ForeignKey("dbo.accounts", t => t.username, cascadeDelete: true)
                .Index(t => t.username);
            
            CreateTable(
                "dbo.orders",
                c => new
                    {
                        orderID = c.Int(nullable: false, identity: true),
                        username = c.String(nullable: false, maxLength: 50),
                        phone = c.String(nullable: false, maxLength: 13),
                        address = c.String(nullable: false, maxLength: 100),
                        orderDate = c.DateTime(nullable: false),
                        totalPrice = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.orderID)
                .ForeignKey("dbo.accounts", t => t.username, cascadeDelete: true)
                .Index(t => t.username);
            
            CreateTable(
                "dbo.orderDetails",
                c => new
                    {
                        orderID = c.Int(nullable: false),
                        bookID = c.String(nullable: false, maxLength: 10),
                        quantity = c.Int(nullable: false),
                        amountPrice = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.orderID, t.bookID })
                .ForeignKey("dbo.books", t => t.bookID, cascadeDelete: true)
                .ForeignKey("dbo.orders", t => t.orderID, cascadeDelete: true)
                .Index(t => t.orderID)
                .Index(t => t.bookID);
            
            CreateTable(
                "dbo.books",
                c => new
                    {
                        bookID = c.String(nullable: false, maxLength: 10),
                        bookName = c.String(nullable: false, maxLength: 100),
                        categoryID = c.String(nullable: false, maxLength: 10),
                        authorID = c.String(nullable: false, maxLength: 10),
                        quantity = c.Int(nullable: false),
                        price = c.Int(nullable: false),
                        image = c.String(),
                        shortDesc = c.String(nullable: false, maxLength: 200),
                        detailDesc = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.bookID)
                .ForeignKey("dbo.authors", t => t.authorID, cascadeDelete: true)
                .ForeignKey("dbo.categories", t => t.categoryID, cascadeDelete: true)
                .Index(t => t.categoryID)
                .Index(t => t.authorID);
            
            CreateTable(
                "dbo.authors",
                c => new
                    {
                        authorID = c.String(nullable: false, maxLength: 10),
                        authorName = c.String(nullable: false, maxLength: 50),
                        description = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.authorID);
            
            CreateTable(
                "dbo.categories",
                c => new
                    {
                        categoryID = c.String(nullable: false, maxLength: 10),
                        categoryName = c.String(nullable: false, maxLength: 50),
                        description = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.categoryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.orderDetails", "orderID", "dbo.orders");
            DropForeignKey("dbo.orderDetails", "bookID", "dbo.books");
            DropForeignKey("dbo.books", "categoryID", "dbo.categories");
            DropForeignKey("dbo.books", "authorID", "dbo.authors");
            DropForeignKey("dbo.orders", "username", "dbo.accounts");
            DropForeignKey("dbo.feedbacks", "username", "dbo.accounts");
            DropIndex("dbo.books", new[] { "authorID" });
            DropIndex("dbo.books", new[] { "categoryID" });
            DropIndex("dbo.orderDetails", new[] { "bookID" });
            DropIndex("dbo.orderDetails", new[] { "orderID" });
            DropIndex("dbo.orders", new[] { "username" });
            DropIndex("dbo.feedbacks", new[] { "username" });
            DropTable("dbo.categories");
            DropTable("dbo.authors");
            DropTable("dbo.books");
            DropTable("dbo.orderDetails");
            DropTable("dbo.orders");
            DropTable("dbo.feedbacks");
            DropTable("dbo.accounts");
        }
    }
}
