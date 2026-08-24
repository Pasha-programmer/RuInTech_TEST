using Npgsql;
using RuInTech_TEST.Database.Entities.Assets;
using RuInTech_TEST.Database.Entities.Assets.Monetary;
using RuInTech_TEST.Database.Entities.Assets.NonMonetary;
using RuInTech_TEST.Database.Entities.Enums;
using RuInTech_TEST.Database.Entities.Organization;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace RuInTech_TEST.Database
{
    public class AssetContext : DbContext
    {
        public AssetContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        { }

        public DbSet<Asset> Assets { get; set; }

        public DbSet<MonetaryAsset> MonetaryAssets { get; set; }

        public DbSet<NonMonetaryAsset> NonMonetaryAssets { get; set; }

        public DbSet<PaymentAccount> PaymentAccount { get; set; }

        public DbSet<Сoupon> Сoupons { get; set; }

        public DbSet<RawMaterial> RawMaterials { get; set; }

        public DbSet<Realty> Realty { get; set; }

        public DbSet<Bank> Banks { get; set; }

        public DbSet<BankAccount> BankAccounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            OnEnumsCreating(modelBuilder);

            OnAssetCreating(modelBuilder);
            OnMonetaryAssetCreating(modelBuilder);
            OnNonMonetaryAssetCreating(modelBuilder);
            OnPaymentAccountCreating(modelBuilder);
            OnСouponCreating(modelBuilder);
            OnRawMaterialCreating(modelBuilder);
            OnRealtyCreating(modelBuilder);
            OnBankCreating(modelBuilder);
            OnBankAccountCreating(modelBuilder);
        }

        private void OnEnumsCreating(DbModelBuilder modelBuilder)
        {
            NpgsqlConnection.GlobalTypeMapper.MapEnum<AssetKind>("asset_kind");
            NpgsqlConnection.GlobalTypeMapper.MapEnum<CurrencyType>("currency_type");
            NpgsqlConnection.GlobalTypeMapper.MapEnum<UnitOfMeasure>("unit_of_measure");
            NpgsqlConnection.GlobalTypeMapper.MapEnum<UnitOfMeasure>("unit_of_measure");
        }

        private void OnAssetCreating(DbModelBuilder modelBuilder)
        {
            var assetEntity = modelBuilder.Entity<Asset>();
            assetEntity.ToTable("assets")
                .HasKey(e => e.Id, o => o.HasName("pk_assets_id"));

            assetEntity.Property(e => e.Id)
                .HasColumnName("id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            assetEntity.Property(e => e.Name)
                .HasColumnName("name")
                .IsRequired();

            assetEntity.Property(e => e.AssetKind)
                .HasColumnName("asset_kind")
                .IsRequired();

            // Настройка наследования (TPT - Table per Type)
            assetEntity.Map<MonetaryAsset>(m => m.ToTable("monetary_assets"));
            assetEntity.Map<NonMonetaryAsset>(m => m.ToTable("non_monetary_assets"));
        }

        private void OnMonetaryAssetCreating(DbModelBuilder modelBuilder)
        {
            var monetaryAssetEntity = modelBuilder.Entity<MonetaryAsset>();
            monetaryAssetEntity.ToTable("monetary_assets");

            monetaryAssetEntity.Property(e => e.Cost)
                .HasColumnName("cost")
                .IsRequired();

            monetaryAssetEntity.Property(e => e.Currency)
                .HasColumnName("currency")
                .IsRequired();

            // Настройка наследования (TPT - Table per Type)
            monetaryAssetEntity.Map<PaymentAccount>(m => m.ToTable("payment_account_assets"));
            monetaryAssetEntity.Map<Сoupon>(m => m.ToTable("coupon_assets"));
        }

        private void OnNonMonetaryAssetCreating(DbModelBuilder modelBuilder)
        {
            var nonMonetaryAssetEntity = modelBuilder.Entity<NonMonetaryAsset>();
            nonMonetaryAssetEntity.ToTable("non_monetary_assets");

            nonMonetaryAssetEntity.Property(e => e.InitialBalanceCost)
                .HasColumnName("initial_balance_cost")
                .IsRequired();

            nonMonetaryAssetEntity.Property(e => e.InitialBalanceCostCurrency)
                .HasColumnName("initial_balance_cost_currency")
                .IsRequired();

            nonMonetaryAssetEntity.Property(e => e.ResidualBalanceCost)
                .HasColumnName("residual_balance_cost")
                .IsRequired();

            nonMonetaryAssetEntity.Property(e => e.ResidualBalanceCostCurrency)
                .HasColumnName("residual_balance_cost_currency")
                .IsRequired();

            nonMonetaryAssetEntity.Property(e => e.EstimatedCost)
                .HasColumnName("estimated_cost")
                .IsRequired();

            nonMonetaryAssetEntity.Property(e => e.EstimatedCostCurrency)
                .HasColumnName("estimated_cost_currency")
                .IsRequired();

            // Настройка наследования (TPT - Table per Type)
            nonMonetaryAssetEntity.Map<RawMaterial>(m => m.ToTable("raw_material_assets"));
            nonMonetaryAssetEntity.Map<Realty>(m => m.ToTable("realty_assets"));
        }

        private void OnPaymentAccountCreating(DbModelBuilder modelBuilder)
        {
            var paymentAccountEntity = modelBuilder.Entity<PaymentAccount>();
            paymentAccountEntity.ToTable("payment_account_assets");

            paymentAccountEntity.Property(e => e.BankAccountId)
                .HasColumnName("bank_account_id")
                .IsRequired();
        }

        private void OnСouponCreating(DbModelBuilder modelBuilder)
        {
            var couponEntity = modelBuilder.Entity<Сoupon>();
            couponEntity.ToTable("coupon_assets");

            couponEntity.Property(e => e.Type)
                .HasColumnName("type")
                .IsRequired();
        }

        private void OnRawMaterialCreating(DbModelBuilder modelBuilder)
        {
            var rawMaterialEntity = modelBuilder.Entity<RawMaterial>();
            rawMaterialEntity.ToTable("raw_material_assets");

            rawMaterialEntity.Property(e => e.RawMaterialKindId)
                .HasColumnName("raw_material_kind_id")
                .IsRequired();

            rawMaterialEntity.Property(e => e.UnitOfMeasure)
                .HasColumnName("unit_of_measure")
                .IsRequired();

            rawMaterialEntity.Property(e => e.Quantity)
                .HasColumnName("quantity")
                .IsRequired();

            rawMaterialEntity.Property(e => e.ProductionDate)
                .HasColumnName("production_date")
                .IsOptional();

            rawMaterialEntity.Property(e => e.AdditionalInfo)
                .HasColumnName("additional_info")
                .IsOptional();
        }

        private void OnRealtyCreating(DbModelBuilder modelBuilder)
        {
            var realtyEntity = modelBuilder.Entity<Realty>();
            realtyEntity.ToTable("realty_assets");

            realtyEntity.Property(e => e.InventoryNumber)
                .HasColumnName("inventory_number")
                .IsRequired();

            realtyEntity.Property(e => e.AdditionalInfo)
                .HasColumnName("additional_info")
                .IsOptional();
        }

        private void OnBankCreating(DbModelBuilder modelBuilder)
        {
            var bankEntity = modelBuilder.Entity<Bank>();
            bankEntity.ToTable("banks")
                .HasKey(e => e.Id, o => o.HasName("pk_banks_id"));

            bankEntity.Property(e => e.Id)
                .HasColumnName("id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            bankEntity.Property(e => e.Name)
                .HasColumnName("name")
                .IsRequired();
        }

        private void OnBankAccountCreating(DbModelBuilder modelBuilder)
        {
            var bankAccountEntity = modelBuilder.Entity<BankAccount>();
            bankAccountEntity.ToTable("bank_accounts")
                .HasKey(e => e.BankAccountId, o => o.HasName("pk_bank_accounts_id"));

            bankAccountEntity.Property(e => e.BankAccountId)
                .HasColumnName("bank_account_id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            bankAccountEntity.Property(e => e.PersonalAccount)
                .HasColumnName("personal_account")
                .IsRequired();

            bankAccountEntity.Property(e => e.BankId)
                .HasColumnName("bank_id")
                .IsRequired();
        }
    }
}
