using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
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
using System.Web.ModelBinding;
using Newtonsoft.Json.Linq;

namespace wcfapi
{
    //visitor token :z2x9c1v3b8n5m6
    //admin token :q0w1e9r2t8y3u7i4o6p5
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "service" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select service.svc or service.svc.cs at the Solution Explorer and start debugging.
    public class service : Iservice
    {
        faq _faq;
        User user;
        admin Admin;
       
        weather w;

        auth auth;

        hypermedia hpmedia;



        //http://apilayer.net/api/live?access_key=69809f4ddf5a11551238129035e6d00c
        private void CommonAuth(string auth_user_type, string auth_key)
        {
            auth = new auth
            {
                auth_user_type = auth_user_type,
                auth_key = auth_key
            };
        }


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




        private void common_admin(string Admin_id, string Admin_pwd)
        {
            Admin = new admin
            {
                Admin_id = Admin_id,

                Admin_pwd = Admin_pwd
            };
        }

        private int verifyToken(string token) {
           
            auth = new auth();
            int i=auth.selectKeys(token);
           // Debug.WriteLine("token: "+ token + "  | get line affected: " + i);
           
            return i;

        }





        public string addNewTraveller(string uid, User u, string key)
        {

            if (!(verifyToken(key) > 0))
            {

                return "You are not authorized to access the resources";
            }
            else {
                if (key.Equals("q0w1e9r2t8y3u7i4o6p5"))
                {
                    //admin accessibility
                    Debug.WriteLine("Admin accessed addNewTraveller with new id:"+ uid);
                }
                else {
                    return "Hi visitor, You are not admin authorized role!";
                }

            }

            if (u==null) {
                return "Null User data detected";
            }


            string User_name = uid;
            string User_profile_img = u.User_profile_img;
            string User_email = u.User_email;
            string User_pwd = u.User_pwd;
            // Debug.WriteLine("insertUser entered!=====");
            Common(User_name, User_profile_img, User_email, User_pwd);
            int rowsaffected = user.addUser();


            if (rowsaffected > 0)
                return "New User " + User_name + " Added successfully";
            else
                return "New User " + User_name + " Added failed";
        }

        public string Jason_Escobar(string key)
        {
            if (!(verifyToken(key) > 0))
            {

                return "You are not authorized to access the resources";
            }
            else
            {
                if (key.Equals("q0w1e9r2t8y3u7i4o6p5"))
                {
                    Debug.WriteLine("Admin accessed Jason_Escobar");
                    //admin accessibility
                    return "Jason Escobar Zhang Yu Hao - admin";
                   
                }
                else
                {

                    return "Jason Escobar Zhang Yu Hao ";
                }

            }

           
        }


        /*
         List<object> objectList = seminarList.Cast<object>()
   .Concat(conferenceList)
   .ToList();
            */


        public userReturn getAllUsers(string key)
        {
            List<User> lstbal;
            List<hypermedia> hyper;
            if (!(verifyToken(key) > 0))
            {
                user = new User();
                hpmedia = new hypermedia();
                 lstbal = user.UnauthorizedUser("");
                hyper = hpmedia.UnauthorizedHypermedia();

             }
            else
            {
                if (key.Equals("q0w1e9r2t8y3u7i4o6p5"))
                {
                    Debug.WriteLine("Admin accessed Jason_Escobar");
                    //admin accessibility


                    user = new User();
                    lstbal = user.SelectAllUser();

                    hyper = new List<hypermedia>();
                    hypermedia post = new hypermedia("Users/addNewTraveller", "POST: To ADD a new Traveller");
                    hypermedia put = new hypermedia("Users/updateTraveller", "PUT: To UPDATE an existed Traveller");
                    hypermedia delete = new hypermedia("Users/deleteTravellerByUID/{User_name}", "DELETE: To REMOVE an existed Traveller");
                    hyper.Add(post);
                    hyper.Add(put);
                    hyper.Add(delete);


                }
                else
                {
                    hpmedia = new hypermedia();
                    user = new User();
                    lstbal = user.UnauthorizedUser("Visitors");
                    hyper = hpmedia.UnauthorizedHypermedia();
                }

            }

            
            userReturn uR = new userReturn(lstbal,hyper);
            if (uR != null)
                return uR;
            else
                return uR;
        }

        
        public adminReturn getAllAdmins(string key)
        {
            List<admin> lstbal;
            List<hypermedia> hyper;
            if (!(verifyToken(key) > 0))
            {
                Admin = new admin();
                hpmedia = new hypermedia();
                lstbal = Admin.UnauthorizedAdmin("");
                hyper = hpmedia.UnauthorizedHypermedia();


            }
            else
            {
                if (key.Equals("q0w1e9r2t8y3u7i4o6p5"))
                {
                    Admin = new admin();
                    lstbal = Admin.SelectAllAdmin();

                    hyper = new List<hypermedia>();
                    hypermedia post = new hypermedia("Users/addNewAdmin", "POST: To ADD a new Admin");
                    hypermedia put = new hypermedia("Users/updateAdmin", "PUT: To UPDATE an existed Admin");
                    hypermedia delete = new hypermedia("Users/deleteAdminByAdminID/{Admin_id}", "DELETE: To REMOVE an existed Admin");
                    hyper.Add(post);
                    hyper.Add(put);
                    hyper.Add(delete);

                }
                else {
                    Admin = new admin();
                    hpmedia = new hypermedia();
                    lstbal = Admin.UnauthorizedAdmin("Visitors");
                    hyper = hpmedia.UnauthorizedHypermedia();
                     
                }

            }

            
            adminReturn aR = new adminReturn(lstbal, hyper);

            if (aR != null)
                return aR;
            else
                return aR;
        }

        
      
