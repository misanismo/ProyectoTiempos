namespace ProyectoTiempos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDetalleApuestaTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DetalleApuestas",
                c => new
                    {
                        IdDetalleApuesta = c.Int(nullable: false, identity: true),
                        IdApuesta = c.Int(nullable: false),
                        IdNumeros = c.Int(nullable: false),
                        Monto = c.Int(nullable: false),
                        Numero_IdNumero = c.Int(),
                    })
                .PrimaryKey(t => t.IdDetalleApuesta)
                .ForeignKey("dbo.Apuestas", t => t.IdApuesta, cascadeDelete: true)
                .ForeignKey("dbo.Numeroes", t => t.Numero_IdNumero)
                .Index(t => t.IdApuesta)
                .Index(t => t.Numero_IdNumero);
            
            AddColumn("dbo.NumeroGanadors", "IdPremio", c => c.Int(nullable: false));
            AddColumn("dbo.NumeroGanadors", "Premio_Id", c => c.Int());
            CreateIndex("dbo.NumeroGanadors", "Premio_Id");
            AddForeignKey("dbo.NumeroGanadors", "Premio_Id", "dbo.Premios", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NumeroGanadors", "Premio_Id", "dbo.Premios");
            DropForeignKey("dbo.DetalleApuestas", "Numero_IdNumero", "dbo.Numeroes");
            DropForeignKey("dbo.DetalleApuestas", "IdApuesta", "dbo.Apuestas");
            DropIndex("dbo.NumeroGanadors", new[] { "Premio_Id" });
            DropIndex("dbo.DetalleApuestas", new[] { "Numero_IdNumero" });
            DropIndex("dbo.DetalleApuestas", new[] { "IdApuesta" });
            DropColumn("dbo.NumeroGanadors", "Premio_Id");
            DropColumn("dbo.NumeroGanadors", "IdPremio");
            DropTable("dbo.DetalleApuestas");
        }
    }
}
