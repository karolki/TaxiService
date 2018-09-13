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
    public class DriverMap : ClassMapping<Driver>
    {
        public DriverMap()
        {
            Schema("DBO");
            Table("Drivers");

            Id(x => x.Drv_DrvId, m=>m.Generator(Generators.Identity));

            Property(x => x.Drv_Name);
            Property(x => x.Drv_Surname);
            Property(x => x.Drv_Latitude);
            Property(x => x.Drv_Longitude);
            Property(x => x.Drv_Timing);
            Property(x => x.Drv_Phone);
            Property(x => x.Drv_Identity);

            Set(x => x.OpinionList, m =>
            {
                m.Inverse(true); m.Key(k => k.Column("Opi_DrvId"));
                m.Lazy(CollectionLazy.Lazy);
            }, r => r.OneToMany(x => x.Class(typeof(Opinion))));

            ManyToOne(x => x.Company, m =>
            {
                m.Column("Drv_ComId");
                m.Lazy(LazyRelation.NoLazy);
                //  m.Fetch(FetchKind.Join);
            });
        

        }
    }
}