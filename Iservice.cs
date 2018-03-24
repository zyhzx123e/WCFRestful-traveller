using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Newtonsoft.Json;

using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;

using System.Net;
using System.Net.Mail;

using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

using System.Xml.Linq;
using wcfapi.Models;
using System.Web.Http;

namespace wcfapi
{

    //visitor token :z2x9c1v3b8n5m6
    //admin token :q0w1e9r2t8y3u7i4o6p5
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "Iservice" in both code and config file together.
    [ServiceContract(Namespace = "http://tempuri.org/")]
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [XmlSerializerFormat]
    public interface Iservice
    {

        
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/getDeveloperName/{key}", 
            BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Xml, 
            ResponseFormat = WebMessageFormat.Xml)]
        string Jason_Escobar(string key);

        
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Users/getAllTravellers/{key}", 
            BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Xml, 
            ResponseFormat = WebMessageFormat.Xml)]

        userReturn getAllUsers(string key);



        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Users/getAllAdmins/{key}", 
            BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Xml, 
            ResponseFormat = WebMessageFormat.Xml)]

        adminReturn getAllAdmins(string key);




        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Users/getSpecificTravellerByUID/{User_name}/{key}", 
            BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Xml, 
            ResponseFormat = WebMessageFormat.Xml)]

        userReturn getSpecificUser(string User_name,string key);



        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Users/getSpecificTravellerByEmail/{User_email}/{key}",
            BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Xml, 
            ResponseFormat = WebMessageFormat.Xml)]

        userReturn getSpecificUser_by_email(string User_email,string key);




        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Users/getSpecificAdminByID/{Admin_id}/{key}", 
            BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Xml,
            ResponseFormat = WebMessageFormat.Xml)]

        adminReturn getSpecificAdmin(string Admin_id, string key);




        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "Users/deleteTravellerByUID/{User_name}/{key}", 
            BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Xml, 
            ResponseFormat = WebMessageFormat.Xml)]

        strReturn deleteUser(string User_name, string key);



        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "Users/deleteAdminByAdminID/{Admin_id}/{key}", 
            BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Xml, 
            ResponseFormat = WebMessageFormat.Xml)]

        strReturn deleteAdmin(string Admin_id, string key);



        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Users/addNewTraveller/{User_name}/{User_email}/{User_pwd}/{key}?User_profile_img={User_profile_img}",
             BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Xml,
            ResponseFormat = WebMessageFormat.Xml)]
        [HttpPost]
        strReturn insertUser(string User_name, string User_profile_img, string User_email, string User_pwd, string key);//



        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Users/newTraveller/{uid}/{key}",
             BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Xml, 
            ResponseFormat = WebMessageFormat.Xml)]
        string addNewTraveller(string uid,User u, string key);//string User_name, string User_profile_img, string User_email, string User_pwd

         
        

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Users/addNewAdmin/{Admin_id}/{Admin_pwd}/{key}",
             BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Xml, 
            ResponseFormat = WebMessageFormat.Xml)]

        strReturn insertAdmin(string Admin_id, string Admin_pwd, string key);




        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "Users/updateTraveller/{User_name}/{User_email}/{User_pwd}/{key}?User_profile_img={User_profile_img}",
             BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Xml,
            ResponseFormat = WebMessageFormat.Xml)]

        strReturn updateUser(string User_name, string User_profile_img, string User_email, string User_pwd, string key);




        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "Users/updateAdmin/{Admin_id}/{Admin_pwd}/{key}",
             BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Xml, 
            ResponseFormat = WebMessageFormat.Xml)]

        strReturn updateAdmin(string Admin_id, string Admin_pwd, string key);




        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "informTraveller/sendEmailToInformUserInfoChanged/{email}/{username}/{newpwd}/{key}",
            BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Xml,
            ResponseFormat = WebMessageFormat.Xml)]
        [WebMethod(Description = "If webservice return '0' means email sent failed! If return '1' means email sent successfully!")]
        string sendEmailToInformUserInfoChanged(string email, string username, string newpwd, string key);



        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "informTraveller/sendEmailToInformGoogleAccount/{email}/{username}/{newpwd}/{key}",
            BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Xml, 
            ResponseFormat = WebMessageFormat.Xml)]
        [WebMethod(Description = "If webservice return '0' means email sent failed! If return '1' means email sent successfully!")]
        string sendEmailToInformGoogleAccount(string email, string username, string newpwd, string key);




        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "informAdmin/sendEmailInformAdminPwdChanged/{newPwd}/{admin_id}/{key}",
            BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Xml, 
            ResponseFormat = WebMessageFormat.Xml)]
        [WebMethod(Description = "return string '1' : Success; return others : Fail")]
        string sendEmail_inform_admin_pwd_changed(string newPwd, string admin_id, string key);



        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "informAdmin/sendEmail_inform_new_faq_question/{faq_email}/{key}?faq_msg={faq_msg}",
          BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Xml,
          ResponseFormat = WebMessageFormat.Xml)]
        [WebMethod(Description = "return string '1' : Success; return others : Fail")]
        string sendEmail_inform_new_faq_question(string faq_msg, string faq_email, string key);







        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "informTraveller/sendRegistrationVerificationCodeToUserEmail/{New_user_email}/{str_rand4digitcode}/{key}",
            BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Xml)]
        [WebMethod(Description = "If webservice return '0' means email sent failed! If return '1' means email sent successfully!")]
        string sendRegistrationVerificationCodeToUserEmail(string New_user_email, string str_rand4digitcode, string key);



        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "getBorneoInfo/getBorneoStory/{key}",
             BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Xml, 
            ResponseFormat = WebMessageFormat.Xml)]
        [WebMethod(Description = "Get Borneo Story")]
        string getBorneoStory(string key);




        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "getBorneoCurrencyRateUSDMYR/{key}",
             BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Xml,
            ResponseFormat = WebMessageFormat.Xml)]
        [WebMethod(Description = "Get Borneo Currency From USD To MYR")]
        string getBorneoCurrency(string key);



        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "getBorneoInfo/getSabahCapitalCityKKweatherCondition/{key}",
             BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Xml, 
            ResponseFormat = WebMessageFormat.Xml)]
        [WebMethod(Description = "Get current weather condition in Kota Kinabalu(Capital City) in Sabah")]
        List<weather> getSabahCapitalCityKKweatherCondition(string key);


        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "getBorneoInfo/getSabahLatestNews/{key}", 
            BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Xml,
            ResponseFormat = WebMessageFormat.Xml)]
        [WebMethod(Description = "Get the latest news in Sabah")]
        string getSabahLatestNews(string key);




        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/getAllFaqs/{key}",
          BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Xml,
          ResponseFormat = WebMessageFormat.Xml)]
        List<faq> getAllFaqs(string key);

    }
}