        public userReturn getSpecificUser(string User_name, string key)
        {

            List<User> lstbal;
            List<hypermedia> hyper;
            if (!(verifyToken(key) > 0))
            {
                user = new User();
                hpmedia = new hypermedia();
                lstbal = user.UnauthorizedUser("");
                hyper = hpmedia.UnauthorizedHypermedia();


            }
            else
            {
                if (key.Equals("q0w1e9r2t8y3u7i4o6p5"))
                {
                    //==========
                    user = new User();
                   lstbal = user.SelectSpecificUser(User_name);

                    hyper = new List<hypermedia>();
                    //hypermedia post = new hypermedia("Users/addNewTraveller", "POST: To ADD a new Traveller");
                    hypermedia put = new hypermedia("Users/updateTraveller", "PUT: To UPDATE an existed Traveller");
                    hypermedia delete = new hypermedia("Users/deleteTravellerByUID/{User_name}", "DELETE: To REMOVE an existed Traveller");
                    //hyper.Add(post);
                    hyper.Add(put);
                    hyper.Add(delete);

                }
                else
                {
                    user = new User();
                    hpmedia = new hypermedia();
                    lstbal = user.UnauthorizedUser("Visitors");
                    hyper = hpmedia.UnauthorizedHypermedia();

                }

            }
            

            userReturn uR = new userReturn(lstbal, hyper);

            if (uR != null)
                return uR;
            else
                return uR;
        }

        
        public userReturn getSpecificUser_by_email(string User_email, string key)
        {
            List<User> lstbal;
            List<hypermedia> hyper;
            if (!(verifyToken(key) > 0))
            {
                user = new User();
                hpmedia = new hypermedia();
                lstbal = user.UnauthorizedUser("");
                hyper = hpmedia.UnauthorizedHypermedia();


            }
            else
            {
                if (key.Equals("q0w1e9r2t8y3u7i4o6p5"))
                {
                    //==========
                    user = new User();
                    lstbal = user.SelectSpecificUser_by_email(User_email);

                    hyper = new List<hypermedia>();
                    //hypermedia post = new hypermedia("Users/addNewTraveller", "POST: To ADD a new Traveller");
                    hypermedia put = new hypermedia("Users/updateTraveller", "PUT: To UPDATE an existed Traveller");
                    hypermedia delete = new hypermedia("Users/deleteTravellerByUID/{User_name}", "DELETE: To REMOVE an existed Traveller");
                    //hyper.Add(post);
                    hyper.Add(put);
                    hyper.Add(delete);

                }
                else
                {
                    user = new User();
                    hpmedia = new hypermedia();
                    lstbal = user.UnauthorizedUser("Visitors");
                    hyper = hpmedia.UnauthorizedHypermedia();

                }

            }

            //=========
           

            userReturn uR = new userReturn(lstbal, hyper);

            if (uR != null)
                return uR;
            else
                return uR;
        }

       
        public adminReturn getSpecificAdmin(string Admin_id, string key)
        {

            List<admin> lstbal;
            List<hypermedia> hyper;
            if (!(verifyToken(key) > 0))
            {
                Admin = new admin();
                hpmedia = new hypermedia();
                lstbal = Admin.UnauthorizedAdmin("");
                hyper = hpmedia.UnauthorizedHypermedia();


            }
            else
            {
                if (key.Equals("q0w1e9r2t8y3u7i4o6p5"))
                {
                    //========
                    Admin = new admin();
                    lstbal = Admin.SelectSpecificAdmin(Admin_id);

                    hyper = new List<hypermedia>();

                    //  hypermedia post = new hypermedia("Users/addNewAdmin", "POST: To ADD a new Admin");
                    hypermedia put = new hypermedia("Users/updateAdmin", "PUT: To UPDATE an existed Admin");
                    hypermedia delete = new hypermedia("Users/deleteAdminByAdminID/{Admin_id}", "DELETE: To REMOVE an existed Admin");
                    //hyper.Add(post);
                    hyper.Add(put);
                    hyper.Add(delete);

                 
                }
                else
                {
                    Admin = new admin();
                    hpmedia = new hypermedia();
                    lstbal = Admin.UnauthorizedAdmin("Visitors");
                    hyper = hpmedia.UnauthorizedHypermedia();

                }

            }
            
            adminReturn aR = new adminReturn(lstbal, hyper);

            if (aR != null)
                return aR;
            else
                return aR;
        }


          
       
