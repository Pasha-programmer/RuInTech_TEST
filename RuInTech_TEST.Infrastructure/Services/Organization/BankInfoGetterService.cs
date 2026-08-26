using RuInTech_TEST.Contract.Interfaces.Organization;
using RuInTech_TEST.Contract.Models.Organization;
using RuInTech_TEST.Database;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace RuInTech_TEST.Infrastructure.Services.Organization
{
    /// <summary>
    /// Реализация контракта <see cref="IBankInfoGetterService"/>
    /// </summary>
    internal class BankInfoGetterService : IBankInfoGetterService
    {
        private readonly IDbContextFactory<AssetContext> _dbContextFactory;

        public BankInfoGetterService(IDbContextFactory<AssetContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        /// <inheritdoc/>
        public async Task<Bank> GetBankFullInfo(long bankId)
        {
            using (var context = _dbContextFactory.Create())
            {
                var bankQuery = from b in context.Banks

                                where b.Id == bankId

                                select new Bank
                                {
                                    Id = b.Id,
                                    Name = b.Name,
                                };

                return await bankQuery.FirstOrDefaultAsync();
            }
        }
    }
}
