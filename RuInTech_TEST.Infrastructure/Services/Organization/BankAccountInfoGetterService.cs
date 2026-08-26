using RuInTech_TEST.Contract.Interfaces.Organization;
using RuInTech_TEST.Database;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace RuInTech_TEST.Infrastructure.Services.Organization
{
    /// <summary>
    /// Реализация контракта <see cref="IBankAccountInfoGetterService"/>
    /// </summary>
    internal class BankAccountInfoGetterService : IBankAccountInfoGetterService
    {
        private readonly IDbContextFactory<AssetContext> _dbContextFactory;

        public BankAccountInfoGetterService(IDbContextFactory<AssetContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        /// <inheritdoc/>
        public async Task<long?> GetPersonalAccountId(string personalAccount, long bankId)
        {
            using (var context = _dbContextFactory.Create())
            {
                var bankQuery = from b in context.BankAccounts

                                where b.PersonalAccount == personalAccount 
                                    && b.BankId == bankId

                                select (long?)b.BankAccountId;

                return await bankQuery.FirstOrDefaultAsync();
            }
        }

        /// <inheritdoc/>
        public async Task<string> GetPersonalAccount(long bankAccountId)
        {
            using (var context = _dbContextFactory.Create())
            {
                var bankQuery = from b in context.BankAccounts

                                where b.BankAccountId == bankAccountId

                                select b.PersonalAccount;

                return await bankQuery.FirstOrDefaultAsync();
            }
        }
    }
}