        public strReturn deleteUser(string User_name, string key)
        {

            List<hypermedia> hyper;
            if (!(verifyToken(key) > 0))
            {
                hpmedia = new hypermedia();
                hyper=hpmedia.UnauthorizedHypermedia();
                strReturn strR = new strReturn("You are not authorized to access the resources!", hyper);
                return strR;
            }
            else
            {
                if (key.Equals("q0w1e9r2t8y3u7i4o6p5"))
                {
                    user = new User();
                    int rowsaffected = user.deleteUser(User_name);

                    hyper = new List<hypermedia>();

                    hypermedia get = new hypermedia("Users/getAllTravellers", "GET: To GET all existing Travellers");
                    hypermedia post = new hypermedia("Users/addNewTraveller", "POST: To ADD a new Traveller");
                    hypermedia delete = new hypermedia("Users/deleteTravellerByUID/{User_name}", "DELETE: To REMOVE another existed Traveller");
                    hyper.Add(get);
                    hyper.Add(post);
                    hyper.Add(delete);


                    if (rowsaffected > 0)
                    {
                        strReturn strR = new strReturn("User " + User_name + " has deleted.", hyper);
                        return strR;
                    }
                    else
                    {
                        strReturn strR = new strReturn("User " + User_name + " deleted failed", hyper);
                        return strR;

                    }

                }
                else {
                    hpmedia = new hypermedia();
                    hyper = hpmedia.UnauthorizedHypermedia();
                    strReturn strR = new strReturn("Visitors are not authorized to delete user!", hyper);
                    return strR;

                }
            }


                 
        }

        
        public strReturn deleteAdmin(string Admin_id, string key)
        {

            List<hypermedia> hyper;
            if (!(verifyToken(key) > 0))
            {
                hpmedia = new hypermedia();
                hyper = hpmedia.UnauthorizedHypermedia();
                strReturn strR = new strReturn("You are not authorized to access the resources!", hyper);
                return strR;
            }
            else
            {
                if (key.Equals("q0w1e9r2t8y3u7i4o6p5"))
                {
                    Admin = new admin();
                    int rowsaffected = Admin.deleteAdmin(Admin_id);

                    hyper = new List<hypermedia>();

                    hypermedia get = new hypermedia("Users/getAllAdmins", "GET: To GET all existing Admins");
                    hypermedia post = new hypermedia("Users/addNewAdmin", "POST: To ADD a new Admin");
                    hypermedia delete = new hypermedia("Users/deleteAdminByAdminID/{Admin_id}", "DELETE: To REMOVE another Admin");
                    hyper.Add(get);
                    hyper.Add(post);
                    hyper.Add(delete);



                    if (rowsaffected > 0)
                    {
                        strReturn strR = new strReturn("Admin " + Admin_id + " has deleted.", hyper);
                        return strR;

                    }
                    else
                    {
                        strReturn strR = new strReturn("Admin " + Admin_id + " deleted failed", hyper);
                        return strR;

                    }
                }
                else {
                    hpmedia = new hypermedia();
                    hyper = hpmedia.UnauthorizedHypermedia();
                    strReturn strR = new strReturn("Visitors are not authorized to delete Admin!", hyper);
                    return strR;
                }
            }






               
        }







        public strReturn insertUser(string User_name, string User_profile_img, string User_email, string User_pwd, string key)
        {
            List<hypermedia> hyper;
            if (!(verifyToken(key) > 0))
            {
                hpmedia = new hypermedia();
                hyper = hpmedia.UnauthorizedHypermedia();
                strReturn strR = new strReturn("You are not authorized to access the resources!", hyper);
                return strR;
            }
            else
            {
                if (key.Equals("q0w1e9r2t8y3u7i4o6p5"))
                {

                    // Debug.WriteLine("insertUser entered!=====");
                    Common(User_name, User_profile_img, User_email, User_pwd);
                    int rowsaffected = user.addUser();
                    hyper = new List<hypermedia>();

                    hypermedia get = new hypermedia("Users/getAllTravellers", "GET: To GET all existing Travellers");
                    hypermedia post = new hypermedia("Users/addNewTraveller", "POST: To ADD another new Traveller");
                    hypermedia put = new hypermedia("Users/updateTraveller", "PUT: To UPDATE an existing Traveller");

                    hypermedia delete = new hypermedia("Users/deleteTravellerByUID/{User_name}", "DELETE: To REMOVE existing Traveller");
                    hyper.Add(get);
                    hyper.Add(post);
                    hyper.Add(put);
                    hyper.Add(delete);


                    if (rowsaffected > 0)
                    {
                        strReturn strR = new strReturn("New User " + User_name + " Added successfully", hyper);
                        return strR;
                    }
                    else
                    {
                        strReturn strR = new strReturn("New User " + User_name + " Added failed", hyper);
                        return strR;

                    }
                }
                else {
                    hpmedia = new hypermedia();
                    hyper = hpmedia.UnauthorizedHypermedia();
                    strReturn strR = new strReturn("Visitors are not authorized to create new user!", hyper);
                    return strR;

                }


            }






 
        }

     

 
        public strReturn insertAdmin(string Admin_id, string Admin_pwd, string key)
        {
            List<hypermedia> hyper;
            if (!(verifyToken(key) > 0))
            {
                hpmedia = new hypermedia();
                hyper = hpmedia.UnauthorizedHypermedia();
                strReturn strR = new strReturn("You are not authorized to access the resources!", hyper);
                return strR;
            }
            else
            {
                if (key.Equals("q0w1e9r2t8y3u7i4o6p5"))
                {
                    common_admin(Admin_id, Admin_pwd);
                    int rowsaffected = Admin.addAdmin();

                    hyper = new List<hypermedia>();

                    hypermedia get = new hypermedia("Users/getAllAdmins", "GET: To GET all existing Admins");
                    hypermedia post = new hypermedia("Users/addNewAdmin", "POST: To ADD another new Admin");
                    hypermedia put = new hypermedia("Users/updateAdmin", "PUT: To UPDATE an existing Admin");
                    hypermedia delete = new hypermedia("Users/deleteAdminByAdminID/{Admin_id}", "DELETE: To REMOVE existing Admin");
                    hyper.Add(get);
                    hyper.Add(post);
                    hyper.Add(put);
                    hyper.Add(delete);



                    if (rowsaffected > 0)
                    {
                        strReturn strR = new strReturn("New Admin " + Admin_id + " Added successfully", hyper);
                        return strR;

                    }
                    else
                    {
                        strReturn strR = new strReturn("New Admin " + Admin_id + " Added failed", hyper);
                        return strR;

                    }
                }
                else {
                    hpmedia = new hypermedia();
                    hyper = hpmedia.UnauthorizedHypermedia();
                    strReturn strR = new strReturn("Visitors are not authorized to create new Admin!", hyper);
                    return strR;
                }
            }








               

             
        }

      
        
     
        public strReturn updateUser(string User_name, string User_profile_img, string User_email, string User_pwd, string key)
        {

            List<hypermedia> hyper;
            if (!(verifyToken(key) > 0))
            {
                hpmedia = new hypermedia();
                hyper = hpmedia.UnauthorizedHypermedia();
                strReturn strR = new strReturn("You are not authorized to access the resources!", hyper);
                return strR;
            }
            else
            {
                if (key.Equals("q0w1e9r2t8y3u7i4o6p5"))
                {
                    Common(User_name, User_profile_img, User_email, User_pwd);
                    int rowsaffected = user.updateUser();

                    hyper = new List<hypermedia>();

                    hypermedia get = new hypermedia("Users/getAllTravellers", "GET: To GET all existing Travellers");
                    hypermedia post = new hypermedia("Users/addNewTraveller", "POST: To ADD a new Traveller");
                    hypermedia put = new hypermedia("Users/updateTraveller", "PUT: To UPDATE another Traveller");

                    hyper.Add(get);
                    hyper.Add(post);
                    hyper.Add(put);

                    if (rowsaffected > 0)
                    {
                        strReturn strR = new strReturn("User " + User_name + " details updated successfully", hyper);
                        return strR;
                    }
                    else
                    {
                        strReturn strR = new strReturn("User " + User_name + " details update failed", hyper);
                        return strR;

                    }

                }
                else {
                    hpmedia = new hypermedia();
                    hyper = hpmedia.UnauthorizedHypermedia();
                    strReturn strR = new strReturn("Visitors are not authorized to update information about existing user!", hyper);
                    return strR;

                }

            }
            
        }

     
        public strReturn updateAdmin(string Admin_id, string Admin_pwd, string key)
        {
            List<hypermedia> hyper;
            if (!(verifyToken(key) > 0))
            {
                hpmedia = new hypermedia();
                hyper = hpmedia.UnauthorizedHypermedia();
                strReturn strR = new strReturn("You are not authorized to access the resources!", hyper);
                return strR;
            }
            else
            {
                if (key.Equals("q0w1e9r2t8y3u7i4o6p5"))
                {


                    common_admin(Admin_id, Admin_pwd);
                    int rowsaffected = Admin.updateAdmin();

                    hyper = new List<hypermedia>();

                    hypermedia get = new hypermedia("Users/getAllAdmins", "GET: To GET all existing Admins");
                    hypermedia post = new hypermedia("Users/addNewAdmin", "POST: To ADD a new Admin");
                    hypermedia put = new hypermedia("Users/updateAdmin", "PUT: To UPDATE another existing Admin");

                    hyper.Add(get);
                    hyper.Add(post);
                    hyper.Add(put);



                    if (rowsaffected > 0)
                    {
                        strReturn strR = new strReturn("Admin " + Admin_id + " details updated successfully", hyper);
                        return strR;

                    }
                    else
                    {
                        strReturn strR = new strReturn("Admin " + Admin_id + " details update failed", hyper);
                        return strR;

                    }

                }
                else {

                    hpmedia = new hypermedia();
                    hyper = hpmedia.UnauthorizedHypermedia();
                    strReturn strR = new strReturn("Visitors are not authorized to update information about existing Admin!", hyper);
                    return strR;
                }

            }

            
        }


