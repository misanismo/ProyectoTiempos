namespace ProyectoTiempos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateNumeroTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Numeroes",
                c => new
                    {
                        IdNumero = c.Int(nullable: false, identity: true),
                        Numeros = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdNumero);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Numeroes");
        }
    }
}
