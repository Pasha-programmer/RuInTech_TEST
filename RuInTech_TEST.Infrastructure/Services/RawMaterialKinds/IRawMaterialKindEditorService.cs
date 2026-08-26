using RuInTech_TEST.Contract.Models.RawMaterial;
using RuInTech_TEST.Database;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace RuInTech_TEST.Contract.Interfaces.RawMaterialKinds
{
    /// <summary>
    /// Реализация контракта <see cref="IRawMaterialKindEditorService"/>.
    /// </summary>
    public class RawMaterialKindEditorService : IRawMaterialKindEditorService
    {
        private readonly IDbContextFactory<AssetContext> _dbContextFactory;

        public RawMaterialKindEditorService(IDbContextFactory<AssetContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        /// <inheritdoc/>
        public async Task<long?> AddRawMaterialKind(RawMaterialKind rawMaterialKind)
        {
            var entity = new Database.Entities.RawMaterial.RawMaterialKind
            {
                Name = rawMaterialKind.Name,
                Description = rawMaterialKind.Description,
            };

            using (var context = _dbContextFactory.Create())
            {
                context.RawMaterialKinds.Add(entity);
                if (await context.SaveChangesAsync() == 0)
                {
                    return null;
                }
                return entity.Id;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteRawMaterialKind(long rawMaterialKindId)
        {
            var entity = new Database.Entities.RawMaterial.RawMaterialKind
            {
                Id = rawMaterialKindId,
            };

            using (var context = _dbContextFactory.Create())
            {
                context.RawMaterialKinds.Attach(entity);
                context.Entry(entity).State = EntityState.Deleted;

                return await context.SaveChangesAsync() > 0;
            }
        }
    }
}