        public string sendEmailToInformUserInfoChanged(string email, string username, string newpwd, string key)
        {
            
            if (!(verifyToken(key) > 0))
            {
                return "Access denied!";
            }
            else
            {
                if (key.Equals("q0w1e9r2t8y3u7i4o6p5"))
                {
                    string i = "";


                    string _id = "traveller.borneo.tour@gmail.com";
                    string _password = "zyhzx123e";
                    string subject = "Traveller User [" + username + "] Password Changed!!!  ";
                    string body = "Dear Traveller " + username + " Your Password has just been changed! Your<br/>New Password : <b>" + newpwd + "</b>. This is an auto-generated non-reply email<br/><hr/>If you did not perform this action, please contact Traveller Support Team : traveller.borneo.tour@gmail.com | +60-16-6028563 <br/><North Borneo Traveller>  --Zhang Yu Hao  [TP037390]  zyh860@gmail.com<br/><br/> <br/><hr/>All Rights Reserved || Traveller North Borneo  &copy; 2018 || Asia Pacific University || UC3F1706IT(MBT)";
                    string smtpAddress = "smtp.gmail.com";
                    bool enableSSL = true;


                    try
                    {


                        using (MailMessage mail = new MailMessage())
                        {

                            mail.From = new MailAddress(_id);
                            mail.Sender = new MailAddress(_id);
                            mail.To.Add(email);
                            mail.Subject = subject;
                            mail.Body = body;
                            mail.IsBodyHtml = true;
                            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                            // Can set to false, if you are sending pure text.

                            // mail.Attachments.Add(new Attachment("E:\\ss\\ss\\ss\\.....txt"));


                            using (SmtpClient smtp = new SmtpClient(smtpAddress, 587))
                            {
                                smtp.Credentials = new System.Net.NetworkCredential(_id, _password);
                                smtp.EnableSsl = enableSSL;
                                smtp.Send(mail);
                                smtp.Timeout = 100000;
                            }
                        }
                        i = "1";


                    }
                    catch (Exception ex)
                    {
                        i = "0";
                    }
                    return i;


                }
                else {
                    return "Visitors are not authorized to perform this action!";
                }
            }



            

        }


