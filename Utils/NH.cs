using NHibernate;
using System;
using System.Collections.Generic;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using TaxiService.Mappings;

namespace TaxiService.Utils
{
    public class NH
    {
        private static ISessionFactory SessionFactory;

        public static void Init(string connectionString)
        {
            if (SessionFactory != null)
                return;

            Configuration config = new Configuration().DataBaseIntegration(db =>
            {
                db.Dialect<MsSql2008Dialect>();
                db.Driver<SqlClientDriver>();
                db.ConnectionString = connectionString;
            });
            ModelMapper mapper = new ModelMapper();
            mapper.AddMappings(new List<Type>()
            {
                typeof(DriverMap),
                typeof(TaxiMap),
                typeof(OpinionMap),
                typeof(CarMap),
                typeof(CompanyMap)
            });

            try
            {
                config.AddDeserializedMapping(mapper.CompileMappingForAllExplicitlyAddedEntities(), null);

                NHibernate.Tool.hbm2ddl.SchemaMetadataUpdater.QuoteTableAndColumns(config);

                SessionFactory = config.BuildSessionFactory();
            }
            catch
            {
                throw;
            }
        }
        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}