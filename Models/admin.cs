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
    public class admin
    {
      

        [DataMember(IsRequired = true)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = @"Admin ID length should be between 6 and 20.")]

        public string Admin_id { get; set; }

        [DataMember(IsRequired = true)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = @"Admin Password length should be between 6 and 20.")]

        public string Admin_pwd { get; set; }

        public static string conStr = dbconnection.conStr;

        public admin(string uname, string upwd)
        {
            this.Admin_id = uname;

            this.Admin_pwd = upwd;
        }

        public admin()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public int addAdmin()
        {
            using (SqlConnection sqlcon = new SqlConnection(conStr))
            {
                SqlCommand sqlcmd = new SqlCommand("insert into traveller_admin(admin_id,admin_pwd) values('" + Admin_id + "','" + Admin_pwd + "');", sqlcon);

                sqlcon.Open();
                int count = sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                return count;

            }
        }


        public int deleteAdmin(string Admin_id)
        {
            using (SqlConnection sqlcon = new SqlConnection(conStr))
            {
                SqlCommand sqlcmd = new SqlCommand("delete from traveller_admin where admin_id='" + Admin_id + "'", sqlcon);

                sqlcon.Open();
                int count = sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                return count;

            }
        }


        public int updateAdmin()
        {
            using (SqlConnection sqlcon = new SqlConnection(conStr))
            {
                SqlCommand sqlcmd = new SqlCommand("update traveller_admin set admin_pwd='" + Admin_pwd + "' where admin_id='" + Admin_id + "'", sqlcon);

                sqlcon.Open();
                int count = sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                return count;


            }
        }

        public List<admin> SelectSpecificAdmin(string Admin_id)
        {
            using (SqlConnection sqlcon = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("select * from traveller_admin where admin_id='" + Admin_id + "'", sqlcon);

                sqlcon.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                List<admin> lstbal = new List<admin>();
                while (dr.Read())
                {
                    admin bal = new admin
                    {
                        Admin_id = dr["admin_id"].ToString(),

                        Admin_pwd = dr["admin_pwd"].ToString()
                    };

                    lstbal.Add(bal);
                }

                return lstbal;
            }
        }

        public List<admin> SelectAllAdmin()
        {
            using (SqlConnection sqlcon = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("select * from traveller_admin", sqlcon);

                sqlcon.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                List<admin> lstbal = new List<admin>();
                while (dr.Read())
                {
                    admin bal = new admin
                    {
                        Admin_id = dr["admin_id"].ToString(),

                        Admin_pwd = dr["admin_pwd"].ToString()
                    };

                    lstbal.Add(bal);
                }

                return lstbal;
            }
        }

        public List<admin> UnauthorizedAdmin(string un)
        {
            if (un.Length > 0)
            {
                List<admin> lstbal = new List<admin>();

                admin ad = new admin(""+un+" are not authorized to access the Admin_id Info!",
                    "" + un + " are not authorized to access the Admin_pwd Info!");
                lstbal.Add(ad);

                return lstbal;

            }
            else {
                List<admin> lstbal = new List<admin>();

                admin ad = new admin("You are not authorized to access the Admin_id Info!",
                    "You are not authorized to access the Admin_pwd Info!");
                lstbal.Add(ad);

                return lstbal;
            }

         

        }










    }
}