        public string sendEmailToInformGoogleAccount(string email, string username, string newpwd, string key)
        {

            if (!(verifyToken(key) > 0))
            {
                return "Access denied!";
            }
            else
            {//informTraveller/sendEmailToInformGoogleAccount/zyh860@gmail.com/akkkk/123456/q0w1e9r2t8y3u7i4o6p5
                if (key.Equals("q0w1e9r2t8y3u7i4o6p5"))
                {

                    string i = "";


                    string _id = "traveller.borneo.tour@gmail.com";
                    string _password = "zyhzx123e";
                    string subject = "Traveller User [" + username + "] Linked with Google!!!  ";
                    string body = "Dear Traveller " + username + " Your Google Account " + email + " has been linked ! Your<br/> Password : <b>" + newpwd + "</b>. This is an auto-generated non-reply email<br/><hr/>If you did not perform this action, please contact Traveller Support Team : traveller.borneo.tour@gmail.com | +60-16-6028563 <br/><North Borneo Traveller>  --Zhang Yu Hao  [TP037390]  zyh860@gmail.com<br/><br/> <br/><hr/>All Rights Reserved || Traveller North Borneo  &copy; 2018 || Asia Pacific University || UC3F1706IT(MBT)";
                    string smtpAddress = "smtp.gmail.com";
                    bool enableSSL = true;


                    try
                    {

                        Debug.WriteLine("now start send email"+ email +": for informing the google account binding");
                        using (MailMessage mail = new MailMessage())
                        {

                            mail.From = new MailAddress(_id);
                            mail.Sender = new MailAddress(_id);
                            mail.To.Add(email);
                            mail.Subject = subject;
                            mail.Body = body;
                            mail.IsBodyHtml = true;
                            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                            // Can set to false, if you are sending pure text.

                            // mail.Attachments.Add(new Attachment("E:\\ss\\ss\\ss\\.....txt"));


                            using (SmtpClient smtp = new SmtpClient(smtpAddress, 587))
                            {
                                smtp.Credentials = new System.Net.NetworkCredential(_id, _password);
                                smtp.EnableSsl = enableSSL;
                                smtp.Send(mail);
                                smtp.Timeout = 100000;
                            }
                        }
                        i = "1";


                    }
                    catch (Exception ex)
                    {
                        i = "0";
                    }
                    return i;

                }
                else {

                    return "Visitors are not authorized to perform this action!";
                }
            }

                 
        }









        public string sendEmail_inform_new_faq_question(string faq_msg, string faq_email, string key) {


            if (!(verifyToken(key) > 0))
            {
                return "Access denied!";
            }
            else
            {
                if (key.Equals("q0w1e9r2t8y3u7i4o6p5"))
                {


                    string return_str = "";

                    string date_today = DateTime.Now.ToString("yyyy-MM-dd");
                    string _id = "traveller.borneo.tour@gmail.com";
                    string _password = "zyhzx123e";
                    string subject = "New Traveller [" + faq_email + "] FAQ Question -> North Borneo Traveller | " + date_today + "";
                    string body = "Hi Admin!<br/><br/> The Traveller <b>" + faq_email + "</b> has following FAQ question(s): <br/><br/> <b>" + faq_msg + "</b>  <br/><hr/>All Rights Reserved || Traveller @ 2018 || Asia Pacific University || UC3F1706IT(MBT)";
                    string smtpAddress = "smtp.gmail.com";
                    bool enableSSL = true;


                    try
                    {


                        using (MailMessage mail = new MailMessage())
                        {

                            mail.From = new MailAddress(_id);
                            mail.Sender = new MailAddress(_id);
                            mail.To.Add("425352086@qq.com");//developer email
                            mail.Subject = subject;
                            mail.Body = body;
                            mail.IsBodyHtml = true;
                            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                            // Can set to false, if you are sending pure text.

                            // mail.Attachments.Add(new Attachment("E:\\ss\\ss\\ss\\.....txt"));


                            using (SmtpClient smtp = new SmtpClient(smtpAddress, 587))
                            {
                                smtp.Credentials = new System.Net.NetworkCredential(_id, _password);
                                smtp.EnableSsl = enableSSL;
                                smtp.Send(mail);
                                smtp.Timeout = 100000;
                            }
                        }

                        return_str = "1";

                    }
                    catch (Exception ex)
                    {
                        return_str = "Error Code 0 : " + ex.Message.ToString();
                        Debug.WriteLine(return_str);
                    }

                    return return_str;
                }
                else {

                    return "Visitors are not authorized to perform this action!";
                }


            }


        }

        public string sendEmail_inform_admin_pwd_changed(string newPwd, string admin_id, string key)
        {
            if (!(verifyToken(key) > 0))
            {
                return "Access denied!";
            }
            else
            {
                if (key.Equals("q0w1e9r2t8y3u7i4o6p5"))
                {



                    string return_str = "";

                    string _id = "traveller.borneo.tour@gmail.com";
                    string _password = "zyhzx123e";
                    string subject = "Traveller Admin Password Changed!!! ->  -Zhang Yu Hao Project- @2018 APU Zhang Yu Hao ";
                    string body = "Hi Admin Zhang Yu Hao!  Admin Account " + admin_id + " Password has just been changed! Your<br/>New Password : <b>" + newPwd + "</b>. This is an auto-generated non-reply email<br/><hr/> <br/><Traveller>  --Zhang Yu Hao          [TP037390]  zyh860@gmail.com<br/><br/> <br/><hr/>All Rights Reserved || Traveller @ 2018 || Asia Pacific University || UC3F1706IT(MBT)";
                    string smtpAddress = "smtp.gmail.com";
                    bool enableSSL = true;


                    try
                    {


                        using (MailMessage mail = new MailMessage())
                        {

                            mail.From = new MailAddress(_id);
                            mail.Sender = new MailAddress(_id);
                            mail.To.Add("425352086@qq.com");//developer email
                            mail.Subject = subject;
                            mail.Body = body;
                            mail.IsBodyHtml = true;
                            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                            // Can set to false, if you are sending pure text.

                            // mail.Attachments.Add(new Attachment("E:\\ss\\ss\\ss\\.....txt"));


                            using (SmtpClient smtp = new SmtpClient(smtpAddress, 587))
                            {
                                smtp.Credentials = new System.Net.NetworkCredential(_id, _password);
                                smtp.EnableSsl = enableSSL;
                                smtp.Send(mail);
                                smtp.Timeout = 100000;
                            }
                        }

                        return_str = "1";

                    }
                    catch (Exception ex)
                    {
                        return_str = "Error Code 0 : " + ex.Message.ToString();
                    }

                    return return_str;
                }
                else {

                    return "Visitors are not authrized to perform this action!";
                }
            }




        }

