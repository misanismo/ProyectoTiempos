namespace ProyectoTiempos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDatabase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sorteos", "HoraInicio", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.Sorteos", "FechaInicio", c => c.DateTime(nullable: false));
            AddColumn("dbo.Sorteos", "HoraFinal", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.Sorteos", "FechaIFinal", c => c.DateTime(nullable: false));
            DropColumn("dbo.Sorteos", "Hora");
            DropColumn("dbo.Sorteos", "Fecha");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sorteos", "Fecha", c => c.DateTime(nullable: false));
            AddColumn("dbo.Sorteos", "Hora", c => c.DateTime(nullable: false));
            DropColumn("dbo.Sorteos", "FechaIFinal");
            DropColumn("dbo.Sorteos", "HoraFinal");
            DropColumn("dbo.Sorteos", "FechaInicio");
            DropColumn("dbo.Sorteos", "HoraInicio");
        }
    }
}
