using RuInTech_TEST.Contract.Interfaces.Assets;
using RuInTech_TEST.Contract.Models;
using RuInTech_TEST.Contract.Models.Assets;
using RuInTech_TEST.Contract.Models.Assets.Monetary;
using RuInTech_TEST.Contract.Models.Assets.NonMonetary;
using RuInTech_TEST.Contract.Models.Enums;
using RuInTech_TEST.Contract.Models.FilterParameters;
using RuInTech_TEST.Contract.Models.Organization;
using RuInTech_TEST.Contract.Models.RawMaterial;
using RuInTech_TEST.Database;
using RuInTech_TEST.Infrastructure.Dtos.Assets;
using RuInTech_TEST.Infrastructure.Dtos.Assets.Monetary;
using RuInTech_TEST.Infrastructure.Dtos.Assets.NonMonetary;
using RuInTech_TEST.Infrastructure.Dtos.Organization;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace RuInTech_TEST.Infrastructure.Services.Assets
{
    /// <summary>
    /// Реализация контракта <see cref="IAssetsInfoGetter"/>
    /// </summary>
    internal class AssetsInfoGetter : IAssetsInfoGetter
    {
        private readonly IDbContextFactory<AssetContext> _dbContextFactory;

        public AssetsInfoGetter(IDbContextFactory<AssetContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyCollection<Asset>> GetAssets(AssetFilterParameters assetFilterParameters)
        {
            //TODO: оптимизировать - вызывать только те, что есть в фильтре
            var cashTask = GetCashAssets(assetFilterParameters);
            var paymentTask = GetPaymentAccountAssets(assetFilterParameters);
            var couponTask = GetСouponAssets(assetFilterParameters);
            var rawMaterialTask = GetRawMaterialAssets(assetFilterParameters);
            var realtyTask = GetRealtyAssets(assetFilterParameters);

            await Task.WhenAll(cashTask, paymentTask, couponTask, rawMaterialTask, realtyTask);

            var allAssets = new List<Asset>();
            allAssets.AddRange(cashTask.Result);
            allAssets.AddRange(paymentTask.Result);
            allAssets.AddRange(couponTask.Result);
            allAssets.AddRange(rawMaterialTask.Result);
            allAssets.AddRange(realtyTask.Result);

            return allAssets;
        }

        private async Task<IReadOnlyCollection<CashAsset>> GetCashAssets(AssetFilterParameters assetFilterParameters = null)
        {
            using (var context = _dbContextFactory.Create())
            {
                var cashQuery = from a in context.Assets
                                join ma in context.MonetaryAssets on a.Id equals ma.Id

                                where a.AssetKind == Database.Entities.Enums.AssetKind.Cash

                                select new CashAssetDto
                                {
                                    Id = a.Id,
                                    Name = a.Name,
                                    Cost = ma.Cost,
                                    Currency = (CurrencyType)ma.Currency,
                                };

                cashQuery = ApplyFilterParameters(cashQuery, assetFilterParameters);

                var data = await cashQuery.ToArrayAsync();

                return data.Select(a => new CashAsset
                {
                    Id = a.Id,
                    Name = a.Name,
                    MonetaryValue = new MonetaryValue(a.Cost, a.Currency),
                }).ToArray();
            }
        }
        
        private async Task<IReadOnlyCollection<PaymentAccount>> GetPaymentAccountAssets(AssetFilterParameters assetFilterParameters = null)
        {
            using (var context = _dbContextFactory.Create())
            {
                var paymentAccountQuery = from a in context.Assets
                                          join ma in context.MonetaryAssets on a.Id equals ma.Id
                                          join pa in context.PaymentAccount on a.Id equals pa.Id
                                          join ba in context.BankAccounts on pa.BankAccountId equals ba.BankAccountId
                                          join b in context.Banks on ba.BankId equals b.Id

                                          where a.AssetKind == Database.Entities.Enums.AssetKind.PaymentAccount

                                          select new PaymentAccountDto
                                          {
                                              Id = a.Id,
                                              Name = a.Name,
                                              AssetKind = AssetKind.PaymentAccount,
                                              Cost = ma.Cost,
                                              Currency = (CurrencyType)ma.Currency,
                                              BankAccount = new BankAccountDto
                                              {
                                                  PersonalAccount = ba.PersonalAccount,
                                                  Bank = new BankDto
                                                  {
                                                      Id = ba.BankId,
                                                      Name = b.Name,
                                                  },
                                              },
                                          };

                paymentAccountQuery = ApplyFilterParameters(paymentAccountQuery, assetFilterParameters);

                var data = await paymentAccountQuery.ToArrayAsync();

                return data.Select(a => new PaymentAccount
                {
                    Id = a.Id,
                    Name = a.Name,
                    MonetaryValue = new MonetaryValue(a.Cost, a.Currency),
                    BankAccount = new BankAccount
                    {
                        Id = a.BankAccount.Id,
                        PersonalAccount = a.BankAccount.PersonalAccount,
                        Bank = new Bank
                        {
                            Id = a.BankAccount.Bank.Id,
                            Name = a.BankAccount.Bank.Name,
                        },
                    },
                }).ToArray();
            }
        }

        private async Task<IReadOnlyCollection<Сoupon>> GetСouponAssets(AssetFilterParameters assetFilterParameters = null)
        {
            using (var context = _dbContextFactory.Create())
            {
                var couponQuery = from a in context.Assets
                                  join ma in context.MonetaryAssets on a.Id equals ma.Id
                                  join c in context.Coupons on a.Id equals c.Id

                                  where a.AssetKind == Database.Entities.Enums.AssetKind.Coupon

                                  select new СouponDto
                                  {
                                      Id = a.Id,
                                      Name = a.Name,
                                      Cost = ma.Cost,
                                      Currency = (CurrencyType)ma.Currency,
                                      AssetKind = AssetKind.Coupon,
                                      Type = c.Type,
                                  };

                couponQuery = ApplyFilterParameters(couponQuery, assetFilterParameters);

                var data = await couponQuery.ToArrayAsync();

                return data.Select(a => new Сoupon
                {
                    Id = a.Id,
                    Name = a.Name,
                    MonetaryValue = new MonetaryValue(a.Cost, a.Currency),
                    Type = a.Type,
                }).ToArray();
            }
        }

        private async Task<IReadOnlyCollection<RawMaterial>> GetRawMaterialAssets(AssetFilterParameters assetFilterParameters = null)
        {
            using (var context = _dbContextFactory.Create())
            {
                var rawMaterialQuery = from a in context.Assets
                                       join nma in context.NonMonetaryAssets on a.Id equals nma.Id
                                       join rm in context.RawMaterials on a.Id equals rm.Id
                                       join rmk in context.RawMaterialKinds on rm.RawMaterialKindId equals rmk.Id

                                       where a.AssetKind == Database.Entities.Enums.AssetKind.RawMaterial

                                       select new RawMaterialDto
                                       {
                                           Id = a.Id,
                                           Name = a.Name,
                                           RawMaterialKind = new RawMaterialKind
                                           {
                                               Id = rmk.Id,
                                               Name = rmk.Name,
                                               Description = rmk.Description,
                                           },
                                           InitialBalanceCost = nma.InitialBalanceCost,
                                           InitialBalanceCostCurrency = (CurrencyType)nma.InitialBalanceCostCurrency,
                                           ResidualBalanceCost = nma.ResidualBalanceCost,
                                           ResidualBalanceCostCurrency = (CurrencyType)nma.ResidualBalanceCostCurrency,
                                           EstimatedCost = nma.EstimatedCost,
                                           EstimatedCostCurrency = (CurrencyType)nma.EstimatedCostCurrency,
                                           Quantity = rm.Quantity,
                                           UnitOfMeasure = (UnitOfMeasure)rm.UnitOfMeasure,
                                           ProductionDate = rm.ProductionDate,
                                           AdditionalInfo = rm.AdditionalInfo,
                                           AssetKind = AssetKind.RawMaterial,
                                       };

                rawMaterialQuery = ApplyFilterParameters(rawMaterialQuery, assetFilterParameters);

                var data = await rawMaterialQuery.ToArrayAsync();

                return data.Select(a => new RawMaterial
                {
                    Id = a.Id,
                    Name = a.Name,
                    InitialBalanceCost = new MonetaryValue(a.InitialBalanceCost, a.InitialBalanceCostCurrency),
                    ResidualBalanceCost = new MonetaryValue(a.ResidualBalanceCost, a.ResidualBalanceCostCurrency),
                    EstimatedCost = new MonetaryValue(a.EstimatedCost, a.EstimatedCostCurrency),
                    Quantity = a.Quantity,
                    ProductionDate = a.ProductionDate,
                    RawMaterialKind = a.RawMaterialKind,
                    UnitOfMeasure = a.UnitOfMeasure,
                    AdditionalInfo = a.AdditionalInfo,
                }).ToArray();
            }
        }

        private async Task<IReadOnlyCollection<Realty>> GetRealtyAssets(AssetFilterParameters assetFilterParameters = null)
        {
            using (var context = _dbContextFactory.Create())
            {
                var realtyQuery = from a in context.Assets
                                  join nma in context.NonMonetaryAssets on a.Id equals nma.Id
                                  join r in context.Realty on a.Id equals r.Id

                                  where a.AssetKind == Database.Entities.Enums.AssetKind.Realty

                                  select new RealtyDto
                                  {
                                      Id = a.Id,
                                      Name = a.Name,
                                      InitialBalanceCost = nma.InitialBalanceCost,
                                      InitialBalanceCostCurrency = (CurrencyType)nma.InitialBalanceCostCurrency,
                                      ResidualBalanceCost = nma.ResidualBalanceCost,
                                      ResidualBalanceCostCurrency = (CurrencyType)nma.ResidualBalanceCostCurrency,
                                      EstimatedCost = nma.EstimatedCost,
                                      EstimatedCostCurrency = (CurrencyType)nma.EstimatedCostCurrency,
                                      InventoryNumber = r.InventoryNumber,
                                      AdditionalInfo = r.AdditionalInfo,
                                      AssetKind = AssetKind.Realty,
                                  };

                realtyQuery = ApplyFilterParameters(realtyQuery, assetFilterParameters);

                var data = await realtyQuery.ToArrayAsync();

                return data.Select(a => new Realty
                {
                    Id = a.Id,
                    Name = a.Name,
                    InitialBalanceCost = new MonetaryValue(a.InitialBalanceCost, a.InitialBalanceCostCurrency),
                    ResidualBalanceCost = new MonetaryValue(a.ResidualBalanceCost, a.ResidualBalanceCostCurrency),
                    EstimatedCost = new MonetaryValue(a.EstimatedCost, a.EstimatedCostCurrency),
                    AdditionalInfo = a.AdditionalInfo,
                    InventoryNumber = a.InventoryNumber,
                }).ToArray();
            }
        }
        
        private IQueryable<T> ApplyFilterParameters<T>(IQueryable<T> query, AssetFilterParameters assetFilterParameters)
            where T : AssetDto
        {
            using (var context = _dbContextFactory.Create())
            {
                if (assetFilterParameters == null)
                {
                    return query;
                }

                if ((assetFilterParameters.AssetIds?.Length ?? 0) > 0)
                {
                    query = query.Where(a => a.Id.HasValue && assetFilterParameters.AssetIds.Contains(a.Id.Value));
                }

                if ((assetFilterParameters.Name?.Length ?? 0) > 0)
                {
                    query = query.Where(a => a.Name.Contains(assetFilterParameters.Name));
                }

                if ((assetFilterParameters.AssetKinds?.Length ?? 0) > 0)
                {
                    query = query.Where(a => assetFilterParameters.AssetKinds.Contains(a.AssetKind));
                }

                return query;
            }
        }

        /// <inheritdoc/>
        public async Task<Asset> GetAsset(long id)
        {
            return (await GetAssets(new AssetFilterParameters
            {
                AssetIds = new[]
                {
                    id,
                }
            })).FirstOrDefault();
        }
    }
}
