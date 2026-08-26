using RuInTech_TEST.Contract.Models.RawMaterial;
using RuInTech_TEST.Database;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace RuInTech_TEST.Contract.Interfaces.RawMaterialKinds
{
    /// <summary>
    /// Реализация контракта <see cref="IRawMaterialKindGetterService"/>.
    /// </summary>
    public class RawMaterialKindGetterService : IRawMaterialKindGetterService
    {
        private readonly IDbContextFactory<AssetContext> _dbContextFactory;

        public RawMaterialKindGetterService(IDbContextFactory<AssetContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyCollection<RawMaterialKind>> GetRawMaterialKinds()
        {
            using (var context = _dbContextFactory.Create())
            {
                var bankQuery = from rmk in context.RawMaterialKinds

                                select new RawMaterialKind
                                {
                                    Id = rmk.Id,
                                    Name = rmk.Name,
                                    Description = rmk.Description,
                                };

                return await bankQuery.ToArrayAsync();
            }
        }
    }
}
