namespace RuInTech_TEST.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateRawMaterialKindTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.raw_material_kinds",
                c => new
                {
                    id = c.Long(nullable: false, identity: true),
                    name = c.String(nullable: false),
                    description = c.String(),
                })
                .PrimaryKey(t => t.id, name: "pk_raw_material_kinds_id");

            AddForeignKey(
                "dbo.raw_material_assets",           // таблица
                "raw_material_kind_id",                     // поле
                "dbo.raw_material_kinds",                   // целевая таблица
                "id",                          // целевое поле
                cascadeDelete: false);         // отключаем каскадное удаление
        }
    }
}
