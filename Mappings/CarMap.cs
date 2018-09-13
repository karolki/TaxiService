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
    public class CarMap : ClassMapping<Car>
    {
        public CarMap()
        {
            Schema("DBO");
            Table("Cars");

            Id(x => x.Car_CarId, m => m.Generator(Generators.Identity));

            Property(x => x.Car_Mark);
            Property(x => x.Car_Model);

        }
    }
}