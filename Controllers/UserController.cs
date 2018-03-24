using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Web.Http;
using wcfapi.Models;

public class UserController : ApiController
{
    // GET api/<controller>
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }


    User user;
    


    private void Common(string User_name, string User_profile_img, string User_email, string User_pwd)
    {
        user = new User
        {
            User_name = User_name,
            User_profile_img = User_profile_img,
            User_email = User_email,
            User_pwd = User_pwd
        };
    }


    // GET api/<controller>/5
    public string Get(int id)
    {
        return "value";
    }


    [OperationContract]
    [WebInvoke(Method = "POST", UriTemplate = "Users/createNewTraveller",
          BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Xml,
         ResponseFormat = WebMessageFormat.Xml)]
   public string createUser([FromBody]string User_name, [FromBody]string User_profile_img, [FromBody]string User_email, [FromBody]string User_pwd)
    {
        
        Common(User_name, User_profile_img, User_email, User_pwd);
        int rowsaffected = user.addUser();


        if (rowsaffected > 0)
            return "New User " + User_name + " Added successfully";
        else
            return "New User " + User_name + " Added failed";
    }
        //string User_name, string User_profile_img, string User_email, string User_pwd


    // POST api/<controller>
    public void Post([FromBody]string value)
    {
    }

    // PUT api/<controller>/5
    public void Put(int id, [FromBody]string value)
    {
    }

    // DELETE api/<controller>/5
    public void Delete(int id)
    {
    }
}
