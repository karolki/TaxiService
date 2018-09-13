using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using TaxiMobile.Const;
using TaxiMobile.Model;
using static TaxiMobile.Repositories.DriverRepository;

namespace TaxiService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITaxiService" in both code and config file together.
    [ServiceContract]
    public interface ITaxiService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/driver/login", BodyStyle = WebMessageBodyStyle.Bare)]
        Driver Login(LoginBody loginBody);//string ident, string password);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/driver/register", BodyStyle = WebMessageBodyStyle.Bare)]
        bool Register(RegisterBody registerBody);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/driver/addopinion", BodyStyle = WebMessageBodyStyle.Bare)]
        bool AddOpinion(Opinion opinion);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/driver/locate", BodyStyle = WebMessageBodyStyle.Bare)]
        bool Locate(LocateBody locateBody);//string ident, decimal lat, decimal lng);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/driver/getlist")]
        List<Driver> GetActiveDrivers();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/taxi")]
        Taxi GetTaxi();




    }
}