        public string sendRegistrationVerificationCodeToUserEmail(string New_user_email, string str_rand4digitcode, string key)
        {
            if (!(verifyToken(key) > 0))
            {
                return "Access denied!";
            }
            else
            {
                if (key.Equals("q0w1e9r2t8y3u7i4o6p5"))
                {

                    string i = "";

                    string _id = "traveller.borneo.tour@gmail.com";
                    string _password = "zyhzx123e";
                    string subject = "Traveller ->  North Borneo";
                    string body = "Hi there! You have one step a head, Your 4 digit verification code is : <b>" + str_rand4digitcode + "</b>. <br/>Please use this code to register your Traveller account and do not share this code with anyone else!<br/>This is an auto-generated non-reply email<br/><hr/> <br/> Traveller &copy; <br/><br/>CEO : Zhang Yu Hao          [TP037390]  zyh860@gmail.com<br/><br/> <br/><hr/>All Rights Reserved || Traveller Borneo Sabah @ 2018 || Asia Pacific University || UC3F1706IT(MBT)";
                    string smtpAddress = "smtp.gmail.com";
                    bool enableSSL = true;


                    try
                    {

                        /*

                         CodeProject. . Send Mail / Contact Form using ASP.NET and C# - CodeProject. 
                         * [ONLINE] Available at: https://www.codeproject.com/Tips/371417/Send-Mail-Contact-Form-using-ASP-NET-and-Csharp. 
                         * [Accessed 13 November 2017].
                         */
                        using (MailMessage mail = new MailMessage())
                        {

                            mail.From = new MailAddress(_id);
                            mail.Sender = new MailAddress(_id);
                            mail.To.Add(New_user_email);
                            mail.Subject = subject;
                            mail.Body = body;
                            mail.IsBodyHtml = true;
                            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                            // Can set to false, if you are sending pure text.

                            // mail.Attachments.Add(new Attachment("E:\\ss\\ss\\ss\\.....txt"));


                            using (SmtpClient smtp = new SmtpClient(smtpAddress, 587))
                            {
                                smtp.Credentials = new System.Net.NetworkCredential(_id, _password);
                                smtp.EnableSsl = enableSSL;
                                smtp.Send(mail);
                                smtp.Timeout = 100000;
                            }
                        }

                        // System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Check Your Traveller Email and get the random generated 4 digit code for Verification!');", true);


                        //txt_verify_code_hint.Visible = true;
                        //txt_verify_code_hint.Text = "Check Your Traveller Email and get the random generated 4 digit code for Verification!";


                        i = "1";//success send
                    }
                    catch (Exception ex)
                    {
                        i = "0";//failed to send
                        Debug.WriteLine("Could not send email\n\n" + ex.ToString());
                    }




                    return i;
                }
                else {

                    return "Visitors are not authorized to perform this action!";
                }
            }





        }




        //weather https://developer.worldweatheronline.com/my/ api:
        //d1e05d78c9394a0c8a012331171711
        //jasonescobargaviria@gmail.com
        //sabah kk weather api:
        //http://api.worldweatheronline.com/premium/v1/weather.ashx?key=d1e05d78c9394a0c8a012331171711&q=5.9804,116.0735&num_of_days=2&format=xml
        //



        /*
         * 
         * <![CDATA[ Partly cloudy ]]>
         * 
            public static void Main()
       {
          String s = "<![CDATA[ Partly cloudy ]]>";
          Console.WriteLine("The initial string: '{0}'", s);
          s = s.Replace("<![CDATA[ ", "").Replace(" ]]>", "");
          Console.WriteLine("The final string: '{0}'", s);
       }
    }
    // The example displays the following output:
    //       The initial string: '<![CDATA[ Partly cloudy ]]>'
    //       The final string: 'Partly cloudy'

         */

        public string SerializeObject(object obj)
        {
            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(obj.GetType());
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                serializer.Serialize(ms, obj);
                ms.Position = 0;
                xmlDoc.Load(ms);
                return xmlDoc.InnerXml;
            }
        }


        public string getBorneoStory(string key)
        {
            if (!(verifyToken(key) > 0))
            {
                return "Access denied!";
            }
            else
            {
                if (key.Equals("q0w1e9r2t8y3u7i4o6p5"))
                {
                    string story = "<div style='padding:10px;font-family:cursive;'><h4 style='font-size:14px;border-bottom:2px solid;padding-bottom:5px;'>What is Borneo?</h4><p style='font-size:12px; '>	Borneo is the third-largest island in the world and the largest island in Asia. At the geographic center of Maritime Southeast Asia, in relation to major Indonesian islands, it is located north of Java, west of Sulawesi, and east of Sumatra.</p><br/><h4 style='padding-top:0;margin-top:0;font-size:14px;border-bottom:2px solid;padding-bottom:5px;'>The history of Sabah in Borneo</h4><p style='font-size:12px; '>Historical records show that as early as the ninth century A.D., Sabah, then under the rule of various chieftains, was already a trading partner with the Chinese. Later on, it also established trade routes with the Spanish and the Portuguese.</p><p style='font-size:11px; '>In the 15th century, Sabah fell into the realm of the Brunei Sultanate. Possession of the state changed hands a few more times before the British North Borneo Company gained control of it. The control was handed over to the British Government after World War II. Peninsular Malaysia or Malaya as it was known then gained independence from the British in 1957. In 1963, Sabah (along with Sarawak) gained independence from the British and decided to merge with the Peninsula, an entity that was christened Malaysia.</p></div>";
                    return story;
                }
                else {
                    return "Visitors are not authorized to perform this action!";
                }
            }
                 
        }

