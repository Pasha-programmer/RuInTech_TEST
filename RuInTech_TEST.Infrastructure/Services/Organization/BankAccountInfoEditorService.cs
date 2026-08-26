using RuInTech_TEST.Contract.Interfaces.Organization;
using RuInTech_TEST.Contract.Models.Organization;
using RuInTech_TEST.Database;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace RuInTech_TEST.Infrastructure.Services.Organization
{
    /// <summary>
    /// Реализация контракта <see cref="IBankAccountInfoEditorService"/>
    /// </summary>
    internal class BankAccountInfoEditorService : IBankAccountInfoEditorService
    {
        private readonly IDbContextFactory<AssetContext> _dbContextFactory;

        public BankAccountInfoEditorService(IDbContextFactory<AssetContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        /// <inheritdoc/>
        public async Task<long?> AddBankAccount(BankAccount bankAccount)
        {
            var entity = new Database.Entities.Organization.BankAccount
            {
                BankId = bankAccount.Bank.Id.Value,
                PersonalAccount = bankAccount.PersonalAccount,
            };

            using (var context = _dbContextFactory.Create())
            {
                context.BankAccounts.Add(entity);
                if (await context.SaveChangesAsync() == 0)
                {
                    return null;
                }
                return entity.BankAccountId;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteBankAccount(long bankAccountId)
        {
            var entity = new Database.Entities.Organization.BankAccount
            {
                BankAccountId = bankAccountId,
            };

            using (var context = _dbContextFactory.Create())
            {
                context.Entry(entity).State = EntityState.Deleted;

                return await context.SaveChangesAsync() > 0;
            }
        }
    }
}
