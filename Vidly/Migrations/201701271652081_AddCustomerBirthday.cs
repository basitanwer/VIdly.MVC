namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomerBirthday : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Birthday", c => c.DateTime());
            AlterColumn("dbo.MembershipTypes", "MembershipName", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MembershipTypes", "MembershipName", c => c.String());
            DropColumn("dbo.Customers", "Birthday");
        }
    }
}
