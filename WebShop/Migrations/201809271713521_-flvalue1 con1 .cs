namespace WebShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class flvalue1con1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tblfilters", "FilterNameId", "dbo.tblFilterName");
            DropForeignKey("dbo.tblfilters", "FilterValueId", "dbo.tblFilterValue");
            DropIndex("dbo.tblfilters", new[] { "FilterNameId" });
            DropIndex("dbo.tblfilters", new[] { "FilterValueId" });
            DropPrimaryKey("dbo.tblFilterNameGroups");
            DropPrimaryKey("dbo.tblfilters");
            AddColumn("dbo.tblFilterNameGroups", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.tblfilters", "FilterNameGroupId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.tblFilterNameGroups", "Id");
            AddPrimaryKey("dbo.tblfilters", new[] { "FilterNameGroupId", "ProductId" });
            CreateIndex("dbo.tblfilters", "FilterNameGroupId");
            AddForeignKey("dbo.tblfilters", "FilterNameGroupId", "dbo.tblFilterNameGroups", "Id", cascadeDelete: true);
            DropColumn("dbo.tblfilters", "FilterNameId");
            DropColumn("dbo.tblfilters", "FilterValueId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblfilters", "FilterValueId", c => c.Int(nullable: false));
            AddColumn("dbo.tblfilters", "FilterNameId", c => c.Int(nullable: false));
            DropForeignKey("dbo.tblfilters", "FilterNameGroupId", "dbo.tblFilterNameGroups");
            DropIndex("dbo.tblfilters", new[] { "FilterNameGroupId" });
            DropPrimaryKey("dbo.tblfilters");
            DropPrimaryKey("dbo.tblFilterNameGroups");
            DropColumn("dbo.tblfilters", "FilterNameGroupId");
            DropColumn("dbo.tblFilterNameGroups", "Id");
            AddPrimaryKey("dbo.tblfilters", new[] { "FilterNameId", "FilterValueId", "ProductId" });
            AddPrimaryKey("dbo.tblFilterNameGroups", new[] { "FilterNameId", "FilterValueId" });
            CreateIndex("dbo.tblfilters", "FilterValueId");
            CreateIndex("dbo.tblfilters", "FilterNameId");
            AddForeignKey("dbo.tblfilters", "FilterValueId", "dbo.tblFilterValue", "Id", cascadeDelete: true);
            AddForeignKey("dbo.tblfilters", "FilterNameId", "dbo.tblFilterName", "Id", cascadeDelete: true);
        }
    }
}
