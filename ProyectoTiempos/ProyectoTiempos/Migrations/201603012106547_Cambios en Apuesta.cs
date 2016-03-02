namespace ProyectoTiempos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CambiosenApuesta : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Apuestas", "IdNumero", "dbo.Numeroes");
            DropIndex("dbo.Apuestas", new[] { "IdNumero" });
            DropColumn("dbo.Apuestas", "IdNumero");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Apuestas", "IdNumero", c => c.Int(nullable: false));
            CreateIndex("dbo.Apuestas", "IdNumero");
            AddForeignKey("dbo.Apuestas", "IdNumero", "dbo.Numeroes", "IdNumero", cascadeDelete: true);
        }
    }
}