        public string getBorneoCurrency(string key)
        {
            if (!(verifyToken(key) > 0))
            {
                return "Access denied!";
            }
            else
            {
                if (key.Equals("q0w1e9r2t8y3u7i4o6p5"))
                {


                    /*JSONobj implementation
                     =======================
                                 string json = @" {
                        ""created_at"": ""Sun, 01 Jan 2012 17:05:32 +0000"",
                          ""entities"": {
                            ""media"": [{
                              ""type"": ""photo"",
                              ""sizes"": {
                                ""large"": {
                                  ""w"": 536,
                                  ""h"": 800,
                                  ""resize"": ""fit""
                                }
                              }
                            }]
                          }
                        }
                        ";

                        JObject o = JObject.Parse(json);
                        int h = (int)o["entities"]["media"][0]["sizes"]["large"]["h"];
                        int h2 = (int)o.SelectToken("entities.media[0].sizes.large.h");
                     =====================================================================



                     */

                    //david: abc18583759f31f4ffc593f845abb86d
                    //          f2e830307335e5e37fdaf9b9ad5b17af
                    var http = (HttpWebRequest)WebRequest.Create(new Uri("http://apilayer.net/api/live?access_key=1906f945b5331c69b92b6e5b28ca520c"));
                    http.Accept = "application/json";
                    http.ContentType = "application/json";
                    http.Method = "GET";

                    var response = http.GetResponse();

                    var stream = response.GetResponseStream();
                    var sr = new StreamReader(stream);
                    string content = sr.ReadToEnd();

                    JObject o = JObject.Parse(content);
                    string rate = (string)o["quotes"]["USDMYR"];




                    return rate;
                }
                else
                {
                    return "Visitors are not authorized to perform this action!";
                }
            }

        }



