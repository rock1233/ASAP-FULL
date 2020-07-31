using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace BmbWcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IBmbService" in both code and config file together.
    [ServiceContract]
    public interface IBmbService
    {

        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "/user/{UserName}/{Password}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        bool UserFound(string UserName, string Password);

        [OperationContract]
        [WebInvoke(Method = "GET",
           UriTemplate = "/user/",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare)]
        List<User> AllUsers();

        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    UriTemplate = "/user/{UserName}/{Password}",
        //    RequestFormat = WebMessageFormat.Json,
        //    ResponseFormat = WebMessageFormat.Json,
        //    BodyStyle = WebMessageBodyStyle.Wrapped)]
        //    void InsertUser(string UserName, string Password);

        [OperationContract]
        [WebInvoke(Method = "POST",
          UriTemplate = "/user/",
          RequestFormat = WebMessageFormat.Json,
          ResponseFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Bare)]
        void InsertUser(User user);

        [OperationContract]
        [WebInvoke(Method = "POST",
         UriTemplate = "/manyusers/",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare)]
        void InsertManyUsers(List<User> users);

    }
}


