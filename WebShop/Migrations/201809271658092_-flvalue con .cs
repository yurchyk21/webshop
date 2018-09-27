namespace WebShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class flvaluecon : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tblFilterCategories", "FilterValueId", "dbo.tblFilterValue");
            DropIndex("dbo.tblFilterCategories", new[] { "FilterValueId" });
            DropPrimaryKey("dbo.tblFilterCategories");
            AddPrimaryKey("dbo.tblFilterCategories", new[] { "FilterNameId", "CategoryId" });
            DropColumn("dbo.tblFilterCategories", "FilterValueId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblFilterCategories", "FilterValueId", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.tblFilterCategories");
            AddPrimaryKey("dbo.tblFilterCategories", new[] { "FilterNameId", "FilterValueId", "CategoryId" });
            CreateIndex("dbo.tblFilterCategories", "FilterValueId");
            AddForeignKey("dbo.tblFilterCategories", "FilterValueId", "dbo.tblFilterValue", "Id", cascadeDelete: true);
        }
    }
}
