namespace ProyectoTiempos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateSorteoTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sorteos",
                c => new
                    {
                        IdSorteo = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        NumeroSorteo = c.Int(nullable: false),
                        Hora = c.DateTime(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        Estado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IdSorteo);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Sorteos");
        }
    }
}
