namespace AccoutingProblem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Record",
                c => new
                    {
                        RecordId = c.Int(nullable: false, identity: true),
                        RecordUrl = c.String(),
                        RecordName = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                        Deleted = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RecordId);
            
            CreateTable(
                "dbo.Transaction",
                c => new
                    {
                        TransactionId = c.Int(nullable: false, identity: true),
                        Account = c.String(),
                        Description = c.String(),
                        CurrencyCode = c.String(),
                        Amount = c.Double(nullable: false),
                        IsValid = c.Boolean(nullable: false),
                        Record_RecordId = c.Int(),
                    })
                .PrimaryKey(t => t.TransactionId)
                .ForeignKey("dbo.Record", t => t.Record_RecordId)
                .Index(t => t.Record_RecordId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transaction", "Record_RecordId", "dbo.Record");
            DropIndex("dbo.Transaction", new[] { "Record_RecordId" });
            DropTable("dbo.Transaction");
            DropTable("dbo.Record");
        }
    }
}
