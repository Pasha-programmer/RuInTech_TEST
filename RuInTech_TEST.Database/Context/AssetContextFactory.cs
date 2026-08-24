using System;
using System.Configuration;
using System.Data.Entity.Infrastructure;

namespace RuInTech_TEST.Database.Context
{
    /// <summary>
    /// Фабрика для создания AssetContext
    /// </summary>
    public class AssetContextFactory : IDbContextFactory<AssetContext>
    {
        private readonly string _nameOrConnectionString;

        public AssetContextFactory()
        {
            _nameOrConnectionString = GetConnectionStringFromConfig();
        }

        public AssetContextFactory(string nameOrConnectionString)
        {
            _nameOrConnectionString = nameOrConnectionString ?? throw new ArgumentNullException(nameof(nameOrConnectionString));
        }

        public AssetContext Create()
        {
            return new AssetContext(_nameOrConnectionString);
        }

        public AssetContext Create(string nameOrConnectionString)
        {
            if (string.IsNullOrEmpty(nameOrConnectionString))
                throw new ArgumentException("Connection string cannot be null or empty", nameof(nameOrConnectionString));

            return new AssetContext(nameOrConnectionString);
        }

        private string GetConnectionStringFromConfig()
        {
            // Проверяем app.config/web.config
            var connectionString = ConfigurationManager
                .ConnectionStrings["Assets"]?.ConnectionString;

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException(
                    "Connection string 'Assets' not found in configuration file.");
            }

            return connectionString;
        }

    }
}
