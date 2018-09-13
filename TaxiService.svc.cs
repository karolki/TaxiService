using NHibernate;
using NHibernate.Linq;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using TaxiService.Utils;
using TaxiMobile.Model;
using BCrypt.Net;
using static TaxiMobile.Repositories.DriverRepository;
using System.Windows.Forms;
using System.IO;

namespace TaxiService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TaxiService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select TaxiService.svc or TaxiService.svc.cs at the Solution Explorer and start debugging.
    public class TaxiService : ITaxiService
    {

        static TaxiService()
        {
            NH.Init("Server = KAROL-PC\\KAROLSERV; Database = RestDB; User Id = sa; Password = admin");
        }

        public bool AddOpinion(Opinion opinion)
        {
            using (ISession session = NH.OpenSession())
            {
                try
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        session.Save(opinion);
                        transaction.Commit();
                    }
                    return true;//result.FirstOrDefault();
                }
                catch
                {
                    return false;
                }
            }
        }

        public List<Driver> GetActiveDrivers()
        {
            using (var session = NH.OpenSession())
            {
                return session.Query<Driver>().Where(x => x.Drv_Timing >= DateTime.Now.AddMinutes(-30)).ToList();
            }
        }

        public Taxi GetTaxi()
        {
            var tax=
             new Taxi()
            {
                Car = new Car() { Car_Mark = "Merc", Car_Model = "Benz" },
                Driver = new Driver() { Drv_Name = "Tom23", Drv_Surname = "Clark23" },
                OpinionList = new List<Opinion>() { new Opinion { Opi_Date = new DateTime(2016, 10, 21), Opi_Description = "Good enough", Opi_Rating = 6, Opi_Person = "tomek123" } }
            };
            return tax;
        }

        public bool Locate(LocateBody locateBody)//string ident, decimal lat, decimal lng)
        {
            try
            {
                using (var session = NH.OpenSession())
                {
                    session.CreateSQLQuery($"update dbo.drivers set Drv_Latitude={locateBody.lat},Drv_Longitude={locateBody.lng},Drv_Timing={DateTime.Now} where Drv_Ident={locateBody.ident}").ExecuteUpdate();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Driver Login(LoginBody loginBody)
        {
            using (ISession session = NH.OpenSession())
            {
                try
                {
                    var _password = session.CreateSQLQuery($"select Drv_Password from dbo.Drivers where Drv_Ident={loginBody.ident}").UniqueResult<string>();
                    Log(_password);
                    var check = loginBody.password == _password;//BCrypt.Net.BCrypt.CheckPassword(loginBody.password, _password);
                    if (check)
                    {
                        return session.Query<Driver>().FetchMany(x => x.OpinionList)
                    .Where(x => x.Drv_Identity == loginBody.ident).FirstOrDefault();
                    }
                    else return null;
                 //   return result;//result.FirstOrDefault();
                }
                catch(Exception e)
                {
                    Log(e.Message);
                    return null;
                }
            }
        }

        public bool Register(RegisterBody registerBody)
        {
            using (var session = NH.OpenSession())
            {
                if(session.Query<Driver>().Where(x=>x.Drv_Identity==registerBody.driver.Drv_Identity).FirstOrDefault()!=null)
                {
                    return false;
                }
                else
                {
                    try
                    {
                        using (var trans = session.BeginTransaction())
                        {
                            var driv = registerBody.driver;
                            session.CreateSQLQuery($"insert into dbo.drivers(Drv_Name,Drv_Surname,Drv_Identity,Drv_Password,Drv_Phone) values ('{driv.Drv_Name}','{driv.Drv_Surname}','{driv.Drv_Identity}','','{driv.Drv_Phone}')").ExecuteUpdate();
                            trans.Commit();
                        }
                            
                        session.CreateSQLQuery($"update dbo.drivers set Drv_Password='{BCrypt.Net.BCrypt.HashPassword(registerBody.password, BCrypt.Net.BCrypt.GenerateSalt())}'").ExecuteUpdate();
                        return true;
                    }
                    catch(Exception e)
                    {
                        Log(e.Message);
                        return false;
                    }
                }
                
            }
        }
        void Log(string e)
        {
            File.WriteAllText("log.txt", e);

        }
    }
}
