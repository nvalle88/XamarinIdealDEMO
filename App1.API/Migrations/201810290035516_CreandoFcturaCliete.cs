namespace App1.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreandoFcturaCliete : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        ClienteId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Apellido = c.String(),
                        Direccion = c.String(),
                    })
                .PrimaryKey(t => t.ClienteId);
            
            CreateTable(
                "dbo.Facturas",
                c => new
                    {
                        FacturaId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        Valor = c.Double(nullable: false),
                        ClienteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FacturaId)
                .ForeignKey("dbo.Clientes", t => t.ClienteId, cascadeDelete: true)
                .Index(t => t.ClienteId);
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Productoes",
                c => new
                    {
                        MyProperty = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.MyProperty);
            
            DropForeignKey("dbo.Facturas", "ClienteId", "dbo.Clientes");
            DropIndex("dbo.Facturas", new[] { "ClienteId" });
            DropTable("dbo.Facturas");
            DropTable("dbo.Clientes");
        }
    }
}
