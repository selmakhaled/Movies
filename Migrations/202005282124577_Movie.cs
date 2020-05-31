namespace Movies.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Movie : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TypeOfMovies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        type_name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TypeOfMovies");
        }
    }
}