        public List<weather> getSabahCapitalCityKKweatherCondition(string key)
        {

            if (!(verifyToken(key) > 0))
            {
                w = new weather();
                List<weather> wlist = w.UnauthorizedWeather();
                return wlist;
            }
            


            string nowTempC = "";
            string todayDate = "";
            string todayMaxTempC = "";
            string todayMinTempC = "";
            string todaySunRiseTime = "";
            string todaySunSetTime = "";
            string todayHumidity = "";
            string todayWeatherDesc = "";
            string todayWeatherIconUrl = "";
            string todayWindSpeedKmph = "";

            string tmrDate = "";
            string tmrMaxTempC = "";
            string tmrMinTempC = "";
            string tmrSunRiseTime = "";
            string tmrSunSetTime = "";


            //e7eee41b789d44bb8d832746181601
            //803a813db7c94e7e90333344181601 
            //f0ac8619e36e478fb9333128181601
            //8df38818ab7e4cac90b32624181601
            //dda42f16eb3b45a0a5d53247181901         
            // 52a5ab9569cc4a1988c13354181303

            //9aac8ef6a58748b9a0155312182003
            var http = (HttpWebRequest)WebRequest.Create(new Uri("http://api.worldweatheronline.com/premium/v1/weather.ashx?key=9aac8ef6a58748b9a0155312182003&q=5.9804,116.0735&num_of_days=2&format=xml"));
            http.Accept = "text/xml";
            http.ContentType = "text/xml; charset=utf-8";
            http.Method = "GET";
            var response = http.GetResponse();
            var stream = response.GetResponseStream();
            var sr = new StreamReader(stream);
            string content = sr.ReadToEnd();

            XDocument doc = XDocument.Parse(content);
            var r = doc.Root;
            var u = doc.Root.Nodes();
            var xDoc = XDocument.Parse(content);

            //var ns = xDoc.Root.Name.Namespace;
            var rootNode = xDoc.Element("data");
            var nodelistCurrentCondition = xDoc.Element("data").Elements("current_condition");

            foreach (var node in nodelistCurrentCondition)
            {
                nowTempC = node.Element("temp_C").Value;
                todayHumidity = node.Element("humidity").Value;
                todayWeatherDesc = node.Element("weatherDesc").Value.ToString().Replace("<![CDATA[ ", "").Replace(" ]]>", "");

                todayWeatherIconUrl = node.Element("weatherIconUrl").Value.ToString().Replace("<![CDATA[ ", "").Replace(" ]]>", "");

                todayWindSpeedKmph = node.Element("windspeedKmph").Value;

            }




            int count = 0;
            foreach (var fileElement in rootNode.Elements("weather"))
            {

                if (count == 0)
                {
                    todayDate = fileElement.Element("date").Value;
                    todayMaxTempC = fileElement.Element("maxtempC").Value;
                    todayMinTempC = fileElement.Element("mintempC").Value;
                    todaySunRiseTime = fileElement.Element("astronomy").Element("sunrise").Value;
                    todaySunSetTime = fileElement.Element("astronomy").Element("sunset").Value;

                }

                if (count == 1)
                {
                    tmrDate = fileElement.Element("date").Value;
                    tmrMaxTempC = fileElement.Element("maxtempC").Value;
                    tmrMinTempC = fileElement.Element("mintempC").Value;
                    tmrSunRiseTime = fileElement.Element("astronomy").Element("sunrise").Value;
                    tmrSunSetTime = fileElement.Element("astronomy").Element("sunset").Value;

                }
                count++;
            }


            //Debug.WriteLine(ulist.Count+" ============");


            w = new weather(nowTempC, todayDate, todayMaxTempC, todayMinTempC, todaySunRiseTime, todaySunSetTime, todayHumidity, todayWeatherDesc, todayWeatherIconUrl, todayWindSpeedKmph, tmrDate, tmrMaxTempC, tmrMinTempC, tmrSunRiseTime, tmrSunSetTime);


            /* XmlDocument dom = new XmlDocument();

             XmlText txt_nowTempC = dom.CreateTextNode(nowTempC);
            //  XmlElement xml_nowTempC = dom.CreateElement("nowTempC"); dom.AppendChild(txt_nowTempC);


              XmlText txt_todayDate = dom.CreateTextNode(todayDate);
              //XmlElement xml_todayDate = dom.CreateElement("todayDate"); dom.AppendChild(txt_todayDate);

              XmlText txt_todayMaxTempC = dom.CreateTextNode(todayMaxTempC);
              //XmlElement xml_todayMaxTempC = dom.CreateElement("todayMaxTempC"); dom.AppendChild(txt_todayMaxTempC);


              XmlText txt_todayMinTempC = dom.CreateTextNode(todayMinTempC);
              //XmlElement xml_todayMinTempC = dom.CreateElement("todayMinTempC"); dom.AppendChild(txt_todayMinTempC);

              XmlText txt_todaySunRiseTime = dom.CreateTextNode(todaySunRiseTime);
              //XmlElement xml_todaySunRiseTime = dom.CreateElement("todaySunRiseTime"); dom.AppendChild(txt_todaySunRiseTime);


              XmlText txt_todaySunSetTime = dom.CreateTextNode(todaySunSetTime);
              //XmlElement xml_todaySunSetTime = dom.CreateElement("todaySunSetTime"); dom.AppendChild(txt_todaySunSetTime);


              XmlText txt_todayHumidity = dom.CreateTextNode(todayHumidity);
              //XmlElement xml_todayHumidity = dom.CreateElement("todayHumidity"); dom.AppendChild(txt_todayHumidity);

              XmlText txt_todayWeatherDesc = dom.CreateTextNode(todayWeatherDesc);
              //XmlElement xml_todayWeatherDesc = dom.CreateElement("todayWeatherDesc"); dom.AppendChild(txt_todayWeatherDesc);


              XmlText txt_todayWeatherIconUrl = dom.CreateTextNode(todayWeatherIconUrl);
              //XmlElement xml_todayWeatherIconUrl = dom.CreateElement("todayWeatherIconUrl"); dom.AppendChild(txt_todayWeatherIconUrl);


              XmlText txt_todayWindSpeedKmph = dom.CreateTextNode(todayWindSpeedKmph);
              //XmlElement xml_todayWindSpeedKmph = dom.CreateElement("todayWindSpeedKmph"); dom.AppendChild(txt_todayWindSpeedKmph);


              XmlText txt_tmrDate = dom.CreateTextNode(tmrDate);
              //XmlElement xml_tmrDate = dom.CreateElement("tmrDate"); dom.AppendChild(txt_tmrDate);

              XmlText txt_tmrMaxTempC = dom.CreateTextNode(tmrMaxTempC);
              //XmlElement xml_tmrMaxTempC = dom.CreateElement("tmrMaxTempC"); dom.AppendChild(txt_tmrMaxTempC);

              XmlText txt_tmrMinTempC = dom.CreateTextNode(tmrMinTempC);
              //XmlElement xml_tmrMinTempC = dom.CreateElement("tmrMinTempC"); dom.AppendChild(txt_tmrMinTempC);

              XmlText txt_tmrSunRiseTime = dom.CreateTextNode(tmrSunRiseTime);
              //XmlElement xml_tmrSunRiseTime = dom.CreateElement("tmrSunRiseTime"); dom.AppendChild(txt_tmrSunRiseTime);

              XmlText txt_tmrSunSetTime = dom.CreateTextNode(tmrSunSetTime);
              //XmlElement xml_tmrSunSetTime = dom.CreateElement("tmrSunSetTime"); dom.AppendChild(txt_tmrSunSetTime);
             */


            List<weather> weatherList = new List<weather>();
            weatherList.Add(w);


            // load some XML ...
            return weatherList;

            //return SerializeObject(w);

        }


        public string getSabahLatestNews(string key) {

            if (!(verifyToken(key) > 0))
            {
                return "Access denied";
            }


            string date_today= DateTime.Now.ToString("yyyy-MM-dd");
            string str = "";
            var http = (HttpWebRequest)WebRequest.Create(new Uri("https://newsapi.org/v2/everything?q=sabah&language=en&from=" + date_today + "&apiKey=c47e2dce56cf4da4b857057bf076c99c"));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "GET";

            var response = http.GetResponse();

            var stream = response.GetResponseStream();
            var sr = new StreamReader(stream);
            var content = sr.ReadToEnd();

            dynamic result = JsonConvert.DeserializeObject<dynamic>(content);
            str = result.ToString();


            return str;


        }
        public List<faq> getAllFaqs(string key)
        {
            if (!(verifyToken(key) > 0))
            {
                _faq = new faq();
                List<faq> lfaq = _faq.UnauthorizedFaq();
                return lfaq;
            }

            _faq = new faq();
            List<faq> lstbal = _faq.SelectAllFaqs();

            if (lstbal != null)
                return lstbal;
            else
                return lstbal;
        }
    }
}
