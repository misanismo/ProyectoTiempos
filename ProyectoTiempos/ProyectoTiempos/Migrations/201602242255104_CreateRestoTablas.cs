namespace ProyectoTiempos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateRestoTablas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Casas",
                c => new
                    {
                        IdCasa = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        MontoInicial = c.Double(nullable: false),
                        MontoAcumulado = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.IdCasa);
            
            CreateTable(
                "dbo.NumeroGanadors",
                c => new
                    {
                        IdNumeroGanador = c.Int(nullable: false, identity: true),
                        IdSorteo = c.Int(nullable: false),
                        IdNumero = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdNumeroGanador)
                .ForeignKey("dbo.Numeroes", t => t.IdNumero, cascadeDelete: true)
                .ForeignKey("dbo.Sorteos", t => t.IdSorteo, cascadeDelete: true)
                .Index(t => t.IdSorteo)
                .Index(t => t.IdNumero);
            
            CreateTable(
                "dbo.Premios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Multiveces = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NumeroGanadors", "IdSorteo", "dbo.Sorteos");
            DropForeignKey("dbo.NumeroGanadors", "IdNumero", "dbo.Numeroes");
            DropIndex("dbo.NumeroGanadors", new[] { "IdNumero" });
            DropIndex("dbo.NumeroGanadors", new[] { "IdSorteo" });
            DropTable("dbo.Premios");
            DropTable("dbo.NumeroGanadors");
            DropTable("dbo.Casas");
        }
    }
}
