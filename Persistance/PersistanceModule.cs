using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance
{
    public class PersistanceModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public PersistanceModule(string connectionString, string migrationAssembly)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssembly;

        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StockDbContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssembly", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<StockDbContext>().As<IStockDbContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssembly", _migrationAssemblyName)
                .InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}
