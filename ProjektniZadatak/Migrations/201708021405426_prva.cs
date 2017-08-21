namespace ProjektniZadatak.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class prva : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Ime", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "Prezime", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "Pol", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "Fotografija", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Fotografija");
            DropColumn("dbo.AspNetUsers", "Pol");
            DropColumn("dbo.AspNetUsers", "Prezime");
            DropColumn("dbo.AspNetUsers", "Ime");
        }
    }
}
