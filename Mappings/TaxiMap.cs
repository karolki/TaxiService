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
    public class TaxiMap : ClassMapping<Taxi>
    {
        public TaxiMap()
        {
            Schema("DBO");
            Table("Taxis");

            Id(x => x.Tax_TaxId, m => m.Generator(Generators.Identity));


            Set(x => x.OpinionList, m =>
            {
                m.Inverse(true); m.Key(k => k.Column("Opi_TaxId"));
                m.Lazy(CollectionLazy.Lazy);
            }, r => r.OneToMany(x => x.Class(typeof(Opinion))));

            ManyToOne(x => x.Company, m =>
            {
                m.Column("Tax_ComId");
            });
            ManyToOne(x => x.Car, m =>
            {
                m.Column("Tax_CarId");
            });
            ManyToOne(x => x.Driver, m =>
            {
                m.Column("Tax_DrvId");
                m.Lazy(LazyRelation.NoLazy);
            });
           
        }
    }
}