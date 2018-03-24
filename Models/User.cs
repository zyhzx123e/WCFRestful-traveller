using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace wcfapi.Models
{
    [DataContract(Namespace = "http://tempuri.org/")]
    public class User
    {
        

        [DataMember(IsRequired = true)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = @"Username length should be between 6 and 20.")]
        public string User_name { get; set; }
   

        [DataMember(IsRequired = true)]
        public string User_profile_img { get; set; }

        [DataMember(IsRequired = true)]
        public string User_email { get; set; }

        [DataMember(IsRequired = true)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = @"Password length should be between 6 and 20.")]

        public string User_pwd { get; set; }

        public static string conStr = dbconnection.conStr;

        public User(string uname, string user_profile_img, string uemail, string upwd)
        {
            this.User_name = uname;
            this.User_profile_img = user_profile_img;
            this.User_email = uemail;
            this.User_pwd = upwd;
        }

        public User()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public int addUser()
        {
            using (SqlConnection sqlcon = new SqlConnection(conStr))
            {
                SqlCommand sqlcmd = new SqlCommand("insert into traveller_user(user_name,user_profile_img,user_email,user_pwd) values('" + User_name + "','" + User_profile_img + "','" + User_email + "','" + User_pwd + "');", sqlcon);

                sqlcon.Open();
                int count = sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                return count;

            }
        }


        public int deleteUser(string User_name)
        {
            using (SqlConnection sqlcon = new SqlConnection(conStr))
            {
                SqlCommand sqlcmd = new SqlCommand("delete from traveller_user where user_name='" + User_name + "'", sqlcon);

                sqlcon.Open();
                int count = sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                return count;

            }
        }


        public int updateUser()
        {
            using (SqlConnection sqlcon = new SqlConnection(conStr))
            {
                SqlCommand sqlcmd = new SqlCommand("update traveller_user set user_profile_img='" + User_profile_img + "',user_email='" + User_email + "',user_pwd='" + User_pwd + "' where user_name='" + User_name + "'", sqlcon);

                sqlcon.Open();
                int count = sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                return count;


            }
        }


        public List<User> SelectSpecificUser_by_email(string User_email)
        {
            using (SqlConnection sqlcon = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("select * from traveller_user where user_email='" + User_email + "'", sqlcon);

                sqlcon.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                List<User> lstbal = new List<User>();
                while (dr.Read())
                {
                    User bal = new User
                    {
                        User_name = dr["user_name"].ToString(),
                        User_profile_img = dr["user_profile_img"].ToString(),
                        User_email = dr["user_email"].ToString(),
                        User_pwd = dr["user_pwd"].ToString()
                    };

                    lstbal.Add(bal);
                }

                return lstbal;
            }
        }


        public List<User> SelectSpecificUser(string User_name)
        {
            using (SqlConnection sqlcon = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("select * from traveller_user where user_name='" + User_name + "'", sqlcon);

                sqlcon.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                List<User> lstbal = new List<User>();
                while (dr.Read())
                {
                    User bal = new User
                    {
                        User_name = dr["user_name"].ToString(),
                        User_profile_img = dr["user_profile_img"].ToString(),
                        User_email = dr["user_email"].ToString(),
                        User_pwd = dr["user_pwd"].ToString()
                    };

                    lstbal.Add(bal);
                }

                return lstbal;
            }
        }

        public List<User> SelectAllUser()
        {
            using (SqlConnection sqlcon = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("select * from traveller_user", sqlcon);

                sqlcon.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                List<User> lstbal = new List<User>();
                while (dr.Read())
                {
                    User bal = new User
                    {
                        User_name = dr["user_name"].ToString(),
                        User_profile_img = dr["user_profile_img"].ToString(),
                        User_email = dr["user_email"].ToString(),
                        User_pwd = dr["user_pwd"].ToString()
                    };

                    lstbal.Add(bal);
                }

                return lstbal;
            }
        }


        public List<User> UnauthorizedUser(string un)
        {
            if (un.Length > 0)
            {

                List<User> lstbal = new List<User>();

                User u = new User(""+un+" are not authorized to access the User_name Info!",
                    "" + un + " are not authorized to access the User_profile_img Info!",
                    "" + un + " are not authorized to access the User_email Info!",
                    "" + un + " are not authorized to access the User_pwd Info!");
                lstbal.Add(u);

                return lstbal;
            }
            else
            {

                List<User> lstbal = new List<User>();

                User u = new User("You are not authorized to access the User_name Info!",
                    "You are not authorized to access the User_profile_img Info!",
                    "You are not authorized to access the User_email Info!",
                    "You are not authorized to access the User_pwd Info!");
                lstbal.Add(u);

                return lstbal;

            }
        }










    }
}