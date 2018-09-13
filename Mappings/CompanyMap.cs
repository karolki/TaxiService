using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using TaxiMobile.Model;

namespace TaxiService.Mappings
{
    public class CompanyMap : ClassMapping<Company>
    {
        public CompanyMap()
        {
            Schema("DBO");
            Table("Companies");

            Id(x => x.Com_ComId, m => m.Generator(Generators.Identity));

            Property(x => x.Com_Name);

            //Set(x => x.DriverList, m =>
            //{
            //    m.Inverse(true); m.Key(k => k.Column("Drv_ComId"));
            //    m.Lazy(CollectionLazy.Lazy);
            //}, r => r.OneToMany(x => x.Class(typeof(Driver))));

            //Set(x => x.TaxiList, m =>
            //{
            //    m.Inverse(true); m.Key(k => k.Column("Tax_ComId"));
            //    m.Lazy(CollectionLazy.Lazy);
            //}, r => r.OneToMany(x => x.Class(typeof(Taxi))));

        }
    }
}