namespace ProyectoTiempos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateApuestaTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Apuestas",
                c => new
                    {
                        IdApuesta = c.Int(nullable: false, identity: true),
                        IdUsuario = c.Int(nullable: false),
                        IdNumero = c.Int(nullable: false),
                        IdSorteo = c.Int(nullable: false),
                        MontoApuesta = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.IdApuesta)
                .ForeignKey("dbo.Numeroes", t => t.IdNumero, cascadeDelete: true)
                .ForeignKey("dbo.Sorteos", t => t.IdSorteo, cascadeDelete: true)
                .ForeignKey("dbo.Usuarios", t => t.IdUsuario, cascadeDelete: true)
                .Index(t => t.IdUsuario)
                .Index(t => t.IdNumero)
                .Index(t => t.IdSorteo);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Apuestas", "IdUsuario", "dbo.Usuarios");
            DropForeignKey("dbo.Apuestas", "IdSorteo", "dbo.Sorteos");
            DropForeignKey("dbo.Apuestas", "IdNumero", "dbo.Numeroes");
            DropIndex("dbo.Apuestas", new[] { "IdSorteo" });
            DropIndex("dbo.Apuestas", new[] { "IdNumero" });
            DropIndex("dbo.Apuestas", new[] { "IdUsuario" });
            DropTable("dbo.Apuestas");
        }
    }
}
