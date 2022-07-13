namespace First_Task.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FileLines",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Color = c.String(nullable: false),
                    Number = c.Int(nullable: false),
                    Label = c.String(nullable: false),
                    TextFileId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.TextFiles",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    CreationDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropTable("dbo.TextFiles");
            DropTable("dbo.FileLines");
        }
    }
}
