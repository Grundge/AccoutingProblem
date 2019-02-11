namespace AccoutingProblem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedDateTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transaction", "TransactionError", c => c.Int(nullable: false));
            AlterColumn("dbo.Record", "Updated", c => c.DateTime());
            AlterColumn("dbo.Record", "Deleted", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Record", "Deleted", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Record", "Updated", c => c.DateTime(nullable: false));
            DropColumn("dbo.Transaction", "TransactionError");
        }
    }
}
