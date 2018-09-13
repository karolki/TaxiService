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
    public class OpinionMap : ClassMapping<Opinion>
    {
        public OpinionMap()
        {
            Schema("DBO");
            Table("Opinions");

            Id(x => x.Opi_OpiId, m => m.Generator(Generators.Identity));

            Property(x => x.Opi_Description);
            Property(x => x.Opi_Date);
            Property(x => x.Opi_Person);
            Property(x => x.Opi_Rating);
            

            ManyToOne(x => x.Taxi, m =>
            {
                m.Column("Opi_TaxId");
                m.Lazy(LazyRelation.NoLazy);
            });

            ManyToOne(x => x.Driver, m =>
            {
                m.Column("Opi_DrvId");
                m.Lazy(LazyRelation.NoLazy);
            });


        }
    }
}