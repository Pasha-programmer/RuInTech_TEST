namespace RuInTech_TEST.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.assets",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        asset_kind = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id, name: "pk_assets_id");
            
            CreateTable(
                "dbo.bank_accounts",
                c => new
                    {
                        bank_account_id = c.Long(nullable: false, identity: true),
                        personal_account = c.String(nullable: false),
                        bank_id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.bank_account_id, name: "pk_bank_accounts_id")
                .ForeignKey("dbo.banks", t => t.bank_id, cascadeDelete: false);

            CreateTable(
                "dbo.banks",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id, name: "pk_banks_id");
            
            CreateTable(
                "dbo.monetary_assets",
                c => new
                    {
                        id = c.Long(nullable: false),
                        cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        currency = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.assets", t => t.id)
                .Index(t => t.id);
            
            CreateTable(
                "dbo.non_monetary_assets",
                c => new
                    {
                        id = c.Long(nullable: false),
                        initial_balance_cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        initial_balance_cost_currency = c.Int(nullable: false),
                        residual_balance_cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        residual_balance_cost_currency = c.Int(nullable: false),
                        estimated_cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        estimated_cost_currency = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.assets", t => t.id)
                .Index(t => t.id);
            
            CreateTable(
                "dbo.payment_account_assets",
                c => new
                    {
                        id = c.Long(nullable: false),
                        bank_account_id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.monetary_assets", t => t.id)
                .ForeignKey("dbo.bank_accounts", t => t.bank_account_id, cascadeDelete: false)
                .Index(t => t.id);
            
            CreateTable(
                "dbo.raw_material_assets",
                c => new
                    {
                        id = c.Long(nullable: false),
                        raw_material_kind_id = c.Long(nullable: false),
                        unit_of_measure = c.Int(nullable: false),
                        quantity = c.Double(nullable: false),
                        production_date = c.DateTimeOffset(precision: 7),
                        additional_info = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.non_monetary_assets", t => t.id)
                .Index(t => t.id);
            
            CreateTable(
                "dbo.realty_assets",
                c => new
                    {
                        id = c.Long(nullable: false),
                        inventory_number = c.String(nullable: false),
                        additional_info = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.non_monetary_assets", t => t.id)
                .Index(t => t.id);
            
            CreateTable(
                "dbo.coupon_assets",
                c => new
                    {
                        id = c.Long(nullable: false),
                        type = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.monetary_assets", t => t.id)
                .Index(t => t.id);
            
        }
    }
}
