using RuInTech_TEST.Contract.Interfaces.Organization;
using RuInTech_TEST.Contract.Models.Organization;
using RuInTech_TEST.Database;
using RuInTech_TEST.Infrastructure.Dtos.Organization;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using System.Linq;

namespace RuInTech_TEST.Infrastructure.Services.Organization
{
    /// <summary>
    /// Реализация контракта <see cref="IBankInfoEditorService"/>
    /// </summary>
    internal class BankInfoEditorService : IBankInfoEditorService
    {
        private readonly IDbContextFactory<AssetContext> _dbContextFactory;

        public BankInfoEditorService(IDbContextFactory<AssetContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        /// <inheritdoc/>
        public async Task<long?> AddBank(Bank bank)
        {
            var entity = new Database.Entities.Organization.Bank
            {
                Name = bank.Name,
            };

            using (var context = _dbContextFactory.Create())
            {
                context.Banks.Add(entity);
                if (await context.SaveChangesAsync() == 0)
                {
                    return null;
                }
                return entity.Id;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteBank(long bankId)
        {
            var entity = new Database.Entities.Organization.Bank
            {
                Id = bankId,
            };

            using (var context = _dbContextFactory.Create())
            {
                context.Entry(entity).State = EntityState.Deleted;

                return await context.SaveChangesAsync() > 0;
            }
        }
    }
}
