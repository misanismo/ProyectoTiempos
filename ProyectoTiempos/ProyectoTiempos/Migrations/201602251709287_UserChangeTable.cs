namespace ProyectoTiempos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserChangeTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuarios", "TipoUsuario_Value", c => c.Int(nullable: false));
            AddColumn("dbo.Usuarios", "TipoUsuario_EnumValue", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuarios", "TipoUsuario_EnumValue");
            DropColumn("dbo.Usuarios", "TipoUsuario_Value");
        }
    }
}
