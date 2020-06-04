namespace Movies.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserProfileTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.favourites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        userId = c.Int(nullable: false),
                        movieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Follows",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PublisherId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Like_Dislike",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        movieId = c.Int(nullable: false),
                        userId = c.Int(nullable: false),
                        isLike = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Like_Dislike");
            DropTable("dbo.Follows");
            DropTable("dbo.favourites");
        }
    }
}
